using UnityEngine;
using System.Collections.Generic;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] Transform unitPoolPos;
    private List<UnitInfo> unitPool = new List<UnitInfo>();
    private int nowUnitSpawnCount = 0;             // 현재 스폰 갯수

    [SerializeField] GameObject monsterGroup;
    private Queue<GameObject> monsterPool = new Queue<GameObject>();
    private int stageLevel = 1;
    private int nowMonsterSpawnCount = 0;                           // 현재 스폰 갯수

    #region Unit
    /// <summary> 유닛 풀링 </summary>
    private void UnitPooling()
    {
        for (int i = 0; i < gameSetting.maximumUnitCount; i++)
        {
            unitPool.Add(UnitCreate());
        }
    }
    /// <summary> 유닛 생성 </summary>
    private UnitInfo UnitCreate()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>(UnitResource.UnitInfo), unitPoolPos);
        UnitInfo info = obj.GetComponent<UnitInfo>();
        info.unitUpgrade = gameView.UnitUI.UnitUpgrade;
        obj.SetActive(false);
        return info;
    }
    #endregion

    #region Monster
    private GameObject MonsterResource()
    {
        return Resources.Load<GameObject>(UnitResource.GetMonster(GameDataManager.Instance.stageData[GameIndex.Stage + stageLevel].ResourceMonster));
    }

    /// <summary> 몬스터 풀링 </summary>
    private void MonsterPooling()
    {
        for (int i = 0; i < gameSetting.maximumMonsterCount; i++)
        {
            MonsterCreate();
        }
    }
    /// <summary> 몬스터 생성 </summary>
    private GameObject MonsterCreate()
    {
        GameObject obj = Instantiate(MonsterResource(), monsterGroup.transform);
        Monster monster = obj.GetComponent<Monster>();
        monster.MovePath(monsterPathInfo.MonsterMovePath);
        monster.dieAction = (info) => MonsterDie(info);
        MonsterInit(obj);
        return obj;
    }
    #endregion
}
