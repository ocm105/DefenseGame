using System.Linq;
using UnityEngine;

public class UnitBase : ChracterBase
{
    public UnitData rawData;
    public UnitModel model { get; private set; }

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
    private void TryAttack()
    {

    }

    //private void Attack()
    //{
    //    var hits = Physics2D.OverlapCircleAll(this.transform.position, rawData.Range);
    //    if (hits.Length <= 0) return;

    //    int hitCount = Mathf.Min(hits.Length, rawData.AttackCount);

    //    for (int i = 0; i < hitCount; i++)
    //    {
    //        //if (hits[i].TryGetComponent<>)
    //    }
    //}

    public override void OnDamage(float damage)
    {
        if (isDead) return;
    }
}
