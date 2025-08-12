using UnityEngine;
using System;
using System.Collections.Generic;

public class InGameManager : MonoBehaviour
{
    private GameState gameState;
    [SerializeField] float roundTime = 20f;                         // 라운드 시간

    #region Monster
    [SerializeField] GameObject monsterGroup;
    [SerializeField] Transform monsterCreatePoint;
    [SerializeField] Transform[] monsterMovePath;                   // 몬스터 이동 경로
    [Range(1, 100)][SerializeField] int maxCreateCount;             // 몬스터 최고 생성 갯수
    [Range(1, 30)][SerializeField] int maxSpwanCount;               // 라운드 스폰 갯수
    private int nowSpwanCount = 0;                                  // 현재 스폰 갯수
    [SerializeField] float monsterSpawnTime = 0.5f;                 // 몬스터 스폰 시간
    private float gameTime, spwanTime = 0;
    private Queue<GameObject> monsterPool = new Queue<GameObject>();
    #endregion

    private UnitControl characterControl;

    private void Awake()
    {

    }
    private void Init()
    {
        gameState = GameState.Start;
        MonsterPooling();
        gameTime = 19;
    }
    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickEvent();
        }
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
                        if (nowSpwanCount >= maxSpwanCount)
                        {
                            nowSpwanCount = 0;
                            gameTime = 0;
                        }
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

    private void ClickEvent()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                characterControl = hit.collider.GetComponent<UnitControl>();
                characterControl.OnClick(true);
            }
        }
        else
        {
            if (characterControl != null)
            {
                characterControl.OnClick(false);
                characterControl = null;
            }
        }
    }
    #endregion

    #region Monster
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
        obj.transform.position = monsterCreatePoint.position;
        obj.transform.rotation = Quaternion.identity;
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
    #endregion

    #region Player

    #endregion
}
