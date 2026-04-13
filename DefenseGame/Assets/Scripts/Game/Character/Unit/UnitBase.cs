using System.Linq;
using UnityEngine;

public class UnitBase : ChracterBase
{
    public UnitData rawData;
    public UnitModel model { get; private set; }

    private UnitAniState state = UnitAniState.Idle;
    public LayerMask targetLayer;


    public void Init(UnitData rawData)
    {
        this.rawData = rawData;

        if (model == null)
        {
            var item = ModelTable.Instance.unitModelTable.Where(x => x.id == rawData.strID.ToString()).FirstOrDefault();
            var go = GameObject.Instantiate(item.prefab, this.transform);
            model = go.GetComponent<UnitModel>();
        }

        model.Init(this);
    }
    private void Update()
    {
        Attack();
    }
    protected override void Attack()
    {
        base.Attack();

        if (model.IsAttack) return;

        var hits = Physics2D.OverlapCircleAll(this.transform.position, rawData.atkRange * 0.5f, targetLayer);
        if (hits.Length <= 0) return;

        int hitCount = Mathf.Min(hits.Length, rawData.atkCount);

        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].TryGetComponent<IDamage>(out var damage))
            {
                model.Attack(damage);
            }
        }
    }
}
