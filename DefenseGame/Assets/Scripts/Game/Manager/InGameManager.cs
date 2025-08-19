using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] GameView gameView;
    [SerializeField] float roundTime = 20f;                         // 라운드 시간
    private float waveTime, spawnTime = 0;
    private int waveIndex = 0;
    private bool wave = false;
    private int gold = 0;

    private GameState gameState;
    public GameState GameState { get { return gameState; } }

    private void Init()
    {
        waveTime = 3;
        GoldSet(100);
        MonsterPooling();
        UnitPooling();
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
                waveTime -= Time.deltaTime;
                gameView.WaveTimeSet(waveTime);
                if (waveTime <= 0)
                {
                    waveIndex++;
                    wave = true;
                    waveTime = roundTime;
                }
                if (wave)
                {
                    spawnTime -= Time.deltaTime;
                    if (spawnTime <= 0)
                    {
                        nowMonsterSpawnCount++;
                        MonsterSpawn();
                        spawnTime = monsterSpawnTime;
                        if (nowMonsterSpawnCount >= maxMonsterSpawnCount)
                        {
                            nowMonsterSpawnCount = 0;
                            wave = false;
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
