using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] GameObject monsterGroup;
    [SerializeField] RectTransform monsterCreatePoint;
    [SerializeField] RectTransform[] monsterMovePath;                   // 몬스터 이동 경로
    [Range(1, 100)][SerializeField] int maxCreateCount;             // 몬스터 최고 생성 갯수
    [Range(1, 30)][SerializeField] int maxSpwanCount;               // 라운드 스폰 갯수
    private int nowSpwanCount = 0;                                  // 현재 스폰 갯수
    [SerializeField] float monsterSpawnTime = 0.5f;                 // 몬스터 스폰 시간
    private float gameTime, spwanTime = 0;
    private Queue<GameObject> monsterPool = new Queue<GameObject>();


    /// <summary> 몬스터 풀링 </summary>
    private void MonsterPooling()
    {
        GameObject obj;
        for (int i = 0; i < maxCreateCount; i++)
        {
            obj = MonsterCreate();
            MonsterInit(obj);
            monsterPool.Enqueue(obj);
        }
    }
    /// <summary> 몬스터 생성 </summary>
    private GameObject MonsterCreate()
    {
        string path = string.Concat(Constants.Character.Monster, '/', "Monster");
        GameObject obj = Instantiate(Resources.Load<GameObject>(path), monsterGroup.transform);
        MonsterControl mc = obj.GetComponent<MonsterControl>();
        mc.MovePath(monsterMovePath);
        mc.dieAction = (ob) => MonsterInit(ob);
        return obj;
    }
    /// <summary> 몬스터 초기화 </summary>
    private void MonsterInit(GameObject obj)
    {
        obj.SetActive(false);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = monsterCreatePoint.anchoredPosition;
        rect.rotation = Quaternion.identity;
    }
    /// <summary> 몬스터 스폰 </summary>
    private void MonsterSpawn()
    {
        if (monsterPool.Count <= 0)
        {
            GameObject obj = MonsterCreate();
            MonsterInit(obj);
            monsterPool.Enqueue(obj);
        }
        GameObject obj2 = monsterPool.Dequeue();
        MonsterControl mc = obj2.GetComponent<MonsterControl>();
        obj2.SetActive(true);
        mc.MonsterStart();
    }
}
