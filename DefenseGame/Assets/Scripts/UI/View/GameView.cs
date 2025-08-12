using UISystem;
using UnityEngine;
using UnityEngine.UI;

public class GameView : UIView
{
    [SerializeField] Button homeBtn;

    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow()
    {
        // homeBtn.onClick.AddListener(OnClick_HomeBtn);
    }
    protected override void OnShow()
    {
        Init();
    }

    private void Init()
    {
    }

    #region Event
    private void OnClick_AttackBtn()
    {
        // SoundManager.Instance.PlaySFXSound("Button");
    }
    private void OnClick_HomeBtn()
    {
        // SoundManager.Instance.PlaySFXSound("Button");
        LoadingManager.Instance.SceneLoad(Constants.Scene.Title);
    }
    #endregion
}
