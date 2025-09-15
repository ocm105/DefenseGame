using UnityEngine;
using System.Collections.Generic;
using System;

public partial class InGameManager : MonoBehaviour
{
    private string unitSource = Constants.Character.Unit + "/UnitInfo";
    [SerializeField] GameObject unitGroup;
    [SerializeField] GridInfo gridInfo;
    private int maxUnitCreateCount = 17;            // 유닛 최고 생성 갯수
    private int nowUnitSpawnCount = 0;             // 현재 스폰 갯수
    private List<UnitInfo> unitPool = new List<UnitInfo>();
    private Dictionary<Vector2, UnitInfo> UnitDic = new Dictionary<Vector2, UnitInfo>();
    public int[] SynergyInfos = new int[(int)SynergyType.Max];

    /// <summary> 유닛 풀링 </summary>
    private void UnitPooling()
    {
        for (int i = 0; i < gridInfo.GirdPos.Length; i++)
        {
            unitPool.Add(UnitCreate());
            UnitDic.Add(gridInfo.GirdPos[i], null);
        }
    }
    /// <summary> 유닛 생성 </summary>
    private UnitInfo UnitCreate()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>(unitSource), unitGroup.transform);
        UnitInfo unitInfo = obj.GetComponent<UnitInfo>();
        unitInfo.inGameManager = this;
        obj.SetActive(false);
        return unitInfo;
    }
    /// <summary> 유닛 초기화 </summary>
    private void UnitInit(UnitInfo info)
    {
        info.UnitIndex = -1;
        info.gameObject.SetActive(false);
    }
    private string GetGridAbility(Vector2 pos)
    {
        int index = 0;
        for (int i = 0; i < gridInfo.GirdPos.Length; i++)
        {
            if (gridInfo.GirdPos[i] == pos)
            {
                index = i;
                break;
            }
        }
        string ability = "ability";
        // 테이블에서 능력 가져와서 설정
        return ability;
    }
    public void UnitSpawn()
    {
        if (gold >= 20 && maxUnitCreateCount > nowUnitSpawnCount)
        {
            bool isSpawn = false;
            int unitIndex = UnitRandom();
            SetSynergy(unitIndex);
            for (int i = 0; i < unitPool.Count; i++)
            {
                // 소환하려는 같은 unit이 있을 때
                if (unitPool[i].UnitIndex == unitIndex && unitPool[i].isFull == false)
                {
                    GoldSet(-20);
                    unitPool[i].UnitCreate((UnitType)unitIndex);
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
                        Vector2 grid = UnitRandomPos();

                        unitPool[i].SetData(unitIndex);
                        unitPool[i].SetPosition(grid);
                        unitPool[i].UnitCreate((UnitType)unitIndex);
                        unitPool[i].Ability = GetGridAbility(grid);
                        UnitDic[grid] = unitPool[i];
                        nowUnitSpawnCount++;
                        break;
                    }
                }
            }
        }
        gameView.UnitCountSet(nowUnitSpawnCount, maxUnitCreateCount);
    }
    /// <summary> 유닛 랜덤 </summary>
    private int UnitRandom()
    {
        int unitDataIndex = UnityEngine.Random.Range((int)UnitType.Unit1, (int)UnitType.Max);

        return unitDataIndex;
    }
    /// <summary> 유닛 랜덤 위치 </summary>
    private Vector2 UnitRandomPos()
    {
        int ran;
        do
        {
            ran = UnityEngine.Random.Range(0, gridInfo.GirdPos.Length);
        } while (UnitDic[gridInfo.GirdPos[ran]] != null);

        return gridInfo.GirdPos[ran];
    }
    /// <summary> Grid에 변경 </summary>
    public void ChangeGridUnit(UnitInfo info, Vector2 prePos, Vector2 nextPos)
    {
        if (UnitDic[nextPos] == null)
        {
            UnitDic[prePos].MovePosition(nextPos).Forget();

            UnitDic[prePos] = null;
            UnitDic[nextPos] = info;
            UnitDic[nextPos].Ability = GetGridAbility(nextPos);
        }
        else
        {
            UnitDic[prePos].MovePosition(nextPos).Forget();
            UnitDic[nextPos].MovePosition(prePos).Forget();

            UnitInfo unitInfo = UnitDic[prePos];
            UnitDic[prePos] = UnitDic[nextPos];
            UnitDic[nextPos] = unitInfo;

            UnitDic[prePos].Ability = GetGridAbility(prePos);
            UnitDic[nextPos].Ability = GetGridAbility(nextPos);
        }
    }
    /// <summary> 유닛 Undate </summary>
    public void UnitUpdate(UnitInfo info)
    {
        gameView.UnitStatusOpen(info.unitSprite, info.UnitData.Attack, info.UnitData.AttackSpeed);
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
