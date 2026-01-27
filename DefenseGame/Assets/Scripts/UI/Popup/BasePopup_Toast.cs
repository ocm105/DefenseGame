using UnityEngine;
using UISystem;
using TMPro;
using LitMotion;
using LitMotion.Extensions;

public class BasePopup_Toast : UIPopup
{
    [SerializeField] protected GameObject frame;
    [SerializeField] TextMeshProUGUI messageText;

    public PopupState Open(string msg)
    {
        ShowLayer();
        SetMessage(msg);
        return state;
    }
    protected override void OnFirstShow()
    {
    }
    protected override void OnShow()
    {
        Toast_Tween();
    }

    protected virtual void SetMessage(string msg)
    {
        // messageText.text = LocalizationManager.Instance.GetLocalizeText(msg);
        messageText.text = msg;
    }

    private void Toast_Tween()
    {
        LSequence.Create()
                 .Append(LMotion.Create(Vector3.zero, Vector3.one, 0.5f)
                                .WithEase(Ease.OutCubic)
                                .BindToLocalScale(frame.transform))
                 .AppendInterval(2f)
                 .Append(LMotion.Create(Vector3.one, Vector3.zero, 0.5f)
                                .WithEase(Ease.OutCubic)
                                .WithOnComplete(() => OnResult(PopupResults.Close))
                                .BindToLocalScale(frame.transform))
                 .Run()
                 .AddTo(this);
    }
}
