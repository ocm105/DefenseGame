using UnityEngine;

public class MonsterBase : ChracterBase
{
    public MonsterData rawData;

    public void Init(MonsterData rawData)
    {
        this.rawData = rawData;
    }

    public override void OnDamage(float damage)
    {

    }
}
