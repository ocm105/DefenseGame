using UnityEngine;
using System.Collections.Generic;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] GameObject monsterGroup;
    [SerializeField] PathInfo monsterPathInfo;
    private int nowMonsterSpawnCount = 0;                           // 현재 스폰 갯수
    private Queue<GameObject> monsterPool = new Queue<GameObject>();

    private int stageLevel = 1;

    private GameObject MonsterResource()
    {
        return Resources.Load<GameObject>(UnitResource.GetMonster(GameDataManager.Instance.stageData[GameIndex.Stage + stageLevel].ResourceMonster));
    }

    /// <summary> 몬스터 풀링 </summary>
    private void MonsterPooling()
    {
        for (int i = 0; i < gameSetting.maximumMonsterCount; i++)
        {
            MonsterCreate();
        }
    }
    /// <summary> 몬스터 생성 </summary>
    private GameObject MonsterCreate()
    {
        GameObject obj = Instantiate(MonsterResource(), monsterGroup.transform);
        Monster monster = obj.GetComponent<Monster>();
        monster.MovePath(monsterPathInfo.MonsterMovePath);
        monster.dieAction = (info) => MonsterDie(info);
        MonsterInit(obj);
        return obj;
    }
    /// <summary> 몬스터 초기화 </summary>
    private void MonsterInit(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = monsterPathInfo.MonsterCreatePoint.position;
        obj.transform.rotation = Quaternion.identity;
        monsterPool.Enqueue(obj);
    }
    /// <summary> 몬스터 스폰 </summary>
    private void MonsterSpawn()
    {
        if (monsterPool.Count <= 0)
        {
            MonsterCreate();
        }
        int monsterDataIndex = GameDataManager.Instance.waveData[GameIndex.Wave + waveIndex].Summon;
        GameObject obj = monsterPool.Dequeue();
        Monster monster = obj.GetComponent<Monster>();
        monster.monsterData = GameDataManager.Instance.monsterData[monsterDataIndex];
        monster.monsterHp = gameView.MonsterUI.SetMonsterHP();
        monster.Spawn();
        obj.SetActive(true);
    }
    /// <summary> 몬스터 죽었을 때 함수 </summary>
    private void MonsterDie(Monster monster)
    {
        MonsterInit(monster.gameObject);
        GoldSet(monster.monsterData.GOLD);
    }
}
