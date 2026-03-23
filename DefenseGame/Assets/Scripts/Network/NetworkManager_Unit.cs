using System;
using UnityEngine;
using UISystem;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

[Serializable]
public class UnitData
{
    public string strID;
    public int grade;               // ·©Å©
    public string name;
    public UnitJobType job;
    public float atk;
    public float atkRange;
    public float atkSpeed;
    public int atkCount;
    public float critical;
    public float criticalPower;
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string UNIT_DATA_PATH = "https://docs.google.com/spreadsheets/d/13F6AfUVGakrPEFcEH-h2PHvT_kzbpjJtDOg-B0yNWWo/export?format=csv";

    public async UniTask<List<UnitData>> GetUnitData()
    {
        var request = await Request_Get(UNIT_DATA_PATH);

        switch (request.state)
        {
            case GAMEDATA_STATE.CONNECTDATAERROR:
            case GAMEDATA_STATE.PROTOCOLERROR:
                PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("Load Unit_DATA Fail");
                popup.OnClose = p => Application.Quit();
                popup.OnOK = p => Application.Quit();
                return null;

            case GAMEDATA_STATE.REQUESTSUCCESS:

                List<UnitData> data = new();
                var items = CSVReader.ReadFromResource<UnitData>(request.data);

                foreach (var unit in items.Values)
                {
                    data.Add(unit);
                }

                Debug.Log("UNIT_DATA Load");
                return data;

            default:
                return null;
        }
    }
}

