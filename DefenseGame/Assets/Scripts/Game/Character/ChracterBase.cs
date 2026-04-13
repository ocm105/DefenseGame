using UnityEngine;
using UnityEngine.Events;

public abstract class ChracterBase : MonoBehaviour, IDamage
{
    public float currentHp;         // 현재 채력
    public float originHP;          // 기존 체력
    public Transform damagerTrans => this.transform;

    public bool isDead => currentHp <= 0f;
    public UnityEvent onDead;

    public virtual void OnDamage(float damage) { }

    protected virtual void Attack() { }

}
