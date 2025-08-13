using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    private string unitSource = Constants.Character.Unit + "/Unit";
    [SerializeField] GameObject unitGroup;
    [SerializeField] GridInfo unitGridInfo;
    private int maxUnitCreateCount = 17;            // 유닛 최고 생성 갯수
    private int nowUnitSpawnCount = 0;             // 현재 스폰 갯수
    private Queue<GameObject> unitPool = new Queue<GameObject>();

    private UnitControl clickUnit;                  // 유닛 클릭 Event 용 변수
    private PointerEventData pointerEventData;
    private List<RaycastResult> pointerResults = new List<RaycastResult>();


    /// <summary> 유닛 풀링 </summary>
    private void UnitPooling()
    {
        for (int i = 0; i < maxUnitCreateCount; i++)
        {
            unitPool.Enqueue(UnitCreate());
        }
    }
    /// <summary> 유닛 생성 </summary>
    private GameObject UnitCreate()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>(unitSource), unitGroup.transform);
        UnitInit(obj);
        return obj;
    }
    /// <summary> 유닛 초기화 </summary>
    private void UnitInit(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void UnitSpawn()
    {
        if (maxUnitCreateCount > nowUnitSpawnCount)
        {
            if (unitPool.Count <= 0)
            {
                unitPool.Enqueue(UnitCreate());
            }
            GameObject obj = unitPool.Dequeue();
            obj.GetComponent<RectTransform>().anchoredPosition = UnitRandomSpawn();
            obj.SetActive(true);
            nowUnitSpawnCount++;
        }
    }
    /// <summary> 유닛 초기화 </summary>
    private Vector2 UnitRandomSpawn()
    {
        int ran;
        do
        {
            ran = Random.Range(0, unitGridInfo.GirdPos.Length);
        } while (unitGridInfo.GridValue[ran] > 0);

        unitGridInfo.ChangeGridValue(unitGridInfo.GirdPos[ran].anchoredPosition);
        return unitGridInfo.GirdPos[ran].anchoredPosition;
    }

    /// <summary> Unit 클릭 </summary>
    private void UnitClickEvent()
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
#endif
        {
            pointerEventData = new PointerEventData(EventSystem.current);
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            pointerEventData.position = Input.mousePosition;
#elif UNITY_ANDROID
            pointerEventData.position = Input.GetTouch(0).position;
#endif
            EventSystem.current.RaycastAll(pointerEventData, pointerResults);

            if (pointerResults.Count > 0)
            {
                for (int i = 0; i < pointerResults.Count; i++)
                {
                    // 클릭한게 unit일때
                    if (pointerResults[i].gameObject.CompareTag("Unit"))
                    {
                        // 이전에 클릭한 unit이 있을 때
                        if (clickUnit != null)
                        {
                            // 같은 unit을 클릭하지 않았을 때
                            if (clickUnit.gameObject != pointerResults[i].gameObject)
                            {
                                clickUnit.OnClick(false);
                                clickUnit = pointerResults[i].gameObject.GetComponent<UnitControl>();
                            }
                        }
                        // 이전에 클릭한 unit이 없을 때
                        else
                        {
                            clickUnit = pointerResults[i].gameObject.GetComponent<UnitControl>();
                        }
                        clickUnit.OnClick(true);
                        break;
                    }
                }
            }
            // 클릭한게 없을 때
            else
            {
                if (clickUnit != null)
                {
                    clickUnit.OnClick(false);
                    clickUnit = null;
                }
            }
        }
    }
}
