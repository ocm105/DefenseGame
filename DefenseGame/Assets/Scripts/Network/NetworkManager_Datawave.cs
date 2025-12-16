using System;
using UISystem;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class WaveData
{
    public int Index;
    public int Summon;
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string WAVE_DATA_PATH = "https://docs.google.com/spreadsheets/d/1Rhn07hnpBnHJQXAY2YIJRovY52ooUBfpJFE6WTL7Jd0/export?format=csv";

    public async UniTask GetWaveDataRequest(Action<Dictionary<int, WaveData>> callback = null)
    {
        await Request_Get(WAVE_DATA_PATH, (dataState, resData) =>
        {
            switch (dataState)
            {
                case GAMEDATA_STATE.CONNECTDATAERROR:
                case GAMEDATA_STATE.PROTOCOLERROR:
                    PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("Loaf WAVE_DATA Fail");
                    popup.OnClose = p => Application.Quit();
                    popup.OnOK = p => Application.Quit();
                    break;
                case GAMEDATA_STATE.REQUESTSUCCESS:
                    Debug.Log("WAVE_DATA Load");
                    callback?.Invoke(CSVReader.ReadFromResource<WaveData>(resData));
                    break;
            }
        });
    }
}

