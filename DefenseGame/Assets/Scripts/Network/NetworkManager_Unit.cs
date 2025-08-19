using System;
using UnityEngine;
using UISystem;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class UnitData
{
    public int Index;
    public int Level;           // 업글
    public int Range;           // 공격범위
    public float AttackSpeed;   // 공격속도
    public int Attack;          // 공격력
    public int AttackCount;     // 1타격에 몇마리 때리는지
    public int Critical;        // 100% 기준 크리티컬 %
    public float CriticalPower; // 크리티컬 공격력 배수
    public List<string> Synergy; // 시너지
    public List<List<string>> Effect; // ?
}
[Serializable]
public class UnitStat
{
    public string value;
}



public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    public const string Unit_DATA_PATH = "https://docs.google.com/spreadsheets/d/13F6AfUVGakrPEFcEH-h2PHvT_kzbpjJtDOg-B0yNWWo/export?format=csv";

    public IEnumerator GetUnitDataRequest(Action<Dictionary<int, UnitData>> callback = null)
    {
        yield return StartCoroutine(Request_Get(Unit_DATA_PATH, (dataState, resData) =>
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
                    callback?.Invoke(CSVReader.ReadFromResource<UnitData>(resData));
                    break;
            }
        }));
    }
}

