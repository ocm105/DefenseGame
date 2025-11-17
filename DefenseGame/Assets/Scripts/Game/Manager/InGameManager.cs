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

    private async UniTask Init()
    {
        await UniTask.WaitUntil(() => gameState == GameState.Start);
        waveTime = gameSetting.startTime;
        GoldSet(100);
        MonsterPooling();
        UnitPooling();
        gameView.WaveCountSet(1);
        gameView.UnitCountSet(nowUnitSpawnCount, gameSetting.maximumUnitCount);
    }
    private void Start()
    {
        Init().Forget();
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
                    waveTime = gameSetting.waveTime;
                    gameView.WaveCountSet(waveIndex);

                    // 최대 웨이브가 되면 종료
                    if (gameSetting.maxmumWave < waveIndex)
                        ChangeGameState(GameState.End);
                }
                if (wave)
                {
                    spawnTime -= Time.deltaTime;
                    if (spawnTime <= 0)
                    {
                        nowMonsterSpawnCount++;
                        MonsterSpawn();
                        spawnTime = gameSetting.monsterSpawnTime;
                        if (nowMonsterSpawnCount >= gameSetting.waveMonsterCount)
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
                PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("게임 끝!!");
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
