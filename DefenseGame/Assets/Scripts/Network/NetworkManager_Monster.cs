using System;
using UISystem;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

[Serializable]
public class MonsterData
{
    public string strID;
    public float hP;
    public float speed;
}

public partial class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
    private string MONSTER_DATA_PATH = "https://docs.google.com/spreadsheets/d/1foTDDD2nLIwpVGan5K3I70mewA4ugg6d1XAI_OKxzT4/export?format=csv";

    public async UniTask<List<MonsterData>> GetMonsterData()
    {
        var request = await Request_Get(MONSTER_DATA_PATH);

        switch (request.state)
        {
            case GAMEDATA_STATE.CONNECTDATAERROR:
            case GAMEDATA_STATE.PROTOCOLERROR:
                PopupState popup = Les_UIManager.Instance.Popup<BasePopup_OneBtn>().Open("Load MONSTER_DATA Fail");
                popup.OnClose = p => Application.Quit();
                popup.OnOK = p => Application.Quit();
                return null;

            case GAMEDATA_STATE.REQUESTSUCCESS:

                List<MonsterData> data = new();
                var items = CSVReader.ReadFromResource<MonsterData>(request.data);

                foreach (var item in items)
                {
                    data.Add(item.Value);
                }

                Debug.Log("MONSTER_DATA Load");
                return data;

            default:
                return null;
        }
    }
}

