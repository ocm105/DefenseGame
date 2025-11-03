using System;
using UnityEngine;

public class MonsterControl : MonoBehaviour, IDamage
{
    private MonsterInfo monsterInfo;
    private Animator animator;
    private SpriteRenderer sprite;
    private Transform[] movePath;
    private int movePathIndex = 0;
    public Action<MonsterInfo> dieAction;
    private MonsterState monsterState;
    public MonsterState MonsterState { get { return monsterState; } }

    [SerializeField] Transform hpPos;

    private void Awake()
    {
        monsterInfo = this.GetComponent<MonsterInfo>();
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
    }
    public void MonsterStart()
    {
        movePathIndex = 0;
        monsterInfo.monsterHp.SetPosition(hpPos.position);
    }

    private void Update()
    {
        switch (InGameManager.Instance.GameState)
        {
            case GameState.Start:
                switch (monsterState)
                {
                    case MonsterState.Arive:
                        this.transform.position = Vector2.MoveTowards(this.transform.position, movePath[movePathIndex].position, monsterInfo.speed * Time.deltaTime);
                        monsterInfo.monsterHp.SetPosition(hpPos.position);
                        DistanceCheck();
                        break;
                    case MonsterState.Stop:
                    case MonsterState.Die:
                        break;
                }
                break;
            case GameState.Pause:
            case GameState.End:
                break;
        }
    }

    #region Fuction
    /// <summary> 몬스터 이동경로 할당 </summary>
    public void MovePath(Transform[] path)
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
        if (Vector2.Distance(this.transform.position, movePath[movePathIndex].position) <= 0.05f)
        {
            movePathIndex++;
            if (movePathIndex >= movePath.Length)
                movePathIndex = 0;

            if (movePathIndex >= movePath.Length * 0.5f)
                sprite.flipX = true;
            else
                sprite.flipX = false;
        }
    }
    #endregion


    /// <summary> interface 데미지 입는 함수 </summary>
    public void OnDamage(float damage)
    {
        if (monsterState == MonsterState.Die) return;
        Hit(damage);
    }
    /// <summary> 실질적 데미지 입는 함수 </summary>
    private void Hit(float damege)
    {
        monsterInfo.MonserHpSet(damege - monsterInfo.monsterData.DEF);

        // Debug.Log($"{damege}를 입음 HP {monsterInfo.HPvalue}");
        if (monsterInfo.HPvalue <= 0) Die();
    }
    private void Die()
    {
        ChangeMonsterState(MonsterState.Die);
        monsterInfo.monsterHp.SetActive(false);
    }
    public void AniEvent_Die()
    {
        dieAction.Invoke(monsterInfo);
        sprite.flipX = false;
    }
}
