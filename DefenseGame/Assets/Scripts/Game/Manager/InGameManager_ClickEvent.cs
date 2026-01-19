using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    private PointerEventData pointerEventData;
    private List<RaycastResult> pointerResults = new List<RaycastResult>();

    private UnitGrid nowGrid = null;
    private UnitGrid nextGrid = null;
    private bool isDragging = false;

    public void ResetUnitClick()
    {
        nowGrid = null;
        nextGrid = null;
    }

    private void UnitClickEvent()
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
#elif UNITY_ANDROID
        if (Input.touchCount == 1)
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
                if (!isDragging)
                {
                    foreach (var result in pointerResults)
                    {
                        if (result.gameObject.layer.Equals(LayerMask.NameToLayer("UI"))) break;
                        if (result.gameObject.CompareTag("UnitGrid"))
                        {
                            UnitGrid grid = result.gameObject.GetComponent<UnitGrid>();

                            if (grid.IsUnit)
                            {
                                if (nowGrid != null && nowGrid != grid)
                                {
                                    nowGrid.UnitInfo.OnClick(false);
                                    nowGrid.ChageColor(false);
                                }

                                grid.UnitInfo.OnClick(true);
                                grid.ChageColor(true);
                                nowGrid = grid;
                                isDragging = true;
                                break;
                            }
                        }
                        else
                        {
                            if (nowGrid != null)
                            {
                                nowGrid.UnitInfo.OnClick(false);
                                nowGrid = null;
                            }
                            gameView.UnitStatusActive(false);
                            isDragging = false;
                            break;
                        }
                    }
                }
            }
        }

        if (isDragging)
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            pointerEventData.position = Input.mousePosition;
#elif UNITY_ANDROID
            pointerEventData.position = Input.GetTouch(0).position;
#endif
            EventSystem.current.RaycastAll(pointerEventData, pointerResults);

            foreach (var result in pointerResults)
            {
                if (result.gameObject.CompareTag("UnitGrid"))
                {
                    UnitGrid grid = result.gameObject.GetComponent<UnitGrid>();

                    if (nowGrid != grid)
                    {
                        grid.ChageColor(true);
                        nowGrid.ChageColor(false);

                        if (nextGrid != null && nextGrid != grid)
                        {
                            nextGrid.ChageColor(false);
                        }
                        nextGrid = grid;
                    }
                    else
                    {
                        nowGrid.ChageColor(true);
                        if (nextGrid != null)
                        {
                            nextGrid.ChageColor(false);
                            nextGrid = null;
                        }
                    }
                    break;
                }
            }
        }
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonUp(0))
#elif UNITY_ANDROID
        if (Input.touchCount != 1)
#endif
        {
            isDragging = false;

            if (nextGrid != null)
            {
                if (nextGrid.IsUnit)
                {
                    UnitInfo info = nextGrid.UnitInfo;
                    nextGrid.UnitMove(nowGrid.UnitInfo);
                    nowGrid.UnitMove(info);
                }
                else
                {
                    nextGrid.UnitMove(nowGrid.UnitInfo);
                    nowGrid.UnitInfo = null;
                }

                nextGrid.ChageColor(false);
                nowGrid.ChageColor(false);
                nowGrid = nextGrid;
                nextGrid = null;
            }

            if (nowGrid != null)
            {
                nowGrid.ChageColor(false);
            }
        }
    }
}
