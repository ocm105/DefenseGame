using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitUpgrade : UIUtil
{
    [SerializeField] Button button;

    public override void SetActive(bool active)
    {
        base.SetActive(active);
    }
    public override void SetPosition(Vector3 target)
    {
        base.SetPosition(target);
    }
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
