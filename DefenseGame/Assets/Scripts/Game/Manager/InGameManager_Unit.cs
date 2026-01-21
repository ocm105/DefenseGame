using System;
using UnityEngine;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] GridInfo gridInfo;
    public int[] SynergyInfos = new int[(int)SynergyType.Max];


    private int UnitRandom()
    {
        int totalWeight = 0;
        var list = GameDataManager.Instance.GetUnitDataGrades(1);
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
    private UnitGrid UnitRandomGrid()
    {
        int ran = 0;
        while (true)
        {
            ran = UnityEngine.Random.Range(0, gridInfo.UnitGrids.Length);
            if (gridInfo.UnitGrids[ran].IsUnit == false) break;
        }

        return gridInfo.UnitGrids[ran];
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
    public void UnitSpawn()
    {
        if (gold >= gameSetting.unitGold && gameSetting.maximumUnitCount > nowUnitSpawnCount)
        {
            SpendGold(-gameSetting.unitGold);
            UnitCreate(UnitRandom());
        }
    }
    public void UnitCreate(int unitIndex)
    {
        bool isSpawn = IsSameUnit(unitIndex);
        SetSynergy(unitIndex);

        for (int i = 0; i < unitPool.Count; i++)
        {
            if (isSpawn)
            {
                if (unitPool[i].UnitIndex == unitIndex && unitPool[i].isFull == false)
                {
                    unitPool[i].UnitCreate();
                    break;
                }
            }
            else
            {
                if (unitPool[i].UnitIndex == -1)
                {
                    UnitGrid grid = UnitRandomGrid();
                    grid.UnitInfo = unitPool[i];

                    unitPool[i].grid = grid;
                    unitPool[i].SetData(unitIndex);
                    unitPool[i].SetPosition(grid.transform);
                    unitPool[i].UnitCreate();
                    break;
                }
            }
        }

        UnitCountUpdate(+1);
    }

    public void UnitCountUpdate(int num)
    {
        nowUnitSpawnCount += num;
        gameView.UnitCountSet(nowUnitSpawnCount, gameSetting.maximumUnitCount);
    }
    public bool IsSameUnit(int unitIndex)
    {
        bool isSame = false;
        for (int i = 0; i < unitPool.Count; i++)
        {
            if (unitPool[i].UnitIndex == unitIndex && unitPool[i].isFull == false)
            {
                isSame = true;
                break;
            }
        }

        return isSame;
    }
}
