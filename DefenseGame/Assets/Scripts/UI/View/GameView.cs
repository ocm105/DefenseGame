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

    [SerializeField] UnitInfoWindow unitInfoWindow;
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
    public void UnitStatusActive(bool isActive, UnitData data = null)
    {
        unitInfoWindow.SetActive(isActive, data);
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
        InGameManager.Instance.ChangeGameState(GameState.Pause);
        PopupState state = Les_UIManager.Instance.Popup<BasePopup_TwoBtn>().Open("게임을 종료하시겠습니까?");
        state.OnYes = p => LoadingManager.Instance.SceneLoad(Scene.LobbyScene).Forget();
        state.OnNo = p => InGameManager.Instance.ChangeGameState(GameState.Start);
         
    }
    #endregion
}
