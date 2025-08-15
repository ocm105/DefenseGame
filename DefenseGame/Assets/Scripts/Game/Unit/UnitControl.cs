using UnityEngine;
using UnityEngine.UI;

public class UnitControl : MonoBehaviour
{
    [SerializeField] UnitInfo unitInfo;
    public UnitInfo UnitInfo { get { return unitInfo; } }
    [SerializeField] Animator animator;
    [SerializeField] Image attackRangeImage;
    [SerializeField] UnitAttackTrigger atkTrigger;
    public float damage = 0f;

    private UnitAniState unitAniState;

    private void Update()
    {
        if (atkTrigger.targets.Count > 0)
        {
            unitInfo.atkCoolTime += Time.deltaTime;
            if (unitInfo.atkCoolTime >= unitInfo.atkSpeed)
            {
                if (atkTrigger.targets.Count >= unitInfo.atkCount)
                {
                    // 공격하려는 갯수가 더 많을 때
                    for (int i = 0; i < unitInfo.atkCount; i++)
                    {
                        Attack(atkTrigger.targets[i]);
                    }
                }
                else
                {
                    // 공격하려는 갯수가 더 적을 때
                    for (int i = 0; i < atkTrigger.targets.Count; i++)
                    {
                        Attack(atkTrigger.targets[i]);
                    }
                }

                unitInfo.atkCoolTime = 0;
            }
        }
        else
            unitInfo.atkCoolTime = 0;
    }
    /// <summary> Unit 클릭 </summary>
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
    /// <summary> 공격 </summary>
    public void Attack(IDamage target)
    {
        if (target != null)
        {
            int ran = Random.Range(0, 101);
            if (unitInfo.critical >= ran)
                damage = unitInfo.atkPower * unitInfo.criPower;
            else
                damage = unitInfo.atkPower;

            target.OnDamage(damage);
        }
        // ChangeUnitAnimation(UnitAniState.Attack);
    }

}
