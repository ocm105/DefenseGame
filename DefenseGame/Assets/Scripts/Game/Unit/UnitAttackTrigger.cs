using System.Collections.Generic;
using UnityEngine;

public class UnitAttackTrigger : MonoBehaviour
{
    public List<IDamage> targets = new List<IDamage>();
    public List<MonsterControl> monsters = new List<MonsterControl>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            // MonsterControl mc = other.GetComponent<MonsterControl>();
            // monsters.Add(mc);
            IDamage target = other.GetComponent<IDamage>();
            targets.Add(target);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // MonsterControl mc = other.GetComponent<MonsterControl>();
        // monsters.Remove(mc);
        IDamage target = other.GetComponent<IDamage>();
        targets.Remove(target);
    }


}
