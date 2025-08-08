using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Dictionary<int, MonsterData> monster_Data = new Dictionary<int, MonsterData>();
    public bool isDataLoad_Completed { get; private set; }

    public GameMaxScoreInfo gameMaxScoreInfo;
    public LocalSettingInfo localSettingInfo;


    private void Start()
    {
        StartCoroutine(LoadData());
    }

    public IEnumerator LoadData()
    {
        if (isDataLoad_Completed == false)
        {
            yield return StartCoroutine(NetworkManager.Instance.GetDescriptRequest((resData) => monster_Data = resData));
        }

        isDataLoad_Completed = true;
    }

    public void OnClick_Test()
    {
        for (int i = 1; i < monster_Data.Count + 1; i++)
        {
            Debug.Log($"index = {i} / {monster_Data[i].type}");
        }
    }


}
