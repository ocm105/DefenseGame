using UnityEngine;
using UISystem;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class SynergyPopup : UIPopup
{
    private const string Synergy = "Synergy";
    [SerializeField] GameObject frame;
    [SerializeField] Transform contentPos;

    private Synergy[] synergys;
    private Dictionary<SynergyType, int> SynergyDic = new Dictionary<SynergyType, int>();

    public PopupState Open(Dictionary<SynergyType, int> dic)
    {
        ShowLayer();
        SynergyDic = dic;
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
        SetSynergy(SynergyDic);
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

    private void SetSynergy(Dictionary<SynergyType, int> dic)
    {
        SynergyType type;
        for (int i = 0; i < dic.Count; i++)
        {
            type = (SynergyType)i;
            synergys[i].SetInfo(type, dic[type]);
            if (dic[type] > 0)
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
