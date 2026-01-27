using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

public class MonsterUI : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] int nomalHpPoolCount;
    [SerializeField] GameObject monsterHPPrefab;
    [Range(1, 100)]
    [SerializeField] int bossHpPoolCount;
    [SerializeField] GameObject bossHPPrefab;
    private LinkedList<MonsterHp> monsterHps = new LinkedList<MonsterHp>();
    private LinkedList<MonsterHp> bossHps = new LinkedList<MonsterHp>();

    [SerializeField] public MonsterHp testBossHp;

    private void Start()
    {
        MonsterHpPool(nomalHpPoolCount);
        BossMonsterHpPool(bossHpPoolCount);
    }

    private void MonsterHpPool(int count)
    {
        GameObject obj;
        MonsterHp monsterHp;
        for (int i = 0; i < count; i++)
        {
            obj = Instantiate(monsterHPPrefab, this.transform);
            monsterHp = obj.GetComponent<MonsterHp>();
            monsterHp.SetActive(false);
            monsterHps.AddLast(monsterHp);
        }
    }
    public MonsterHp SetMonsterHP()
    {
        MonsterHp monsterHp = null;
        foreach (MonsterHp hp in monsterHps)
        {
            if (!hp.isUsed)
            {
                monsterHp = hp;
                break;
            }
        }
        if (monsterHp == null)
        {
            MonsterHpPool(+1);
            monsterHp = monsterHps.Last.Value;
        }
        return monsterHp;
    }
    private void BossMonsterHpPool(int count)
    {
        GameObject obj;
        MonsterHp monsterHp;
        for (int i = 0; i < count; i++)
        {
            obj = Instantiate(bossHPPrefab, this.transform);
            monsterHp = obj.GetComponent<MonsterHp>();
            monsterHp.SetActive(false);
            bossHps.AddLast(monsterHp);
        }
    }
    public MonsterHp SetBossMonsterHP()
    {
        MonsterHp monsterHp = null;
        foreach (MonsterHp hp in bossHps)
        {
            if (!hp.isUsed)
            {
                monsterHp = hp;
                break;
            }
        }
        if (monsterHp == null)
        {
            BossMonsterHpPool(+1);
            monsterHp = bossHps.Last.Value;
        }
        return monsterHp;
    }
}
