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
    private bool wave = false;
    private int gold = 0;

    private GameState gameState = GameState.End;
    public GameState GameState { get { return gameState; } }
    
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        waveTime = gameSetting.startTime;
        gameView.WaveTimeSet(waveTime);
        gameView.WaveCountSet(1);
        gameView.UnitCountSet(nowUnitSpawnCount, gameSetting.maximumUnitCount);
        gameView.SetMonsterCount(monsterAriveCount);

        UnitPooling();
        MonsterPooling();
        SpendGold(100);
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
                    if (gameSetting.maxmumWave <= waveIndex)
                    {
                        ChangeGameState(GameState.End);
                        return;
                    }
                    NextWave();
                }
                if(waveTime > 0 && IsMonsterArive == false && wave == false)
                {
                    NextWave();
                }
                if (wave)
                {
                    spawnTime -= Time.deltaTime;
                    if (spawnTime <= 0)
                    {
                        nowMonsterSpawnCount++;
                        MonsterSpawn();
                        spawnTime = gameSetting.monsterSpawnTime;
                        if (nowMonsterSpawnCount >= (waveIndex % 5 == 0 ? 1 : gameSetting.waveMonsterCount))
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
                PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("∞‘¿” ≥°");
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
        waveTime = waveIndex % 5 == 0 ? gameSetting.bossTime : gameSetting.waveTime;
        wave = true;
    }
    #endregion

}
