using UnityEngine;
using UnityEngine.UI;

public partial class UnitControl : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Image attackRangeImage;
    [SerializeField] UnitAttackTrigger atkTrigger;

    private float atkRange = 5f;
    private float atkPower = 10f;
    private int atkCount = 3;
    private float atkSpeed = 1f;
    private float atkCoolTime = 0;
    private float critical = 10f;
    private float criPower = 1.5f;
    private float damage = 0f;

    private UnitAniState unitAniState;

    private void Awake()
    {
        unitRect = this.GetComponent<RectTransform>();
        dragObjectRect = dragObject.GetComponent<RectTransform>();
    }

    private void Start()
    {
        ChangeUnitAnimation(UnitAniState.Idle);
        attackRangeImage.rectTransform.localScale = new Vector3(atkRange, atkRange);
        attackRangeImage.GetComponent<CircleCollider2D>().radius = atkRange * 10f;
    }

    private void Update()
    {
        if (atkTrigger.targets.Count > 0)
        {
            atkCoolTime += Time.deltaTime;
            if (atkCoolTime >= atkSpeed)
            {
                if (atkTrigger.targets.Count >= atkCount)
                {
                    for (int i = 0; i < atkCount; i++)
                    {
                        Attack(atkTrigger.targets[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < atkTrigger.targets.Count; i++)
                    {
                        Attack(atkTrigger.targets[i]);
                    }
                }

                atkCoolTime = 0;
            }
        }
        else
            atkCoolTime = 0;

    }

    public void OnClick(bool isOn = true)
    {
        attackRangeImage.enabled = isOn;
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
                break;
            case UnitAniState.Skill:
                animator.SetBool("Skill", true);
                break;
        }
        unitAniState = state;
    }

    public void Attack(IDamage target)
    {
        if (target != null)
        {
            int ran = Random.Range(0, 101);
            if (critical >= ran)
                damage = atkPower * criPower;
            else
                damage = atkPower;

            target.OnDamage(damage);
        }
        // ChangeUnitAnimation(UnitAniState.Attack);
    }

}
