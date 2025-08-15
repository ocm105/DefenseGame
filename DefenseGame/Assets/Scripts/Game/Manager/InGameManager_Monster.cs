using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    private string monsterSource = Constants.Character.Monster + "/Monster";
    [SerializeField] GameObject monsterGroup;
    [SerializeField] PathInfo monsterPathInfo;
    private int maxMonsterCreateCount = 60;                         // 몬스터 최고 생성 갯수
    private int maxMonsterSpawnCount = 10;                          // 라운드 스폰 갯수
    private int nowMonsterSpawnCount = 0;                           // 현재 스폰 갯수
    [SerializeField] float monsterSpawnTime = 0.5f;                 // 몬스터 스폰 시간
    private Queue<GameObject> monsterPool = new Queue<GameObject>();


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
        GameObject obj = Instantiate(Resources.Load<GameObject>(monsterSource), monsterGroup.transform);
        MonsterControl mc = obj.GetComponent<MonsterControl>();
        mc.MovePath(monsterPathInfo.MonsterMovePath);
        mc.dieAction = (ob) => MonsterInit(ob);
        MonsterInit(obj);
        return obj;
    }
    /// <summary> 몬스터 초기화 </summary>
    private void MonsterInit(GameObject obj)
    {
        obj.SetActive(false);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = monsterPathInfo.MonsterCreatePoint.anchoredPosition;
        rect.rotation = Quaternion.identity;
        monsterPool.Enqueue(obj);
    }
    /// <summary> 몬스터 스폰 </summary>
    private void MonsterSpawn()
    {
        if (monsterPool.Count <= 0)
        {
            MonsterCreate();
        }
        GameObject obj = monsterPool.Dequeue();
        MonsterControl mc = obj.GetComponent<MonsterControl>();
        obj.SetActive(true);
        mc.MonsterStart();
    }
}
