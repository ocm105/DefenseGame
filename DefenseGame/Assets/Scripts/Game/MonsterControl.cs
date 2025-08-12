using System;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    private float speed = 1f;
    private SpriteRenderer monsterSprite;
    private Transform[] movePath;
    private int movePathIndex = 0;

    public Action<GameObject> dieAction;
    private MonsterState monsterState;

    private void Awake()
    {
        monsterSprite = this.GetComponent<SpriteRenderer>();
    }

    public void MonsterStart()
    {
        movePathIndex = 0;
        monsterState = MonsterState.Arive;
    }
    private void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Arive:
                this.transform.position = Vector2.MoveTowards(this.transform.position, movePath[movePathIndex].position, speed * Time.deltaTime);
                DistanceCheck();
                break;
            case MonsterState.Stop:
            case MonsterState.Die:
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
                break;
            case MonsterState.Stop:
                break;
            case MonsterState.Die:
                dieAction.Invoke(this.gameObject);
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

            monsterSprite.flipX = movePathIndex >= movePath.Length * 0.5f ? true : false;
        }
    }
    #endregion
}
