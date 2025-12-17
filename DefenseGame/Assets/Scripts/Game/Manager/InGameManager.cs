using UnityEngine;
using Cysharp.Threading.Tasks;
using UISystem;

public partial class InGameManager : MonoBehaviour
{
    private static InGameManager instance = null;
    public static InGameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindFirstObjectByType(typeof(InGameManager)) as InGameManager;
            return instance;
        }
    }

    [SerializeField] GameView gameView;
    [SerializeField] GameSetting gameSetting;
    private float waveTime, spawnTime = 0;
    private int waveIndex = 0;
    private bool waveStart = false;
    private bool isBossWave = false;
    private int gold = 0;

    private GameState gameState = GameState.End;
    public GameState GameState { get { return gameState; } }

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        UnitPooling();
        MonsterPooling();

        waveTime = gameSetting.startTime;
        gameView.WaveTimeSet(waveTime);
        gameView.WaveCountSet(gameSetting.startWave);
        gameView.UnitCountSet(nowUnitSpawnCount, gameSetting.maximumUnitCount);
        gameView.SetMonsterCount(monsterAriveCount);

        SpendGold(gameSetting.startGold);
        gameState = GameState.Ready;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Ready:
                waveTime -= Time.deltaTime;
                gameView.WaveTimeSet(waveTime);
                if (waveTime <= 0)
                {
                    gameState = GameState.Start;
                    NextWave();
                }
                break;

            case GameState.Start:
                waveTime -= Time.deltaTime;
                gameView.WaveTimeSet(waveTime);
                if (waveTime <= 0)
                {
                    if (gameSetting.IsMaxmumWave(waveIndex))
                    {
                        ChangeGameState(GameState.End);
                        return;
                    }
                    NextWave();
                }
                if (waveTime > 0 && IsMonsterArive == false && waveStart == false)
                {
                    NextWave();
                }
                if (waveStart)
                {
                    if (isBossWave)
                    {
                        // 보스 스폰
                        waveStart = false;
                    }
                    else
                    {
                        spawnTime -= Time.deltaTime;
                        if (spawnTime <= 0)
                        {
                            MonsterSpawn();
                            spawnTime = gameSetting.monsterSpawnTime;
                            if (nowMonsterSpawnCount >= gameSetting.waveMonsterCount)
                            {
                                nowMonsterSpawnCount = 0;
                                waveStart = false;
                            }
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
    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Ready:
                break;
            case GameState.Start:
                break;
            case GameState.Pause:
                break;
            case GameState.End:
                PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("게임 끝");
                break;
        }
        gameState = state;
    }
    public void SpendGold(int _gold)
    {
        gold += _gold;
        gameView.GoldSet(gold);
    }

    private void NextWave()
    {
        waveIndex++;
        gameView.WaveCountSet(waveIndex);

        isBossWave = gameSetting.IsBossWave(waveIndex);
        waveTime = gameSetting.WaveTime(waveIndex);

        waveStart = true;
    }
    #endregion

}
