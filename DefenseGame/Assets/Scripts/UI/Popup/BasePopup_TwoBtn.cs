using LitMotion;
using LitMotion.Extensions;
using System;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

public class BasePopup_TwoBtn : BasePopup_Toast
{
    [SerializeField] Button okButton;
    [SerializeField] Button noButton;
    [SerializeField] Button exitButton;

    protected override void OnFirstShow()
    {
        okButton.onClick.AddListener(OnClick_OkBtn);
        noButton.onClick.AddListener(OnClick_NoBtn);
        exitButton.onClick.AddListener(OnClick_NoBtn);
    }
    protected override void OnShow()
    {
        ShowTween();
    }

    private void OnClick_OkBtn()
    {
        // SoundManager.Instance.PlaySFXSound("Button");
        CloseTween(() => OnResult(PopupResults.Yes));
    }
    private void OnClick_NoBtn()
    {
        // SoundManager.Instance.PlaySFXSound("Button");
        CloseTween(() => OnResult(PopupResults.No));
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
