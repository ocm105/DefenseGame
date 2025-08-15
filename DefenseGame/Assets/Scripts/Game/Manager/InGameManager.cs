using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] GameView gameView;
    [SerializeField] float roundTime = 20f;                         // 라운드 시간
    private float gameTime, spawnTime = 0;
    private int gold = 0;

    private GameState gameState;

    private void Init()
    {
        gameTime = 19;
        GoldSet(100);
        MonsterPooling();
        UnitPooling();
        ChangeGameState(GameState.Start);
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
                    spawnTime += Time.deltaTime;
                    if (spawnTime >= monsterSpawnTime)
                    {
                        nowMonsterSpawnCount++;
                        MonsterSpawn();
                        spawnTime = 0;
                        if (nowMonsterSpawnCount >= maxMonsterSpawnCount)
                        {
                            nowMonsterSpawnCount = 0;
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
        UnitClickEvent();
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
    private void GoldSet(int _gold)
    {
        gold += _gold;
        gameView.GoldSet(gold);
    }
    #endregion

}
