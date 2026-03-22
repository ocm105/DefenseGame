using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public abstract class ChracterModel : MonoBehaviour
{
    protected Animator animator;
    public UnityEvent onDead;

    protected bool isAttack = false;
    protected CancellationTokenSource atkToken;

    protected virtual void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    protected void AttackCancle()
    {
        if (atkToken != null)
        {
            atkToken.Cancel();
            atkToken.Dispose();
            atkToken = null;
        }
    }
}
