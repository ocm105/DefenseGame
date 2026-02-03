using TMPro;
using UnityEngine;

public class UnitInfoWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI unitName;
    [SerializeField] TextMeshProUGUI unitLevel;
    [SerializeField] TextMeshProUGUI unitJob;
    [SerializeField] TextMeshProUGUI unitSynergy;
    [SerializeField] TextMeshProUGUI unitMana;
    [SerializeField] TextMeshProUGUI unitAttack;
    [SerializeField] TextMeshProUGUI unitAttackCount;
    [SerializeField] TextMeshProUGUI unitAttackRange;
    [SerializeField] TextMeshProUGUI unitCriticalPrecent;
    [SerializeField] TextMeshProUGUI unitCriticalPower;

    private void SetData(UnitData data)
    {
        unitName.text = StringExtension.StringMerge("ÀÌ¸§ : ", data.Name);
        unitLevel.text = StringExtension.StringMerge("·¹º§ : ", data.Level.ToString());
        unitJob.text = StringExtension.StringMerge("Á÷¾÷ : ", data.Job.ToString());
        string synergy = string.Empty;
        foreach (var str in data.Synergy)
        {
            synergy += StringExtension.StringMerge(str, " ");
        }
        unitSynergy.text = StringExtension.StringMerge("½Ã³ÊÁö€ : ", synergy);
        unitMana.text = StringExtension.StringMerge("¸¶³ª : ", data.Mana.ToString());
        unitAttack.text = StringExtension.StringMerge("°ø°Ý·Â€ : ", data.Attack.ToString());
        unitAttackCount.text = StringExtension.StringMerge("°ø°Ý°¡´É ¼ö: ", data.AttackCount.ToString());
        unitAttackRange.text = StringExtension.StringMerge("°ø°Ý ¹üÀ§ : ", data.Range.ToString());
        unitCriticalPrecent.text = StringExtension.StringMerge("Ä¡¸íÅ¸À²: ", data.Critical.ToString(), "%");
        unitCriticalPower.text = StringExtension.StringMerge("Ä¡¸íÅ¸ :", (data.CriticalPower * 100).ToString(), "%");
    }

    public void SetActive(bool isActive, UnitData data = null)
    {
        this.gameObject.SetActive(isActive);
        if (isActive) SetData(data);
    }

}
