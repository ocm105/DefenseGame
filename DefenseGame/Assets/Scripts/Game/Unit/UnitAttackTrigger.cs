using System.Collections.Generic;
using UnityEngine;

public class UnitAttackTrigger : MonoBehaviour
{
    public List<IDamage> targets = new List<IDamage>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            IDamage target = other.GetComponent<IDamage>();
            targets.Add(target);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        IDamage target = other.GetComponent<IDamage>();
        targets.Remove(target);
    }


}
