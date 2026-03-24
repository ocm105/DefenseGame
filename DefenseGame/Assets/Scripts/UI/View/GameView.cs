using Cysharp.Threading.Tasks;
using Spellbind;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : UIView
{
    private static GameView instance;
    public static GameView Instance { get { return instance; } }

    private Camera mainCam;
    public CharacterFactory characterFactory { get; private set; }

    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI waveTimeText;
    [SerializeField] TextMeshProUGUI monsterCountText;
    private int wave = 1;
    private int monsterCount = 0;

    [SerializeField] TextMeshProUGUI unitCountText;
    [SerializeField] TextMeshProUGUI goldText;
    private int unitCount = 0;

    [SerializeField] public WaringPanel waringPanel;

    [SerializeField] Transform unitSpawnUI;
    [SerializeField] TweenButton unitSpawnBtn;
    [SerializeField] Transform unitMergeUI;
    [SerializeField] TweenButton unitMergeBtn;

    [SerializeField] MonsterSpawner monsterSpawner;

    [SerializeField] Button homeBtn;

    public void Show()
    {
        instance = this;
        mainCam = Camera.main;
        characterFactory = new CharacterFactory();
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        homeBtn.onClick.AddListener(OnClick_Home);
    }
    protected override void OnShow()
    {
        StartWave().Forget();
        Set_UnitCount(0, 0);
    }

    #region Event
    public void Set_Gold(int gold)
    {
        goldText.text = gold.ToString();
    }
    public void Set_UnitCount(int add, int max)
    {
        unitCount += add;
        unitCountText.text = $"{unitCount}/{max}";
    }
    private void Set_Wave(int wave)
    {
        waveText.text = $"Wave : {wave}";
    }
    private void Set_WaveTime(float time)
    {
        int m = Mathf.FloorToInt((time % 3600) / 60f);
        int s = Mathf.FloorToInt(time % 60f);

        waveTimeText.text = $"Time : {m.ToString("00")}:{s.ToString("00")}";
    }
    private void Set_MonsterCount(int add)
    {
        monsterCount += add;
        monsterCountText.text = $"Monster : {monsterCount}";
    }
    private void OnClick_Home()
    {
        PopupState state = Les_UIManager.Instance.Popup<BasePopup_TwoBtn>().Open("게임을 종료하시겠습니까?");
        state.OnYes = p => LoadingManager.Instance.SceneLoad(Scene.LobbyScene).Forget();
    }
    #endregion

    public void Set_UnitSpawnUI(bool active, Transform trans = null)
    {
        if (unitSpawnUI.gameObject.activeSelf != active)
            unitSpawnUI.gameObject.SetActive(active);

        if (!active || trans == null) return;
        unitSpawnUI.transform.position = mainCam.WorldToScreenPoint(trans.position);
    }
    public void Set_UnitSpawnEvent(UnityAction action)
    {
        unitSpawnBtn.onClick.RemoveAllListeners();
        unitSpawnBtn.onClick.AddListener(action);
    }
    public void Set_UnitMergeUI(bool active, Transform trans = null)
    {
        if (unitMergeUI.gameObject.activeSelf != active)
            unitMergeUI.gameObject.SetActive(active);

        if (!active || trans == null) return;
        unitMergeUI.transform.position = mainCam.WorldToScreenPoint(trans.position);
    }
    public void Set_UnitMergeEvent(UnityAction action)
    {
        unitMergeBtn.onClick.RemoveAllListeners();
        unitMergeBtn.onClick.AddListener(action);
    }
    private async UniTask StartWave()
    {
        wave = 1;
        float maxTime = 60f;
        float currentTime = maxTime;
        while (UnityEditor.EditorApplication.isPlaying)
        {
            Set_WaveTime(currentTime);

            if (currentTime == maxTime)
            {
                Set_Wave(wave);
                monsterSpawner.OnSpawn();
            }

            await UniTask.WaitForSeconds(1f).SuppressCancellationThrow();
            currentTime -= 1f;
            if (currentTime <= 0f)
            {
                wave++;
                currentTime = maxTime;
            }
        }
    }
    public MonsterBase GetMonster(MonsterData data, Vector3 pos, Transform parent)
    {
        Set_MonsterCount(+1);
        return characterFactory.GetMonster(data, pos, parent);
    }
    public void ReturnMonsterFactroy(MonsterBase monster)
    {
        Set_MonsterCount(-1);
        characterFactory.ReturnMonster(monster);
    }
    public UnitBase GetUnit(UnitData data, Transform parent)
    {
        Set_UnitCount(+1, 0);
        return characterFactory.GetUnit(data, parent);
    }
    public void ReturnUnitFactroy(UnitBase unit)
    {
        Set_UnitCount(-1, 0);
        characterFactory.ReturnUnit(unit);
    }
}
