using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UISystem;

public class GameDataManager : SingletonMonoBehaviour<GameDataManager>
{
    public List<WaveData> waveData = new();
    public List<UnitData> unitData = new();
    public List<MonsterData> monsterData = new();

    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    public async UniTask LoadData()
    {
        waveData = await NetworkManager.Instance.GetWaveData();
        monsterData = await NetworkManager.Instance.GetMonsterData();
        unitData = await NetworkManager.Instance.GetUnitData();
    }

    //public List<UnitData> GetUnitDataGrades(int grade, int level = 1)
    //{
    //    var result = new List<UnitData>();
    //    foreach (var kvp in unitData)
    //    {
    //        if (kvp.Value.Grade == grade && kvp.Value.Level == level)
    //            result.Add(kvp.Value);
    //    }
    //    return result;
    //}
}
