using System;
using UnityEngine;

public class MonsterControl : MonoBehaviour, IDamage
{
    [SerializeField] MonsterInfo monsterInfo;
    [SerializeField] Animator animator;
    [SerializeField] RectTransform monsterRect;
    private RectTransform[] movePath;
    private int movePathIndex = 0;
    public Action<MonsterInfo> dieAction;
    private MonsterState monsterState;
    public MonsterState MonsterState { get { return monsterState; } }

    public void MonsterStart()
    {
        movePathIndex = 0;
    }

    private void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Arive:
                monsterRect.anchoredPosition = Vector2.MoveTowards(monsterRect.anchoredPosition, movePath[movePathIndex].anchoredPosition, monsterInfo.speed * Time.deltaTime);
                DistanceCheck();
                break;
            case MonsterState.Stop:
            case MonsterState.Die:
                break;
        }
    }

    #region Fuction
    /// <summary> 몬스터 이동경로 할당 </summary>
    public void MovePath(RectTransform[] path)
    {
        movePath = path;
    }
    /// <summary> 몬스터 상태 변경 </summary>
    public void ChangeMonsterState(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Arive:
                animator.CrossFade("Walk", 0);
                break;
            case MonsterState.Stop:
                break;
            case MonsterState.Die:
                animator.CrossFade("Death", 0);
                break;
        }
        monsterState = state;
    }
    /// <summary> 도착 - 현재 거리 체크 </summary>
    private void DistanceCheck()
    {
        if (Vector2.Distance(monsterRect.anchoredPosition, movePath[movePathIndex].anchoredPosition) <= 0.05f)
        {
            movePathIndex++;
            if (movePathIndex >= movePath.Length)
                movePathIndex = 0;

            if (movePathIndex >= movePath.Length * 0.5f)
                monsterRect.localScale = new Vector2(-monsterRect.localScale.x, monsterRect.localScale.y);
            else
                monsterRect.localScale = new Vector2(monsterRect.localScale.x, monsterRect.localScale.y);
        }
    }
    #endregion


    /// <summary> interface 데미지 입는 함수 </summary>
    public void OnDamage(float damage)
    {
        Hit(damage);
    }
    /// <summary> 실질적 데미지 입는 함수 </summary>
    private void Hit(float damege)
    {
        monsterInfo.HPvalue -= damege - monsterInfo.def;

        // Debug.Log($"{damege}를 입음 HP {monsterInfo.HPvalue}");
        if (monsterInfo.HPvalue <= 0) Die();
    }
    private void Die()
    {
        ChangeMonsterState(MonsterState.Die);
    }
    public void AniEvent_Die()
    {
        dieAction.Invoke(monsterInfo);
    }
}
