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
    public Dictionary<int, SynergyData> synergyData = new Dictionary<int, SynergyData>();
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
                NetworkManager.Instance.GetStageDataRequest((resData) => stageData = resData),
                NetworkManager.Instance.GetSynergyDataRequest((resData) => synergyData = resData)
            );

            foreach (var unit in unitData.Values)
            {
                unit.SetUnitStat(unit.Effect);
            }
            foreach (var synergy in synergyData.Values)
            {
                synergy.SetPassiveSynergy(synergy.PassiveSynergy);
            }
        }
        isDataLoad_Completed = true;
    }
}
public class DataUnit
{

}
