using System;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] RectTransform monsterRect;
    private RectTransform myRect;
    private RectTransform[] movePath;
    private int movePathIndex = 0;
    private float speed = 200f;
    private MonsterState monsterState;
    public Action<GameObject> dieAction;

    private void Awake()
    {
        myRect = this.GetComponent<RectTransform>();
    }

    public void MonsterStart()
    {
        movePathIndex = 0;
        ChangeMonsterState(MonsterState.Arive);
    }
    private void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Arive:
                myRect.anchoredPosition = Vector2.MoveTowards(myRect.anchoredPosition, movePath[movePathIndex].anchoredPosition, speed * Time.deltaTime);
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
                dieAction.Invoke(this.gameObject);
                animator.CrossFade("Death", 0);
                break;
        }
        monsterState = state;
    }
    /// <summary> 도착 - 현재 거리 체크 </summary>
    private void DistanceCheck()
    {
        if (Vector2.Distance(myRect.anchoredPosition, movePath[movePathIndex].anchoredPosition) <= 0.05f)
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
}
