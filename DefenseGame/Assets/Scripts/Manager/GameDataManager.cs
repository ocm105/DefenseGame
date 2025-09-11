using UnityEngine;
using UISystem;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GameDataManager : SingletonMonoBehaviour<GameDataManager>
{
    public Dictionary<int, MonsterData> monsterData = new Dictionary<int, MonsterData>();
    public Dictionary<int, WaveData> waveData = new Dictionary<int, WaveData>();
    public Dictionary<int, UnitData> unitData = new Dictionary<int, UnitData>();
    public bool isDataLoad_Completed { get; private set; }

    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    public async UniTask LoadData()
    {
        if (isDataLoad_Completed == false)
        {
            await UniTask.WhenAll
            (
                NetworkManager.Instance.GetMonsterDataRequest((resData) => monsterData = resData),
                NetworkManager.Instance.GetWaveDataRequest((resData) => waveData = resData),
                NetworkManager.Instance.GetUnitDataRequest((resData) => unitData = resData)
            );

            for (int i = 0; i < unitData.Count; i++)
            {
                unitData[200001 + i].SetUnitStat(unitData[200001 + i].Effect);
            }

            // for (int i = 0; i < unitData.Count; i++)
            // {
            //     for (int j = 0; j < unitData[20001 + i].unitStats.Length; j++)
            //     {
            //         Debug.Log(unitData[20001 + i].unitStats[j].value1);
            //         Debug.Log(unitData[20001 + i].unitStats[j].value2);
            //         Debug.Log(unitData[20001 + i].unitStats[j].value3);
            //     }
            // }

        }
        isDataLoad_Completed = true;
    }
}
public class DataUnit
{

}
