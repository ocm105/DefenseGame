
using UnityEngine;

public interface IDamage
{
    public Transform damagerTrans { get; }
    public void OnDamage(float damage);
}
