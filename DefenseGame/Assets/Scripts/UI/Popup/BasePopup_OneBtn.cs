using UnityEngine;
using UISystem;
using UnityEngine.UI;
using System;
using LitMotion;
using LitMotion.Extensions;

public class BasePopup_OneBtn : BasePopup_Toast
{
    [SerializeField] Button okButton;

    protected override void OnFirstShow()
    {
        okButton.onClick.AddListener(OnClick_OkBtn);
    }
    protected override void OnShow()
    {
        ShowTween();
    }

    private void OnClick_OkBtn()
    {
        // SoundManager.Instance.PlaySFXSound("Button");
        CloseTween(() => OnResult(PopupResults.OK));
    }

    private void ShowTween()
    {
        LMotion.Create(Vector3.zero, Vector3.one, 0.5f)
               .WithEase(Ease.OutCubic)
               .BindToLocalScale(frame.transform)
               .AddTo(this);
    }
    private void CloseTween(Action call)
    {
        LMotion.Create(Vector3.one, Vector3.zero, 0.5f)
               .WithEase(Ease.OutCubic)
               .WithOnComplete(() => call?.Invoke())
               .BindToLocalScale(frame.transform)
               .AddTo(this);
    }
}
