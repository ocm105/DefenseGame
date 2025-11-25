using Cysharp.Threading.Tasks;
using UISystem;
using UnityEngine;

public class TitleView : UIView
{
    public void Show()
    {
        ShowLayer();
    }
    protected override void OnFirstShow() { }
    protected override void OnShow()
    {
        GameStart().Forget();
    }

    private async UniTaskVoid GameStart()
    {
        await GameDataManager.Instance.LoadData();
        Debug.Log("DataLoad 완료");
        await LoadingManager.Instance.SceneLoad(Scene.LobbyScene);
    }
}
