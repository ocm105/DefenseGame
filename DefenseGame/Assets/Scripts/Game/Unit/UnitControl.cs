using UnityEngine;
using UnityEngine.EventSystems;

public partial class UnitControl : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject shotAttackRange;
    [SerializeField] GameObject longAttackRange;
    private UnitAniState unitAniState;

    private void Awake()
    {
        unitRect = this.GetComponent<RectTransform>();
        dragObjectRect = dragObject.GetComponent<RectTransform>();
    }

    private void Start()
    {
        ChangeUnitAnimation(UnitAniState.Idle);
    }

    public void OnClick(bool isOn = true)
    {
        shotAttackRange.SetActive(isOn);
    }

    /// <summary> 몬스터 상태 변경 </summary>
    public void ChangeUnitAnimation(UnitAniState state)
    {
        switch (state)
        {
            case UnitAniState.Idle:
                // animator.CrossFade("Idle", 0);
                break;
            case UnitAniState.Attack:
                animator.SetBool("Attack", true);
                Attack();
                break;
            case UnitAniState.Skill:
                animator.SetBool("Skill", true);
                break;
        }
        unitAniState = state;
    }

    private void Attack()
    {
    }
}
