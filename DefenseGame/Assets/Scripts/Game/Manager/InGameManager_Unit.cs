using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    private string unitSource = Constants.Character.Unit + "/UnitInfo";
    [SerializeField] GameObject unitGroup;
    [SerializeField] GridInfo gridInfo;
    private int maxUnitCreateCount = 17;            // 유닛 최고 생성 갯수
    private int nowUnitSpawnCount = 0;             // 현재 스폰 갯수
    private List<UnitInfo> unitPool = new List<UnitInfo>();
    private Dictionary<Vector2, UnitInfo> UnitDic = new Dictionary<Vector2, UnitInfo>();
    private bool IsFull
    {
        get
        {
            for (int i = 0; i < UnitDic.Count; i++)
            {
                if (UnitDic[gridInfo.GirdPos[i].anchoredPosition] == null) return false;
            }
            return true;
        }
    }

    /// <summary> 유닛 풀링 </summary>
    private void UnitPooling()
    {
        for (int i = 0; i < gridInfo.GirdPos.Length; i++)
        {
            unitPool.Add(UnitCreate());
            UnitDic.Add(gridInfo.GirdPos[i].anchoredPosition, null);
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
    public void UnitSpawn()
    {
        if (gold >= 20 && maxUnitCreateCount > nowUnitSpawnCount)
        {
            if (IsFull == false)
            {
                bool isSpawn = false;
                int unitIndex = UnitRandom();
                for (int i = 0; i < unitPool.Count; i++)
                {
                    // 소환하려는 같은 unit이 있을 때
                    if (unitPool[i].UnitIndex == unitIndex && unitPool[i].isFull == false)
                    {
                        GoldSet(-20);
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
                            Vector2 grid = UnitRandomPos();

                            unitPool[i].SetData(unitIndex);
                            unitPool[i].SetPosition(grid);
                            unitPool[i].UnitCreate();
                            UnitDic[grid] = unitPool[i];
                            nowUnitSpawnCount++;
                            break;
                        }
                    }
                }
            }
        }
        gameView.UnitCountSet(nowUnitSpawnCount, maxUnitCreateCount);
    }
    /// <summary> 유닛 랜덤 </summary>
    private int UnitRandom()
    {
        int unitDataIndex = 20000;

        return unitDataIndex + 1;
    }
    /// <summary> 유닛 랜덤 위치 </summary>
    private Vector2 UnitRandomPos()
    {
        int ran;
        do
        {
            ran = Random.Range(0, gridInfo.GirdPos.Length);
        } while (UnitDic[gridInfo.GirdPos[ran].anchoredPosition] != null);

        return gridInfo.GirdPos[ran].anchoredPosition;
    }
    /// <summary> Grid에 변경 </summary>
    public void ChangeGridUnit(UnitInfo info, Vector2 prePos, Vector2 nextPos)
    {
        if (UnitDic[nextPos] == null)
        {
            UnitDic[prePos].SetPosition(nextPos);

            UnitDic[prePos] = null;
            UnitDic[nextPos] = info;
        }
        else
        {
            UnitDic[prePos].SetPosition(nextPos);
            UnitDic[nextPos].SetPosition(prePos);

            UnitInfo unitInfo = UnitDic[prePos];
            UnitDic[prePos] = UnitDic[nextPos];
            UnitDic[nextPos] = unitInfo;
        }
    }
    /// <summary> 유닛 Undate </summary>
    public void UnitUpdate(UnitInfo info)
    {
        // gameView.UnitStatusOpen(info.UnitImage.sprite, info.unitData.Attack, info.unitData.AttackSpeed);
    }


}
