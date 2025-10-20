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
        unitName.text = StringExtension.StringMerge("이름 : ", data.Name);
        unitLevel.text = StringExtension.StringMerge("레벨 : ", data.Level.ToString());
        unitJob.text = StringExtension.StringMerge("직업 : ", data.Job);
        string synergy = string.Empty;
        foreach (var str in data.Synergy)
        {
            synergy += StringExtension.StringMerge(str, " ");
        }
        unitSynergy.text = StringExtension.StringMerge("시너지 : ", synergy);
        unitMana.text = StringExtension.StringMerge("마나 : ", data.Mana.ToString());
        unitAttack.text = StringExtension.StringMerge("데미지 : ", data.Attack.ToString());
        unitAttackCount.text = StringExtension.StringMerge("때리는 수 : ", data.AttackCount.ToString());
        unitAttackRange.text = StringExtension.StringMerge("범위 : ", data.Range.ToString());
        unitCriticalPrecent.text = StringExtension.StringMerge("치명타율 : ", data.Critical.ToString(), "%");
        unitCriticalPower.text = StringExtension.StringMerge("치명타 배율 :", (data.CriticalPower * 100).ToString(), "%");
    }

    public void SetActive(bool isActive, UnitData data = null)
    {
        this.gameObject.SetActive(isActive);
        if (isActive) SetData(data);
    }

}
