using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

public partial class Monster : MonoBehaviour, IDamage // Control
{
    [SerializeField] Transform hpPos;
    [SerializeField] SpriteRenderer sprite;
    private Transform[] movePath;
    private int movePathIndex = 0;

    public bool Death { get; set; }
    private async UniTaskVoid MonsterUpdate()
    {
        cancel = new CancellationTokenSource();
        while (InGameManager.Instance.GameState != GameState.End || monsterState != MonsterState.Die)
        {
            switch (InGameManager.Instance.GameState)
            {
                case GameState.Start:
                    switch (monsterState)
                    {
                        case MonsterState.Arive:
                            this.transform.position = Vector2.MoveTowards(this.transform.position, movePath[movePathIndex].position, speed * Time.deltaTime);
                            monsterHp.SetPosition(hpPos.position);
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
            await UniTask.Yield(cancellationToken: cancel.Token);
        }
    }

    #region Fuction
    /// <summary> 몬스터 상태 변경 </summary>
    private void ChangeMonsterState(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Arive:
                Death = false;
                animator.CrossFade("Walk", 0);
                break;
            case MonsterState.Stop:
                break;
            case MonsterState.Die:
                animator.CrossFade("Death", 0);
                monsterHp.SetActive(false);
                break;
        }
        monsterState = state;
    }
    /// <summary> 몬스터 이동경로 할당 </summary>
    public void MovePath(Transform[] path)
    {
        movePath = path;
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
        MonserHpSet(damege - monsterData.DEF);

        // Debug.Log($"{damege}를 입음 HP {monsterInfo.HPvalue}");
        if (HPvalue <= 0) Die();
    }
    private void Die()
    {
        ChangeMonsterState(MonsterState.Die);
    }
}
