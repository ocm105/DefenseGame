using System;
using UnityEngine;
using UISystem;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

[Serializable]
public class UnitData
{
    public int Index;
    public string Name;
    public int Level;               // 업글
    public string Job;
    public int Mana;
    public int Range;               // 공격범위
    public float AttackSpeed;       // 공격속도
    public int Attack;              // 공격력
    public int AttackCount;         // 1타격에 몇마리 때리는지
    public int Critical;            // 100% 기준 크리티컬 %
    public float CriticalPower;     // 크리티컬 공격력 배수
    public string[] Synergy;        // 시너지
    public string Resource;
    public int Weight;              // 가중치
    public List<string> Effect;     // { "Atk|10|10", "DEF|5|5" }
    public UnitStat[] unitStats;

    public void SetUnitStat(List<string> strs)
    {
        string[] arr = new string[strs.Count];
        unitStats = new UnitStat[strs.Count];
        for (int i = 0; i < strs.Count; i++)
        {
            unitStats[i] = new UnitStat();
            arr = strs[i].Split('|');
            unitStats[i].value1 = arr[0];
            unitStats[i].value2 = arr[1];
            unitStats[i].value3 = arr[2];
        }
    }
}

public class UnitStat
{
    public string value1;
    public string value2;
    public string value3;
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string UNIT_DATA_PATH = "https://docs.google.com/spreadsheets/d/13F6AfUVGakrPEFcEH-h2PHvT_kzbpjJtDOg-B0yNWWo/export?format=csv";

    public async UniTask GetUnitDataRequest(Action<Dictionary<int, UnitData>> callback = null)
    {
        await Request_Get(UNIT_DATA_PATH, (dataState, resData) =>
        {
            switch (dataState)
            {
                case GAMEDATA_STATE.CONNECTDATAERROR:
                case GAMEDATA_STATE.PROTOCOLERROR:
                    PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("Unit_DATA를 받아오지 못했습니다.");
                    popup.OnClose = p => Application.Quit();
                    popup.OnOK = p => Application.Quit();
                    break;
                case GAMEDATA_STATE.REQUESTSUCCESS:
                    var unitData = CSVReader.ReadFromResource<UnitData>(resData);

                    foreach (var unit in unitData.Values)
                    {
                        unit.SetUnitStat(unit.Effect);
                    }
                    Debug.Log("UNIT_DATA 완료");
                    callback?.Invoke(unitData);
                    break;
            }
        });
    }
}

