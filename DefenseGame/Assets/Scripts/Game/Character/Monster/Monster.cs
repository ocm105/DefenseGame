using UnityEngine;
using System.Threading;
using System;

public partial class Monster : MonoBehaviour
{
    [SerializeField] MonsterData monsterData;
    [SerializeField] Animator animator;

    private float speed = 1f;
    private float hp;
    private bool Dead => hp <= 0;
    private MonsterHp monsterHp;
    private MonsterState monsterState;

    public CancellationTokenSource cancel;

    private void OnDisable()
    {
        cancel?.Cancel();
    }
    private void OnDestroy()
    {
        cancel?.Cancel();
        cancel?.Dispose();
    }

    public void Initialize(MonsterData data, MonsterHp monsterHp)
    {
        this.monsterData = data;
        this.monsterHp = monsterHp;
        this.monsterHp.SetHp(1f);
        this.monsterHp.SetPosition(hpPos.position);
        hp = monsterData.HP;
        movePathIndex = 0;
    }
    public void Spawn()
    {
        monsterHp.SetActive(true);
        this.gameObject.SetActive(true);
        ChangeMonsterState(MonsterState.Arive);
        MonsterUpdate().Forget();
    }
    private void MonserHpSet(float damage)
    {
        hp -= damage;
        monsterHp.SetHp(Mathf.Clamp01(hp / monsterData.HP));
    }
    public void AniEvent_Die()
    {
        InGameManager.Instance.SpendGold(monsterData.GOLD);
        InGameManager.Instance.MonsterDespawn(this);
    }

}
