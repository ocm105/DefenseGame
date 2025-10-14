using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] GameObject monsterGroup;
    [SerializeField] PathInfo monsterPathInfo;
    private int maxMonsterCreateCount = 60;                         // 몬스터 최고 생성 갯수
    private int maxMonsterSpawnCount = 10;                          // 라운드 스폰 갯수
    private int nowMonsterSpawnCount = 0;                           // 현재 스폰 갯수
    private float monsterSpawnTime = 0.5f;                 // 몬스터 스폰 시간
    private Queue<GameObject> monsterPool = new Queue<GameObject>();

    private int stageLevel = 1;

    private GameObject MonsterResource()
    {
        return Resources.Load<GameObject>(UnitResource.GetMonster(GameDataManager.Instance.stageData[GameIndex.Stage + stageLevel].ResourceMonster));
    }

    /// <summary> 몬스터 풀링 </summary>
    private void MonsterPooling()
    {
        for (int i = 0; i < maxMonsterCreateCount; i++)
        {
            MonsterCreate();
        }
    }
    /// <summary> 몬스터 생성 </summary>
    private GameObject MonsterCreate()
    {
        GameObject obj = Instantiate(MonsterResource(), monsterGroup.transform);
        MonsterControl mc = obj.GetComponent<MonsterControl>();
        mc.MovePath(monsterPathInfo.MonsterMovePath);
        mc.dieAction = (info) => MonsterDie(info);
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
        MonsterInfo info = obj.GetComponent<MonsterInfo>();
        info.monsterData = GameDataManager.Instance.monsterData[monsterDataIndex];
        info.monsterHp = gameView.MonsterUI.SetMonsterHP();
        info.Spawn();
        obj.SetActive(true);
    }
    /// <summary> 몬스터 죽었을 때 함수 </summary>
    private void MonsterDie(MonsterInfo info)
    {
        MonsterInit(info.gameObject);
        GoldSet(info.monsterData.GOLD);
    }
}
