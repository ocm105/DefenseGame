using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHp : UIUtil
{
    [SerializeField] Image hp;
    
    public override void SetPosition(Vector3 target)
    {
        base.SetPosition(target);
    }
    public void SetHp(float _hp)
    {
        this.transform.SetAsLastSibling();
        hp.fillAmount = _hp;
    }
}
