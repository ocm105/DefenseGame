using System;
using UISystem;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class WaveData
{
    public int Index;
    public string MonsterID;
    public float Speed;
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    private string WAVE_DATA_PATH = "https://docs.google.com/spreadsheets/d/1Rhn07hnpBnHJQXAY2YIJRovY52ooUBfpJFE6WTL7Jd0/export?format=csv";

    public async UniTask<List<WaveData>> GetWaveData()
    {
        var request = await Request_Get(WAVE_DATA_PATH);
        switch (request.state)
        {
            case GAMEDATA_STATE.CONNECTDATAERROR:
            case GAMEDATA_STATE.PROTOCOLERROR:
                PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("Loaf WAVE_DATA Fail");
                popup.OnClose = p => Application.Quit();
                popup.OnOK = p => Application.Quit();
                return null;

            case GAMEDATA_STATE.REQUESTSUCCESS:

                List<WaveData> data = new();
                var items = CSVReader.ReadFromResource<WaveData>(request.data);

                foreach (var item in items)
                {
                    data.Add(item.Value);
                }

                Debug.Log("WAVE_DATA Load");
                return data;

            default:
                return null;
        }
    }
}

