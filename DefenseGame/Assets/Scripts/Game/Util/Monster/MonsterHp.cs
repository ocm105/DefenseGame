using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHp : UIUtil
{
    [SerializeField] protected Image hpBar;

    public override void SetPosition(Vector3 target)
    {
        base.SetPosition(target);
    }
    public virtual void SetHp(float curHp, float maxHp)
    {
        this.transform.SetAsLastSibling();
        hpBar.fillAmount = Mathf.Clamp01(curHp / maxHp);
    }
}
