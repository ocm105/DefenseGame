using UnityEngine;
using UISystem;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Linq;

public class GameDataManager : SingletonMonoBehaviour<GameDataManager>
{
    public Dictionary<int, WaveData> waveData = new Dictionary<int, WaveData>();
    public Dictionary<int, StageData> stageData = new Dictionary<int, StageData>();
    public Dictionary<int, SynergyData> synergyData = new Dictionary<int, SynergyData>();
    public Dictionary<int, MonsterData> monsterData = new Dictionary<int, MonsterData>();
    public Dictionary<int, UnitData> unitData = new Dictionary<int, UnitData>();

    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    public async UniTask LoadData()
    {
        await NetworkManager.Instance.GetWaveDataRequest((resData) => waveData = resData);
        await NetworkManager.Instance.GetStageDataRequest((resData) => stageData = resData);
        await NetworkManager.Instance.GetSynergyDataRequest((resData) => synergyData = resData);
        await NetworkManager.Instance.GetMonsterDataRequest((resData) => monsterData = resData);
        await NetworkManager.Instance.GetUnitDataRequest((resData) => unitData = resData);
    }

    public List<UnitData> GetUnitDataGrades(int grade, int level = 1)
    {
        var result = new List<UnitData>();
        foreach (var kvp in unitData)
        {
            if (kvp.Value.Grade == grade && kvp.Value.Level == level)
                result.Add(kvp.Value);
        }
        return result;
    }
}
