using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonsterHp
{
    [SerializeField] TMP_Text text;


    public override void SetHp(float curHp, float maxHp)
    {
        base.SetHp(curHp, maxHp);

        text.text = $"{curHp} / {maxHp}";
    }
}
