using UnityEngine;
using UnityEngine.Events;

public abstract class ChracterBase : MonoBehaviour, IDamage
{
    public float currentHp;         // 현재 채력
    public float originHP;          // 기존 체력
    public Transform damagerTrans => this.transform;

    public bool isDead => false;
    public UnityEvent onDead;

    public abstract void OnDamage(float damage);
}
