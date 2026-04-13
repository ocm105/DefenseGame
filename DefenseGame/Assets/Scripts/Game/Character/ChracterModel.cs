using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public abstract class ChracterModel : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    public UnityEvent onDead;

    protected bool isAttack = false;
    public bool IsAttack => isAttack;
    protected CancellationTokenSource atkToken;

    protected void AttackCancle()
    {
        if (atkToken != null)
        {
            atkToken.Cancel();
            atkToken.Dispose();
            atkToken = null;
        }
    }

    public virtual void Attack(IDamage damage)
    {
        if (isAttack) return;
        AttackCancle();
        atkToken = new CancellationTokenSource();

        AttackAsync(damage).Forget();
    }

    protected virtual async UniTask AttackAsync(IDamage damage)
    {
        await UniTask.CompletedTask;
    }
}
