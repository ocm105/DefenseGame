using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

public class GameView : UIView
{
    [SerializeField] InGameManager gameManager;
    [SerializeField] Button unitSpawnBtn;
    [SerializeField] Button homeBtn;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI waveTimeText;

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
    public void GoldSet(int gold)
    {
        goldText.text = gold.ToString();
    }
    public void WaveTimeSet(float time)
    {
        waveTimeText.text = Mathf.CeilToInt(time).ToString();
    }
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
