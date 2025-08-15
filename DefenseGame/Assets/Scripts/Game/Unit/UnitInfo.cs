using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    public InGameManager inGameManager;
    private UnitControl unitControl;
    [SerializeField] Image unitImage;
    public Image UnitImage { get { return unitImage; } }
    [SerializeField] RectTransform dragObject;
    [SerializeField] Image attackRange;
    [SerializeField] Button upgradBtn;
    public string type = "Unit";
    public float atkPower = 10f;
    public float atkRange = 5f;
    public int atkCount = 3;
    public float atkSpeed = 1f;
    public float atkCoolTime = 0;
    public float critical = 10f;
    public float criPower = 1.5f;


    private void Awake()
    {
        unitControl = unitImage.GetComponent<UnitControl>();
        upgradBtn.onClick.AddListener(OnUpgrade);
    }
    public void Spawn(Vector2 pos)
    {
        unitImage.rectTransform.anchoredPosition = pos;
        dragObject.anchoredPosition = pos;
        attackRange.rectTransform.anchoredPosition = pos;
        UnitInfoSet();
        unitControl.ChangeUnitAnimation(UnitAniState.Idle);
    }

    /// <summary> Unit 정보 설정 </summary>
    private void UnitInfoSet()
    {
        attackRange.rectTransform.localScale = new Vector3(atkRange, atkRange);
        attackRange.GetComponent<CircleCollider2D>().radius = atkRange * 10f;
    }

    /// <summary> Unit 클릭 </summary>
    public void OnClick(bool isOn = true)
    {
        attackRange.enabled = isOn;
        upgradBtn.gameObject.SetActive(isOn);
    }
    private void OnUpgrade()
    {
        atkPower *= 2;
        atkSpeed *= 0.5f;
        critical *= 2;
        upgradBtn.interactable = false;
        inGameManager.UnitUpgrade(this);
    }
}
