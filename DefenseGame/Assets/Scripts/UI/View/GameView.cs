using System.Collections;
using Cysharp.Threading.Tasks;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

public class GameView : UIView
{
    [SerializeField] InGameManager gameManager;

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
        gameManager.ChangeGameState(GameState.Start);

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
    public void UnitStatusOpen(Sprite unit, float power, float atkSpeed)
    {
        unitImage.sprite = unit;
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
            Les_UIManager.Instance.Popup<SynergyPopup>().Open(gameManager.SynergyDic);
        }
        else
        {
            Les_UIManager.Instance.Popup<SynergyPopup>().Close();
        }

        isSynergyOn = !isSynergyOn;
    }
    private void OnClick_UnitSpawn()
    {
        gameManager.UnitSpawn();
    }

    private void OnClick_Home()
    {
        // SoundManager.Instance.PlaySFXSound("Button");
        PopupState state = Les_UIManager.Instance.Popup<BasePopup_TwoBtn>().Open("게임을 나가시겠습니까?");
        gameManager.ChangeGameState(GameState.Pause);
        state.OnYes = p => gameManager.ChangeGameState(GameState.Start);
        state.OnNo = p => gameManager.ChangeGameState(GameState.Start);
        // LoadingManager.Instance.SceneLoad(Constants.Scene.Title);
    }
    #endregion
}
