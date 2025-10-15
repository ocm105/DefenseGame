using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    private UnitInfo unitInfo;                  // 유닛 클릭 Event 용 변수
    private Camera mainCam;
    private Vector2 clickPos;
    private RaycastHit2D[] hits;
    private bool isUnitClick = false;

    private UnitGrid preGrid;
    private UnitGrid nextGrid;
    private bool isDragging = false;

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
            isUnitClick = false;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("UnitGrid"))
                {
                    // 이전에 클릭한 unit이 있을 때
                    if (unitInfo != null)
                    {
                        unitInfo.OnClick(false);
                        unitInfo = null;
                    }

                    preGrid = hit.collider.GetComponent<UnitGrid>();

                    // 클릭한 grid에 유닛이 있을 때
                    if (preGrid.IsUnit)
                    {
                        unitInfo = preGrid.UnitInfo;
                        unitInfo.OnClick(true);
                        UnitStatusOpen(unitInfo);
                        preGrid.ChageColor(isDragging);

                        isUnitClick = true;
                        isDragging = true;
                    }
                    break;
                }
            }
            // 클릭한게 없을 때
            if (isUnitClick == false)
            {
                if (unitInfo != null)
                {
                    unitInfo.OnClick(false);
                    unitInfo = null;
                }
                gameView.UnitStatusClose();
            }
        }

        if (isDragging)
        {
            clickPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            hits = Physics2D.RaycastAll(clickPos, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("UnitGrid"))
                {
                    if (nextGrid != null)
                        nextGrid.ChageColor(false);

                    preGrid.ChageColor(false);
                    nextGrid = hit.collider.GetComponent<UnitGrid>();
                    nextGrid.ChageColor(isDragging);
                    break;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;

                if (nextGrid != null && nextGrid.IsUnit)
                {
                    preGrid.UnitMove(nextGrid.UnitInfo);
                    nextGrid.UnitMove(unitInfo);
                }
                else
                {
                    nextGrid.UnitMove(unitInfo);
                    preGrid.UnitInfo = null;
                }

                preGrid.ChageColor(isDragging);
                nextGrid.ChageColor(isDragging);
                preGrid = null;
                nextGrid = null;
            }
        }
    }
}
