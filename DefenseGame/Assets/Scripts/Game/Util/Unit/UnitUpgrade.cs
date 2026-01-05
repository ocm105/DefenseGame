using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public  class UnitUpgrade : UIUtil
{
    [SerializeField] Button button;

    public void SetUpgrade_Action(UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
    public void SetInteractable(bool active)
    {
        button.interactable = active;
    }
}
