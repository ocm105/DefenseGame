using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    private UnitInfo clickUnit;                  // 유닛 클릭 Event 용 변수
    private Camera mainCam;
    private Vector2 clickPos;
    private RaycastHit2D[] hits;
    private void Awake()
    {
        mainCam = Camera.main;
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
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            clickPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID
            clickPos = mainCam.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
            hits = Physics2D.RaycastAll(clickPos, Vector2.zero);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    // 클릭한게 unit일때
                    if (hits[i].collider.CompareTag("UnitGrid"))
                    {
                        // 이전에 클릭한 unit이 있을 때
                        if (clickUnit != null)
                        {
                            // 같은 unit을 클릭하지 않았을 때
                            if (clickUnit.gameObject != hits[i].collider.gameObject)
                            {
                                clickUnit.OnClick(false);
                                clickUnit = hits[i].collider.gameObject.GetComponent<UnitGrid>().UnitInfo;
                            }
                        }
                        // 이전에 클릭한 unit이 없을 때
                        else
                        {
                            clickUnit = hits[i].collider.gameObject.GetComponent<UnitGrid>().UnitInfo;
                        }

                        if (clickUnit != null)
                        {
                            clickUnit.OnClick(true);
                            UnitUpdate(clickUnit);
                        }
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
