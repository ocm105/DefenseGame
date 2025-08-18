using System;
using UnityEngine;
using UISystem;
using System.Collections;
using System.Collections.Generic;



[Serializable]
public class WaveData
{
    public int Index;
    public int Summon;
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string WAVE_DATA_PATH = "https://docs.google.com/spreadsheets/d/1Rhn07hnpBnHJQXAY2YIJRovY52ooUBfpJFE6WTL7Jd0/export?format=csv";

    public IEnumerator GetWaveDataRequest(Action<Dictionary<int, WaveData>> callback = null)
    {
        yield return StartCoroutine(Request_Get(WAVE_DATA_PATH, (dataState, resData) =>
        {
            switch (dataState)
            {
                case GAMEDATA_STATE.CONNECTDATAERROR:
                case GAMEDATA_STATE.PROTOCOLERROR:
                    // PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("데이터를 받아오지 못했습니다.");
                    // popup.OnClose = p => Application.Quit();
                    // popup.OnOK = p => Application.Quit();
                    break;
                case GAMEDATA_STATE.REQUESTSUCCESS:
                    callback?.Invoke(CSVReader.ReadFromResource<WaveData>(resData));
                    break;
            }
        }));
    }
}

