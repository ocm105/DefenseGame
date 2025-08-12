using UnityEngine;

public partial class UnitControl : MonoBehaviour
{
    [SerializeField] GameObject dragObject;
    [SerializeField] GameObject shotAttackRange;
    [SerializeField] GameObject longAttackRange;

    public void OnClick(bool isOn = true)
    {
        dragObject.SetActive(isOn);
        shotAttackRange.SetActive(isOn);
    }

}
