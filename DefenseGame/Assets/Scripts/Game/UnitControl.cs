using UnityEngine;
using UnityEngine.EventSystems;

public partial class UnitControl : MonoBehaviour
{

    [SerializeField] GameObject shotAttackRange;
    [SerializeField] GameObject longAttackRange;

    public void OnClick(bool isOn = true)
    {
        shotAttackRange.SetActive(true);
    }
}
