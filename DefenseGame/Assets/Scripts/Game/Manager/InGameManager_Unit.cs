using System;
using UnityEngine;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] GridInfo gridInfo;
    public int[] SynergyInfos = new int[(int)SynergyType.Max];

    public void UnitSpawn()
    {
        if (gold >= gameSetting.unitGold && gameSetting.maximumUnitCount > nowUnitSpawnCount)
        {
            bool isSpawn = false;
            int unitIndex = UnitRandom();
            SetSynergy(unitIndex);
            for (int i = 0; i < unitPool.Count; i++)
            {
                if (unitPool[i].UnitIndex == unitIndex && unitPool[i].isFull == false)
                {
                    SpendGold(-gameSetting.unitGold);
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
                    if (unitPool[i].UnitIndex == -1)
                    {
                        SpendGold(-gameSetting.unitGold);
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

    private UnitGrid UnitRandomGrid()
    {
        int ran = 0;
        do
        {
            ran = UnityEngine.Random.Range(0, gridInfo.UnitGrids.Length);
        } while (gridInfo.UnitGrids[ran].IsUnit);

        return gridInfo.UnitGrids[ran];
    }
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
