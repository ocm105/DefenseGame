using System;
using UnityEngine;
using UISystem;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class MonsterData
{
    public string index;
    // public int next_index;
    // public string descript_key;
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string MONSTER_DATA_PATH = "https://docs.google.com/spreadsheets/d/1MWeE9xWdgDAYwJJNh1XTF00yMPXmDJvKaal0eMfg0hw/export?format=csv";

    public IEnumerator GetDescriptRequest(Action<Dictionary<int, MonsterData>> callback = null)
    {
        yield return StartCoroutine(Request_Get(MONSTER_DATA_PATH, (dataState, resData) =>
        {
            switch (dataState)
            {
                case GAMEDATA_STATE.CONNECTDATAERROR:
                case GAMEDATA_STATE.PROTOCOLERROR:
                    // PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("데이터를 받아오지 못했습니다.");
                    // popup.OnClose = p => Application.Quit();
                    // popup.OnOK = p => Application.Quit();
                    Debug.Log("ERROR");
                    break;
                case GAMEDATA_STATE.REQUESTSUCCESS:
                    Debug.Log("OK");
                    callback?.Invoke(CSVReader.ReadFromResource<MonsterData>(resData));
                    break;
            }
        }));
    }
}

