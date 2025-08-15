using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    private UnitControl unitControl;
    [SerializeField] RectTransform unit;
    [SerializeField] RectTransform dragObject;
    [SerializeField] RectTransform attackRange;
    public float atkRange = 5f;
    public float atkPower = 10f;
    public int atkCount = 3;
    public float atkSpeed = 1f;
    public float atkCoolTime = 0;
    public float critical = 10f;
    public float criPower = 1.5f;
    public float damage = 0f;

    private void Awake()
    {
        unitControl = unit.GetComponent<UnitControl>();
    }
    public void Spawn(Vector2 pos)
    {
        unit.anchoredPosition = pos;
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
