using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    private UnitInfo clickUnit;                  // 유닛 클릭 Event 용 변수
    private PointerEventData pointerEventData;
    private List<RaycastResult> pointerResults = new List<RaycastResult>();

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
                    if (pointerResults[i].gameObject.CompareTag("UnitGrid"))
                    {
                        // 이전에 클릭한 unit이 있을 때
                        if (clickUnit != null)
                        {
                            // 같은 unit을 클릭하지 않았을 때
                            if (clickUnit.gameObject != pointerResults[i].gameObject)
                            {
                                clickUnit.OnClick(false);
                                clickUnit = pointerResults[i].gameObject.GetComponent<UnitGrid>().UnitInfo;
                            }
                        }
                        // 이전에 클릭한 unit이 없을 때
                        else
                        {
                            clickUnit = pointerResults[i].gameObject.GetComponent<UnitGrid>().UnitInfo;
                        }
                        clickUnit.OnClick(true);
                        UnitUpdate(clickUnit);
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
                    gameView.UnitStatusClose();
                    clickUnit = null;
                }
            }
        }
    }
}
