using Cysharp.Threading.Tasks;
using Gpm.Ui;
using UISystem;
using UnityEngine;
using UnityEngine.UI;



public class LobbyView : UIView
{
    [SerializeField] Button gameStartBtn;
    [SerializeField] TabController tabController;
    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        gameStartBtn.onClick.AddListener(OnClick_GameStart);
    }
    protected override void OnShow()
    {
        Init();
    }

    private void Init()
    {
        var tab = tabController.GetTab((int)LobbyState.Home);
        tabController.Select(tab);
    }

    private void OnClick_GameStart()
    {
        LoadingManager.Instance.SceneLoad(Scene.GameScene).Forget();
    }
}
