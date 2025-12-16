using System;
using UISystem;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class StageData
{
    public int Index;
    public string Name;
    public string ResourceBGM;
    public StageType StageType;
    public float Increase;
    public string ResourceMonster;
    public string ResourceBossMonster;
}
public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string STAGE_DATA_PATH = "https://docs.google.com/spreadsheets/d/1taP-VXQClrxg7wuJeHwsN3dFriGW2LTNAI10S6jLw3A/export?format=csv";

    public async UniTask GetStageDataRequest(Action<Dictionary<int, StageData>> callback = null)
    {
        await Request_Get(STAGE_DATA_PATH, (dataState, resData) =>
        {
            switch (dataState)
            {
                case GAMEDATA_STATE.CONNECTDATAERROR:
                case GAMEDATA_STATE.PROTOCOLERROR:
                    PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("Load STAGE_DATA Fail");
                    popup.OnClose = p => Application.Quit();
                    popup.OnOK = p => Application.Quit();
                    break;
                case GAMEDATA_STATE.REQUESTSUCCESS:
                    Debug.Log("STAGE_DATA Load");
                    callback?.Invoke(CSVReader.ReadFromResource<StageData>(resData));
                    break;
            }
        });
    }
}

