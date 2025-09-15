using UnityEngine;
using UISystem;
using System;
using DG.Tweening;
using System.Collections.Generic;



public class SynergyPopup : UIPopup
{
    private const string Synergy = "Prefabs/Synergy";
    [SerializeField] GameObject frame;
    [SerializeField] Transform contentPos;

    private Synergy[] synergys;

    public PopupState Open(int[] infos)
    {
        ShowLayer();
        SetSynergy(infos);
        return state;
    }
    public override void Close()
    {
        CloseTween(() => OnResult(PopupResults.Close));
    }
    protected override void OnFirstShow()
    {
        CreateSynergyPrefab();

    }
    protected override void OnShow()
    {
        ShowTween();
    }

    private void CreateSynergyPrefab()
    {
        synergys = new Synergy[(int)SynergyType.Max];

        for (int i = 0; i < synergys.Length; i++)
        {
            synergys[i] = Instantiate(Resources.Load<GameObject>(Synergy), contentPos).GetComponent<Synergy>();
            synergys[i].gameObject.SetActive(false);
        }
    }

    private void SetSynergy(int[] infos)
    {
        SynergyType type;
        for (int i = 0; i < infos.Length; i++)
        {
            type = (SynergyType)i;
            synergys[i].SetInfo(type, infos[i]);
            if (infos[i] > 0)
                synergys[i].gameObject.SetActive(true);
        }
    }

    private void ShowTween()
    {
        frame.transform.localScale = Vector3.zero;
        frame.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutCubic);
    }
    private void CloseTween(Action call)
    {
        frame.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutCubic).OnComplete(call.Invoke);
    }
}
