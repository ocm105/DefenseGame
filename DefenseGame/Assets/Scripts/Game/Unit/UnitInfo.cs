using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatus
{

}

public class UnitInfo : MonoBehaviour
{
    private UnitControl unitControl;
    [SerializeField] Image unitImage;
    public Image UnitImage { get { return unitImage; } }
    [SerializeField] RectTransform dragObject;
    [SerializeField] RectTransform attackRange;
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
    }
    public void Spawn(Vector2 pos)
    {
        unitImage.rectTransform.anchoredPosition = pos;
        dragObject.anchoredPosition = pos;
        attackRange.anchoredPosition = pos;
        UnitInfoSet();
        unitControl.ChangeUnitAnimation(UnitAniState.Idle);
    }

    /// <summary> Unit 정보 설정 </summary>
    private void UnitInfoSet()
    {
        attackRange.localScale = new Vector3(atkRange, atkRange);
        attackRange.GetComponent<CircleCollider2D>().radius = atkRange * 10f;
    }
}
