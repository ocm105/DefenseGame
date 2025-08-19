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
            await NetworkManager.Instance.GetMonsterDataRequest((resData) => monsterData = resData);
            await NetworkManager.Instance.GetWaveDataRequest((resData) => waveData = resData);
            // yield return StartCoroutine(NetworkManager.Instance.GetUnitDataRequest((resData) => unitData = resData));
            // for (int i = 0; i < unitData.Count; i++)
            // {
            //     for (int j = 0; j < unitData[20001 + i].Effect.Count; j++)
            //     {
            //         // Debug.Log(unitData[20001 + i].Effect[j].Count);
            //         for (int l = 0; l < unitData[20001 + i].Effect[j].Count; l++)
            //         {
            //             Debug.Log(unitData[20001 + i].Effect[j][l]);
            //         }
            //     }
            // }
        }

        isDataLoad_Completed = true;
    }
}
