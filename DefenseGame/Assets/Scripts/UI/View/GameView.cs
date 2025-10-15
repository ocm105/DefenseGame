using System.Collections;
using Cysharp.Threading.Tasks;
using TMPro;
using UISystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameView : UIView
{
    [SerializeField] Button unitSpawnBtn;
    [SerializeField] Button synergyBtn;
    private bool isSynergyOn = false;
    [SerializeField] Button homeBtn;

    [SerializeField] TextMeshProUGUI waveCountText;
    [SerializeField] TextMeshProUGUI waveTimeText;

    [SerializeField] GameObject unitStatWindow;
    [SerializeField] Image unitImage;
    [SerializeField] TextMeshProUGUI unitPowerText;
    [SerializeField] TextMeshProUGUI unitAtkSpeedText;

    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI unitCountText;

    [SerializeField] UnitUI unitUI;
    public UnitUI UnitUI { get { return unitUI; } }
    [SerializeField] MonsterUI monsterUI;
    public MonsterUI MonsterUI { get { return monsterUI; } }

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        synergyBtn.onClick.AddListener(OnClick_Synergy);
        unitSpawnBtn.onClick.AddListener(OnClick_UnitSpawn);
        homeBtn.onClick.AddListener(OnClick_Home);
    }
    protected override void OnShow()
    {
        Init();
    }

    private void Init()
    {
        DataLoad().Forget();
    }

    private async UniTaskVoid DataLoad()
    {
        await GameDataManager.Instance.LoadData();
        InGameManager.Instance.ChangeGameState(GameState.Start);

    }
    #region Event
    public void GoldSet(int gold)
    {
        goldText.text = gold.ToString();
    }
    public void UnitCountSet(int now, int max)
    {
        unitCountText.text = $"{now}/{max}";
    }
    public void WaveCountSet(int count)
    {
        waveCountText.text = count.ToString();
    }
    public void WaveTimeSet(float time)
    {
        waveTimeText.text = Mathf.CeilToInt(time).ToString();
    }
    public void UnitStatusOpen(float power, float atkSpeed)
    {
        unitPowerText.text = power.ToString();
        unitAtkSpeedText.text = atkSpeed.ToString();
        unitStatWindow.SetActive(true);
    }
    public void UnitStatusClose()
    {
        unitStatWindow.SetActive(false);
    }
    private void OnClick_Synergy()
    {
        if (!isSynergyOn)
        {
            Les_UIManager.Instance.Popup<SynergyPopup>().Open(InGameManager.Instance.SynergyInfos);
        }
        else
        {
            Les_UIManager.Instance.Popup<SynergyPopup>().Close();
        }

        isSynergyOn = !isSynergyOn;
    }
    private void OnClick_UnitSpawn()
    {
        InGameManager.Instance.UnitSpawn();
    }

    private void OnClick_Home()
    {
        // SoundManager.Instance.PlaySFXSound("Button");
        PopupState state = Les_UIManager.Instance.Popup<BasePopup_TwoBtn>().Open("게임을 나가시겠습니까?");
        InGameManager.Instance.ChangeGameState(GameState.Pause);
        state.OnYes = p => InGameManager.Instance.ChangeGameState(GameState.Start);
        state.OnNo = p => InGameManager.Instance.ChangeGameState(GameState.Start);
        // LoadingManager.Instance.SceneLoad(Constants.Scene.Title);
    }
    #endregion
}
