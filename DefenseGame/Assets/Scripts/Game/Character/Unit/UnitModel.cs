using UnityEngine;

public class UnitModel : ChracterModel
{
    private UnitBase owner;
    [SerializeField] SpriteRenderer level;
    [SerializeField] Transform atkRange;

    public virtual void Init(UnitBase owner)
    {
        this.owner = owner;
        atkRange.localScale = Vector3.one * owner.rawData.atkRange;
    }

    public void ShowRange(bool active)
    {
        if (atkRange.gameObject.activeSelf == active) return;
        atkRange.gameObject.SetActive(active);
    }
}
