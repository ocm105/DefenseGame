using System;
using System.Collections.Generic;
using UnityEngine;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] Transform unitPoolPos;
    [SerializeField] GridInfo gridInfo;
    private int nowUnitSpawnCount = 0;             // 현재 스폰 갯수
    private List<UnitInfo> unitPool = new List<UnitInfo>();
    public int[] SynergyInfos = new int[(int)SynergyType.Max];

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
    public void UnitSpawn()
    {
        if (gold >= gameSetting.unitGold && gameSetting.maximumUnitCount > nowUnitSpawnCount)
        {
            bool isSpawn = false;
            int unitIndex = UnitRandom();
            SetSynergy(unitIndex);
            for (int i = 0; i < unitPool.Count; i++)
            {
                // 소환하려는 같은 unit이 있을 때
                if (unitPool[i].UnitIndex == unitIndex && unitPool[i].isFull == false)
                {
                    GoldSet(-gameSetting.unitGold);
                    unitPool[i].UnitCreate();
                    nowUnitSpawnCount++;
                    isSpawn = true;
                    break;
                }
            }
            if (isSpawn == false)
            {
                for (int i = 0; i < unitPool.Count; i++)
                {
                    // 같은 unit이 없지만 남은 pool이 있을 때
                    if (unitPool[i].UnitIndex == -1)
                    {
                        GoldSet(-20);
                        UnitGrid grid = UnitRandomGrid();
                        grid.UnitInfo = unitPool[i];

                        unitPool[i].SetData(unitIndex);
                        unitPool[i].SetPosition(grid.transform);
                        unitPool[i].UnitCreate();
                        nowUnitSpawnCount++;
                        break;
                    }
                }
            }
        }
        gameView.UnitCountSet(nowUnitSpawnCount, gameSetting.maximumUnitCount);
    }
    /// <summary> 유닛 랜덤 </summary>
    private int UnitRandom()
    {
        int totalWeight = 0;
        var list = GameDataManager.Instance.unitData.Values;
        foreach (var item in list)
        {
            totalWeight += item.Weight;
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight);

        int cumulative = 0;
        foreach (var item in list)
        {
            cumulative += item.Weight;
            if (randomValue < cumulative)
            {
                return item.Index;
            }
        }
        return 0;
    }

    /// <summary> 유닛 랜덤 위치 </summary>
    private UnitGrid UnitRandomGrid()
    {
        int ran = 0;
        do
        {
            ran = UnityEngine.Random.Range(0, gridInfo.UnitGrids.Length);
        } while (gridInfo.UnitGrids[ran].IsUnit);

        return gridInfo.UnitGrids[ran];
    }
    /// <summary> 유닛 Update </summary>
    public void UnitStatusOpen(UnitData data)
    {
        gameView.UnitStatusActive(true, data);
    }
    public void UnitStatusClose()
    {
        gameView.UnitStatusActive(false);
    }

    private void SetSynergy(int index)
    {
        int num = GameDataManager.Instance.unitData[index].Synergy.Length;
        for (int i = 0; i < num; i++)
        {
            SynergyType type = Enum.Parse<SynergyType>(GameDataManager.Instance.unitData[index].Synergy[i]);
            SynergyInfos[(int)type]++;
        }
    }

}
