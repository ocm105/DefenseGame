using UnityEngine;

public class UnitModel : ChracterModel
{
    private UnitBase owner;
    [SerializeField] SpriteRenderer model;
    [SerializeField] SpriteRenderer level;
    [SerializeField] Transform atkRange;
    [SerializeField] public Transform upgradeBtnPos;

    public virtual void Init(UnitBase owner)
    {
        this.owner = owner;
        atkRange.localScale = Vector3.one * owner.rawData.Range;
    }
}
