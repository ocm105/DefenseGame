using Cysharp.Threading.Tasks;
using Spellbind;
using System.Linq;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : UIView
{
    [SerializeField] Button homeBtn;

    [SerializeField] TextMeshProUGUI waveCountText;
    [SerializeField] TextMeshProUGUI waveTimeText;
    [SerializeField] TextMeshProUGUI monsterCountText;

    [SerializeField] UnitInfoWindow unitInfoWindow;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI unitCountText;

    [SerializeField] public WaringPanel waringPanel;

    [SerializeField] Transform unitSpawnUI;
    [SerializeField] TweenButton unitSpawnBtn;
    [SerializeField] Transform unitMergeUI;
    [SerializeField] TweenButton unitMergeBtn;

    private Camera mainCam;
    public CharacterFactory characterFactory { get; private set; }

    private void Awake()
    {
        mainCam = Camera.main;
        characterFactory = new CharacterFactory();
    }
    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        homeBtn.onClick.AddListener(OnClick_Home);
    }
    protected override void OnShow() { }

    #region Event
    public void GoldSet(int gold)
    {
        goldText.text = gold.ToString();
    }
    public void UnitCountSet(int now, int max)
    {
        unitCountText.text = $"{now}/{max}";
    }
    public void SetMonsterCount(int count)
    {
        monsterCountText.text = $"Monster Count : {count}";
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
}
