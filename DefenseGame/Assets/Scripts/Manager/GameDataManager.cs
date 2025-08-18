using UnityEngine;
using UISystem;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GameDataManager : SingletonMonoBehaviour<GameDataManager>
{
    public Dictionary<int, MonsterData> monsterData = new Dictionary<int, MonsterData>();
    public Dictionary<int, WaveData> waveData = new Dictionary<int, WaveData>();
    public bool isDataLoad_Completed { get; private set; }

    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    public IEnumerator LoadData()
    {
        if (isDataLoad_Completed == false)
        {
            yield return StartCoroutine(NetworkManager.Instance.GetMonsterDataRequest((resData) => monsterData = resData));
            yield return StartCoroutine(NetworkManager.Instance.GetWaveDataRequest((resData) => waveData = resData));
        }

        isDataLoad_Completed = true;
    }

    // public UniTask<bool> LoadData()
    // {
    //     if (isDataLoad_Completed == false)
    //     {
    //         yield return StartCoroutine(NetworkManager.Instance.GetMonsterDataRequest((resData) => monsterData = resData));
    //         yield return StartCoroutine(NetworkManager.Instance.GetWaveDataRequest((resData) => waveData = resData));
    //         isDataLoad_Completed = true;
    //     }
    //     yield return isDataLoad_Completed;
    // }
}
