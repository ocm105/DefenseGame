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

    private MonsterType type;

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

    public void Initialize(MonsterData data, MonsterType type, MonsterHp monsterHp)
    {
        this.monsterData = data;
        this.type = type;
        this.hp = monsterData.HP;

        this.monsterHp = monsterHp;
        this.monsterHp.SetHp(hp, monsterData.HP);
        if (type != MonsterType.Boss)
            this.monsterHp.SetPosition(hpPos.position);
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
        monsterHp.SetHp(hp, monsterData.HP);
    }
    public void AniEvent_Die()
    {
        InGameManager.Instance.MonsterRefresh(this);
    }

}
