using UnityEngine;
using System.Threading;
using System;

public partial class Monster : MonoBehaviour
{
    [SerializeField] public MonsterData monsterData;
    [SerializeField] Animator animator;
    private float speed = 1f;
    private float hp;
    public float HPvalue { get { return hp; } }
    public MonsterHp monsterHp { get; set; }
    private MonsterState monsterState;
    public Action<Monster> dieAction;

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

    public void Spawn()
    {
        hp = monsterData.HP;
        monsterHp.SetHp(1f);
        monsterHp.SetActive(true);
        MonsterStart();
        ChangeMonsterState(MonsterState.Arive);
    }
    private void MonsterStart()
    {
        movePathIndex = 0;
        monsterHp.SetPosition(hpPos.position);
        MonsterUpdate().Forget();
    }
    private void MonserHpSet(float damage)
    {
        hp -= damage;
        monsterHp.SetHp(Mathf.Clamp01(hp / monsterData.HP));
    }

    public void AniEvent_Die()
    {
        dieAction.Invoke(this);
    }

}
