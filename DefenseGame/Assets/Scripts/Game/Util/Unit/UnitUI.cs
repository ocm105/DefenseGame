using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public partial class UnitUI : UIUtil
{
    [SerializeField] Button upgradeBtn;
    [SerializeField] Button sellBtn;

    public void SetUpgrade_Action(UnityAction action)
    {
        upgradeBtn.onClick.RemoveAllListeners();
        upgradeBtn.onClick.AddListener(action);
    }
    public void SetUpgrade_Interactable(bool active)
    {
        upgradeBtn.interactable = active;
    }

    public void SetSell_Action(UnityAction action)
    {
        sellBtn.onClick.RemoveAllListeners();
        sellBtn.onClick.AddListener(action);
    }
    public void SetSell_Interactable(bool active)
    {
        sellBtn.interactable = active;
    }
}
