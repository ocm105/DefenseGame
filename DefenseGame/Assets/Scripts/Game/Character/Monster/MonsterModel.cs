using UnityEngine;

public class MonsterModel : ChracterModel
{
    private MonsterBase owner;
    [SerializeField] Transform hpPos;
    [SerializeField] Transform damagePos;

    public virtual void Init(MonsterBase owner)
    {
        this.owner = owner;
        animator.Rebind();
        animator.Update(0f);
    }
}
