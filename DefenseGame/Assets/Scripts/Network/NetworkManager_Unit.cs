using System;
using UnityEngine;
using UISystem;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

[Serializable]
public class UnitData
{
    public int Grade;
    public string Name;
    public int Level;               // 레벨
    public UnitJobType Job;
    public int Mana;
    public int Range;               // 공격범위
    public float AttackSpeed;       // 공격속도
    public float Attack;              // 공격력
    public int AttackCount;         // 타격 갯수
    public int Critical;            // 치명타
    public float CriticalPower;     // 치명타 배수
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

