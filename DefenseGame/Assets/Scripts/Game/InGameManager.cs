using UnityEngine;
using System;
using System.Collections.Generic;

public class InGameManager : MonoBehaviour
{
    private GameState gameState;
    [SerializeField] float roundTime = 20f;                         // 라운드 시간

    [SerializeField] GameObject monsterGroup;
    [SerializeField] Transform monsterCreatePoint;
    [SerializeField] Transform[] monsterMovePath;                   // 몬스터 이동 경로
    [Range(1, 100)][SerializeField] int maxCreateCount;             // 몬스터 최고 생성 갯수
    [Range(1, 30)][SerializeField] int maxSpwanCount;               // 라운드 스폰 갯수
    private int nowSpwanCount = 0;                                  // 현재 스폰 갯수
    [SerializeField] float monsterSpawnTime = 0.5f;                 // 몬스터 스폰 시간
    private float gameTime, spwanTime = 0;
    private Queue<GameObject> monsterPool = new Queue<GameObject>();

    private void Awake()
    {

    }
    private void Init()
    {
        gameState = GameState.Start;
        MonsterPooling();
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                gameTime += Time.deltaTime;
                if (gameTime >= roundTime)
                {
                    spwanTime += Time.deltaTime;
                    if (spwanTime >= monsterSpawnTime)
                    {
                        nowSpwanCount++;
                        MonsterSpawn();
                        spwanTime = 0;
                        if (nowSpwanCount >= maxCreateCount) gameTime = 0;
                    }
                }
                break;
            case GameState.Pause:
                break;
            case GameState.End:
                break;
        }
    }

    #region Fuction
    /// <summary> 게임 상태 변경 </summary>
    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.Pause:
                break;
            case GameState.End:
                break;
        }
        gameState = state;
    }
    #endregion

    #region Monster
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
    private GameObject MonsterCreate()
    {
        string path = string.Concat(Constants.Character.Monster, '/', "Monster");
        return Instantiate(Resources.Load<GameObject>(path), monsterGroup.transform);
    }
    private void MonsterInit(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = monsterCreatePoint.position;
        obj.transform.rotation = Quaternion.identity;
    }
    private void MonsterSpawn()
    {
        if (monsterPool.Count <= 0)
        {
            GameObject obj = MonsterCreate();
            MonsterInit(obj);
            monsterPool.Enqueue(obj);
        }
        monsterPool.Dequeue().SetActive(true);
    }
    #endregion
}
