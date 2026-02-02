using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public partial class UnitBase : MonoBehaviour // Attack
{
    [SerializeField] Transform atkRange;

    private float atkCoolTime = 0;
    private int attackCount = 0;
    private float AttackRange = 0;

    private Collider2D[] hits;
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
                hits = Physics2D.OverlapCircleAll(this.transform.position, AttackRange, 1 << LayerMask.NameToLayer("Monster"));

                attackCount = Mathf.Min(UnitData.AttackCount, hits.Length);

                for (int i = 0; i < attackCount; i++)
                {
                    if (hits[i].TryGetComponent<IDamage>(out var damage))
                    {
                        if (Critical())
                            damage.OnDamage(UnitData.Attack * UnitData.CriticalPower);
                        else
                            damage.OnDamage(UnitData.Attack);

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

    private bool Critical()
    {
        int ran = UnityEngine.Random.Range(0, 101);
        if (UnitData.Critical >= ran)
            return true;
        else
            return false;
    }
}
