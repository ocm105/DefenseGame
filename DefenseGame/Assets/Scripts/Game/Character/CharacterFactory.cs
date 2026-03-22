using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory
{
    private Queue<UnitBase> unitPool = new Queue<UnitBase>();
    private Queue<MonsterBase> monsterPool = new Queue<MonsterBase>();

    public UnitBase GetUnit(UnitData rawData, Transform parent)
    {
        UnitBase unit = null;

        if (unitPool.Count > 0)
            unit = unitPool.Dequeue();
        else
            unit = CreateUnitMono();

        unit.transform.parent = parent;
        unit.transform.localPosition = Vector3.zero;
        unit.Init(rawData);

        return unit;
    }
    public MonsterBase GetMonster(MonsterData rawData, Transform parent)
    {
        MonsterBase monster = null;

        if (monsterPool.Count > 0)
            monster = monsterPool.Dequeue();
        else
            monster = CreateMonsterMono();

        monster.transform.parent = parent;
        monster.transform.localPosition = Vector3.zero;
        monster.Init(rawData);

        return monster;
    }
    public void ReturnUnit(UnitBase unit)
    {
        unit.gameObject.SetActive(false);
        unitPool.Enqueue(unit);
    }
    public void ReturnMonster(MonsterBase monster)
    {
        monster.gameObject.SetActive(false);
        monsterPool.Enqueue(monster);
    }

    private UnitBase CreateUnitMono()
    {
        var prefab = Resources.Load<GameObject>(Character.UnitMono);
        if (prefab == null)
        {
            Debug.LogError($"[CharacterFactory] {Character.UnitMono} 프리팹을 찾을 수 없습니다.");
            return null;
        }
        return GameObject.Instantiate(prefab).GetComponent<UnitBase>();
    }
    private MonsterBase CreateMonsterMono()
    {
        var prefab = Resources.Load<GameObject>(Character.MonsterMono);
        if (prefab == null)
        {
            Debug.LogError($"[CharacterFactory] {Character.MonsterMono} 프리팹을 찾을 수 없습니다.");
            return null;
        }
        return GameObject.Instantiate(prefab).GetComponent<MonsterBase>();
    }
}
