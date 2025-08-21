using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    [HideInInspector] public InGameManager inGameManager;
    private UnitControl unitControl;
    [SerializeField] Image unitImage;
    public Image UnitImage { get { return unitImage; } }
    [SerializeField] RectTransform unit;
    [SerializeField] RectTransform dragObject;
    [SerializeField] Image attackRange;
    [SerializeField] Button upgradBtn;

    public UnitData unitData { get; set; }
    public float atkCoolTime = 0;

    private void Awake()
    {
        unitControl = unit.GetComponent<UnitControl>();
        upgradBtn.onClick.AddListener(OnUpgrade);
    }
    public void Spawn(Vector2 pos)
    {
        // unitImage.rectTransform.anchoredPosition = pos;
        unit.anchoredPosition = pos;
        dragObject.anchoredPosition = pos;
        attackRange.rectTransform.anchoredPosition = pos;
        UnitInfoSet();
        unitControl.ChangeUnitAnimation(UnitAniState.Idle);
    }

    /// <summary> Unit 정보 설정 </summary>
    private void UnitInfoSet()
    {
        attackRange.rectTransform.localScale = new Vector3(unitData.Range, unitData.Range);
    }

    /// <summary> Unit 클릭 </summary>
    public void OnClick(bool isOn = true)
    {
        attackRange.enabled = isOn;
        upgradBtn.gameObject.SetActive(isOn);
    }
    private void OnUpgrade()
    {
        unitData.Attack *= 2;
        unitData.AttackSpeed *= 0.5f;
        unitData.Critical *= 2;
        upgradBtn.interactable = false;
        inGameManager.UnitUpgrade(this);
    }
}
