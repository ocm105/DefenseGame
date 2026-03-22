using UnityEngine;

public class UnitBase : ChracterBase
{
    public UnitData rawData;
    private UnitModel model;

    public void Init(UnitData rawData)
    {
        this.rawData = rawData;

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
