using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

public partial class Monster : MonoBehaviour, IDamage // Control
{
    [SerializeField] Transform hpPos;
    [SerializeField] Transform damagePos;
    [SerializeField] SpriteRenderer sprite;
    private Transform[] movePath;
    private int movePathIndex = 0;

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
    private void ChangeMonsterState(MonsterState state)
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
    public void MovePath(Transform[] path)
    {
        movePath = path;
    }

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

    public void OnDamage(float damage)
    {
        if (monsterState == MonsterState.Die) return;
        Hit(damage);
    }
    private void Hit(float damege)
    {
        InGameManager.Instance.GetDamageFont(damege, damagePos).Forget();
        MonserHpSet(damege - monsterData.DEF);

        if (Dead)
        {
            InGameManager.Instance.SpendGold(monsterData.GOLD);
            InGameManager.Instance.MonsterDespawn();
            ChangeMonsterState(MonsterState.Die);
            monsterHp.SetActive(false);
            monsterHp = null;
        }
    }
}
