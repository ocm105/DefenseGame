using System.Collections.Generic;
using UnityEngine;

public class MonsterUI : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] int hpPoolCount;
    [SerializeField] GameObject monsterHPPrefab;
    public List<MonsterHp> monsterHps = new List<MonsterHp>();
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        MonsterHpPool(hpPoolCount);
    }
    private void MonsterHpPool(int count)
    {
        GameObject obj;
        MonsterHp monsterHp;
        for (int i = 0; i < count; i++)
        {
            obj = Instantiate(monsterHPPrefab, this.transform);
            monsterHp = obj.GetComponent<MonsterHp>();
            monsterHp.mainCam = mainCam;
            monsterHp.SetActive(false);
            monsterHps.Add(monsterHp);
        }
    }
    public MonsterHp SetMonsterHP()
    {
        MonsterHp monsterHp = null;
        foreach (MonsterHp hp in monsterHps)
        {
            if (!hp.IsUse)
            {
                monsterHp = hp;
                break;
            }
        }
        if (monsterHp == null)
        {
            MonsterHpPool(+1);
            monsterHp = monsterHps[monsterHps.Count - 1];
        }
        return monsterHp;
    }
}
