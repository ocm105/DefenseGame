using System;
using UISystem;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

[Serializable]
public class MonsterData
{
    public int Index;
    public int Level;
    public int HP;
    public int DEF;
    public int GOLD;
    public int DIAMOND;
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string MONSTER_DATA_PATH = "https://docs.google.com/spreadsheets/d/1foTDDD2nLIwpVGan5K3I70mewA4ugg6d1XAI_OKxzT4/export?format=csv";

    public async UniTask GetMonsterDataRequest(Action<Dictionary<int, MonsterData>> callback = null)
    {
        await Request_Get(MONSTER_DATA_PATH, (dataState, resData) =>
        {
            switch (dataState)
            {
                case GAMEDATA_STATE.CONNECTDATAERROR:
                case GAMEDATA_STATE.PROTOCOLERROR:
                    PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("Load MONSTER_DATA Fail");
                    popup.OnClose = p => Application.Quit();
                    popup.OnOK = p => Application.Quit();
                    break;
                case GAMEDATA_STATE.REQUESTSUCCESS:
                    Debug.Log("MONSTER_DATA Load");
                    callback?.Invoke(CSVReader.ReadFromResource<MonsterData>(resData));
                    break;
            }
        });
    }
}

