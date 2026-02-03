
using UnityEngine;

public interface IDamage
{
    public Transform damagerTrans { get; }
    public bool isDead { get; }
    public void OnDamage(float damage);
}
