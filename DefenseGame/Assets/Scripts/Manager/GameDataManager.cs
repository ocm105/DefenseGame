using UnityEngine;
using UISystem;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GameDataManager : SingletonMonoBehaviour<GameDataManager>
{
    public Dictionary<int, MonsterData> monsterData = new Dictionary<int, MonsterData>();
    public Dictionary<int, WaveData> waveData = new Dictionary<int, WaveData>();
    public Dictionary<int, UnitData> unitData = new Dictionary<int, UnitData>();
    public Dictionary<int, StageData> stageData = new Dictionary<int, StageData>();
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
                NetworkManager.Instance.GetUnitDataRequest((resData) => unitData = resData),
                NetworkManager.Instance.GetStageDataRequest((resData) => stageData = resData)
            );

            for (int i = 1; i < unitData.Count + 1; i++)
            {
                unitData[GameIndex.Unit + i].SetUnitStat(unitData[GameIndex.Unit + i].Effect);
            }
        }
        isDataLoad_Completed = true;
    }
}
public class DataUnit
{

}
