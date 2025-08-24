using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    private UnitInfo unitInfo;
    private Animator animator;
    private float atkCoolTime = 0;
    private float damage = 0f;

    private UnitAniState unitAniState;

    private void Awake()
    {
        unitInfo = this.GetComponentInParent<UnitInfo>();
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        switch (unitInfo.inGameManager.GameState)
        {
            case GameState.Start:
                if (unitInfo.AtkTrigger.targets.Count > 0)
                {
                    atkCoolTime += Time.deltaTime;
                    if (atkCoolTime >= unitInfo.UnitData.AttackSpeed)
                    {
                        if (unitInfo.AtkTrigger.targets.Count >= unitInfo.UnitData.AttackCount)
                        {
                            // 공격하려는 갯수가 더 많을 때
                            for (int i = 0; i < unitInfo.UnitData.AttackCount; i++)
                            {
                                Attack(unitInfo.AtkTrigger.targets[i]);
                            }
                        }
                        else
                        {
                            // 공격하려는 갯수가 더 적을 때
                            for (int i = 0; i < unitInfo.AtkTrigger.targets.Count; i++)
                            {
                                Attack(unitInfo.AtkTrigger.targets[i]);
                            }
                        }
                        atkCoolTime = 0;
                    }
                }
                else
                {
                    ChangeUnitAnimation(UnitAniState.Idle);
                    atkCoolTime = 0;
                }
                break;
            case GameState.Pause:
            case GameState.End:
                break;
        }
    }

    /// <summary> 유닛 상태 변경 </summary>
    public void ChangeUnitAnimation(UnitAniState state)
    {
        switch (state)
        {
            case UnitAniState.Idle:
                break;
            case UnitAniState.Attack:
                break;
            case UnitAniState.Skill:
                break;
        }
        animator.SetInteger("Index", (int)state);
        unitAniState = state;
    }
    /// <summary> 공격 </summary>
    public void Attack(IDamage target)
    {
        if (target != null)
        {
            if (Critical())
                damage = unitInfo.UnitData.Attack * unitInfo.UnitData.CriticalPower;
            else
                damage = unitInfo.UnitData.Attack;

            target.OnDamage(damage);
        }
        ChangeUnitAnimation(UnitAniState.Attack);
    }

    private bool Critical()
    {
        int ran = Random.Range(0, 101);
        if (unitInfo.UnitData.Critical >= ran)
            return true;
        else
            return false;
    }

}
