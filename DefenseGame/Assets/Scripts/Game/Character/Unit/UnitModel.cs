using Cysharp.Threading.Tasks;
using UnityEngine;

public class UnitModel : ChracterModel
{
    private UnitBase owner;
    private UnitData rawData;
    [SerializeField] SpriteRenderer level;
    [SerializeField] Transform atkRange;


    public virtual void Init(UnitBase owner)
    {
        this.owner = owner;
        this.rawData = owner.rawData;
        atkRange.localScale = Vector3.one * owner.rawData.atkRange;
    }

    public void ShowRange(bool active)
    {
        if (atkRange.gameObject.activeSelf == active) return;
        atkRange.gameObject.SetActive(active);
    }

    public override void Attack(IDamage damage)
    {
        base.Attack(damage);
    }

    protected override async UniTask AttackAsync(IDamage damage)
    {
        isAttack = true;

        await BaseAttack(damage);

        isAttack = false;
    }

    private async UniTask BaseAttack(IDamage damage)
    {
        animator.SetTrigger("Attack");

        damage.OnDamage(rawData.atk);

        await UniTask.WaitForSeconds(0.75f, cancellationToken: atkToken.Token).SuppressCancellationThrow();
    }
}
