using UISystem;
using UnityEngine;
using UnityEngine.UI;

public class GameView : UIView
{
    [SerializeField] InGameManager gameManager;
    [SerializeField] Button unitSpawnBtn;
    [SerializeField] Button homeBtn;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        unitSpawnBtn.onClick.AddListener(OnClick_UnitSpawn);
    }
    protected override void OnShow()
    {
        Init();
    }

    private void Init()
    {
    }

    #region Event
    private void OnClick_UnitSpawn()
    {
        gameManager.UnitSpawn();
    }

    private void OnClick_HomeBtn()
    {
        // SoundManager.Instance.PlaySFXSound("Button");
        LoadingManager.Instance.SceneLoad(Constants.Scene.Title);
    }
    #endregion
}
