using Cysharp.Threading.Tasks;
using LitMotion;
using System;
using System.Threading;
using UnityEngine;

public partial class UnitBase : MonoBehaviour // Attack
{
    [SerializeField] Transform atkRange;
    private float atkCoolTime = 0;
    private int attackCount = 0;
    private float AttackRange = 0;

    private Collider2D[] hits;
    private LayerMask targetLayer { get { return 1 << LayerMask.NameToLayer("Monster"); } }
    private CancellationTokenSource atkCancel;

    private void InitAttackInfo(UnitData data)
    {
        AttackRange = data.Range * 0.5f;
        atkRange.localScale = Vector3.one * data.Range;
    }
    private void AttackCancel()
    {
        if (atkCancel != null)
        {
            atkCancel.Cancel();
            atkCancel.Dispose();
            atkCancel = null;
        }
    }

    private async UniTaskVoid UnitUpdate()
    {
        atkCancel = new CancellationTokenSource();
        while (InGameManager.Instance.GameState != GameState.End)
        {
            atkCoolTime += Time.deltaTime;
            if (InGameManager.Instance.GameState == GameState.Start && atkCoolTime >= UnitData.AttackSpeed)
            {
                if (TryAttack())
                    atkCoolTime = 0;
            }
            await UniTask.Yield(cancellationToken: atkCancel.Token);
        }
    }

    public bool TryAttack()
    {
        bool isAttack = false;

        foreach (var unit in unitModel)
        {
            if (unit != null && !unit.isAttack)
            {
                hits = Physics2D.OverlapCircleAll(this.transform.position, AttackRange, targetLayer);

                Array.Sort(hits, (a, b) =>
                {
                    float distA = (transform.position - a.transform.position).sqrMagnitude;
                    float distB = (transform.position - b.transform.position).sqrMagnitude;
                    return distA.CompareTo(distB);
                });

                attackCount = Mathf.Min(UnitData.AttackCount, hits.Length);

                for (int i = 0; i < attackCount; i++)
                {
                    if (hits[i].TryGetComponent<IDamage>(out var damage))
                    {
                        switch (UnitData.Job)
                        {
                            case UnitJobType.Warrior:
                            case UnitJobType.Ranger:
                            case UnitJobType.Thief:
                                OnDamage(damage);
                                break;
                            case UnitJobType.Wizard:
                                if (unit.attackPrefab != null && unit.attackPos != null)
                                {
                                    GameObject go = Instantiate(unit.attackPrefab);
                                    LMotion.Create(0f, 1f, 0.5f)
                                           .WithOnComplete(() =>
                                           {
                                               OnDamage(damage);
                                               Destroy(go);
                                           })
                                           .Bind(t =>
                                           {
                                               go.transform.position = Vector2.Lerp(unit.attackPos.position, damage.damagerTrans.position, t);
                                           }).AddTo(go);
                                }
                                break;
                        }

                        isAttack = true;
                    }
                }
                if (isAttack)
                {
                    unit.animator.SetTrigger("Attack");
                    break;
                }
            }
        }

        return isAttack;
    }

    private void OnDamage(IDamage damage)
    {
        int ran = UnityEngine.Random.Range(0, 101);
        if (UnitData.Critical >= ran)
            damage.OnDamage(UnitData.Attack * UnitData.CriticalPower);
        else
            damage.OnDamage(UnitData.Attack);
    }
}
