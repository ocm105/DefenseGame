using UnityEngine;
using System.Collections.Generic;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] Transform unitPoolPos;
    private List<UnitInfo> unitPool = new List<UnitInfo>();
    private int nowUnitSpawnCount = 0;

    [SerializeField] Transform monsterPoolPos;
    private Queue<Monster> monsterPool = new Queue<Monster>();
    private int stageLevel = 1;
    private int nowMonsterSpawnCount = 0; 

    #region Unit
    private void UnitPooling()
    {
        for (int i = 0; i < gameSetting.maximumUnitCount; i++)
        {
            unitPool.Add(UnitCreate());
        }
    }
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

    private void MonsterPooling()
    {
        for (int i = 0; i < gameSetting.maximumMonsterCount; i++)
        {
            MonsterCreate();
        }
    }
    private GameObject MonsterCreate()
    {
        GameObject obj = Instantiate(MonsterResource(), monsterPoolPos);
        Monster monster = obj.GetComponent<Monster>();
        monster.MovePath(monsterPathInfo.MonsterMovePath);
        MonsterRefresh(monster);
        return obj;
    }
    public void MonsterRefresh(Monster monster)
    {
        monster.transform.position = monsterPathInfo.MonsterCreatePoint.position;
        monster.transform.rotation = Quaternion.identity;
        monster.gameObject.SetActive(false);
        monsterPool.Enqueue(monster);
    }
    #endregion
}
