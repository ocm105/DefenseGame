using System;
using UnityEngine;
using UISystem;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

[Serializable]
public class SynergyData
{
    public int Index;
    public SynergyType Type;
    public int Required;
    public string ResourceIcon;
    public List<string> PassiveSynergy;

    public PassiveSynergy[] passiveSynergies;

    public void SetPassiveSynergy(List<string> strs)
    {
        string[] arr = new string[strs.Count];
        passiveSynergies = new PassiveSynergy[strs.Count];
        for (int i = 0; i < strs.Count; i++)
        {
            passiveSynergies[i] = new PassiveSynergy();
            arr = strs[i].Split('|');
            passiveSynergies[i].value1 = arr[0];
            passiveSynergies[i].value2 = arr[1];
        }
    }
}

public class PassiveSynergy
{
    public string value1;
    public string value2;
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string SYNERGY_DATA_PATH = "https://docs.google.com/spreadsheets/d/1uokxUKHiaea26gGqmH-cfrStkVSW2xdyOcb_j_U4I5c/export?format=csv";

    public async UniTask GetSynergyDataRequest(Action<Dictionary<int, SynergyData>> callback = null)
    {
        await Request_Get(SYNERGY_DATA_PATH, (dataState, resData) =>
        {
            switch (dataState)
            {
                case GAMEDATA_STATE.CONNECTDATAERROR:
                case GAMEDATA_STATE.PROTOCOLERROR:
                    PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("데이터를 받아오지 못했습니다.");
                    popup.OnClose = p => Application.Quit();
                    popup.OnOK = p => Application.Quit();
                    break;
                case GAMEDATA_STATE.REQUESTSUCCESS:
                    callback?.Invoke(CSVReader.ReadFromResource<SynergyData>(resData));
                    break;
            }
        });
    }
}

