using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector] public UnitInfo unitInfo;
    private Animator animator;
    private float atkCoolTime = 0;
    private float damage = 0f;
    private int atkCount = 0;
    private UnitAniState unitAniState;
    private CancellationTokenSource cancel;
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    private void Start()
    {
        Create();
    }
    private void OnDestroy()
    {
        cancel?.Cancel();
        cancel?.Dispose();
    }
    public void Create()
    {
        atkCoolTime = 0;
        animator.Rebind();
        ChangeUnitAnimation(UnitAniState.Idle);
        this.gameObject.SetActive(true);
        UnitUpdate().Forget();
    }
    public void Delete()
    {
        atkCoolTime = 0;
        animator.Rebind();
        ChangeUnitAnimation(UnitAniState.Idle);
        this.gameObject.SetActive(false);
        cancel?.Cancel();
    }

    private async UniTaskVoid UnitUpdate()
    {
        cancel = new CancellationTokenSource();
        while (InGameManager.Instance.GameState != GameState.End)
        {
            switch (InGameManager.Instance.GameState)
            {
                case GameState.Start:

                    if (unitInfo.AtkTrigger.targets.Count > 0)
                    {
                        atkCoolTime += Time.deltaTime;
                        if (atkCoolTime >= unitInfo.UnitData.AttackSpeed)
                        {
                            ChangeUnitAnimation(UnitAniState.Attack);
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
            await UniTask.Yield(cancellationToken: cancel.Token);
        }
        atkCoolTime = 0;
        ChangeUnitAnimation(UnitAniState.Idle);
    }

    /// <summary> 유닛 상태 변경 </summary>
    private void ChangeUnitAnimation(UnitAniState state)
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

    /// <summary> 애니메이션에서 공격 </summary>
    private void Attack()
    {
        // 공격하려는 갯수가 더 많을 때
        if (unitInfo.AtkTrigger.targets.Count >= unitInfo.UnitData.AttackCount)
        {

            atkCount = unitInfo.UnitData.AttackCount;
        }
        // 공격하려는 갯수가 더 적을 때
        else
            atkCount = unitInfo.AtkTrigger.targets.Count;

        for (int i = 0; i < atkCount; i++)
        {
            if (Critical())
                damage = unitInfo.UnitData.Attack * unitInfo.UnitData.CriticalPower;
            else
                damage = unitInfo.UnitData.Attack;

            unitInfo.AtkTrigger.targets[i].OnDamage(damage);
        }
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
