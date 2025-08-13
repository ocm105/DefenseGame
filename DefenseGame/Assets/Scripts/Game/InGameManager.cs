using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    private GameState gameState;
    [SerializeField] float roundTime = 20f;                         // 라운드 시간

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
    #endregion

}
