using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDrag : MonoBehaviour
{
    private UnitGrid originGrid;
    private UnitGrid otherGrid;
    private UnitInfo unitInfo;
    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private bool isDragging = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("UnitGrid"))
                {
                    originGrid = hit.collider.GetComponent<UnitGrid>();

                    // 클릭한 grid에 유닛이 있을 때
                    if (originGrid.IsUnit && unitInfo == null)
                    {
                        unitInfo = originGrid.UnitInfo;
                        originGrid.ChageColor(isDragging);
                        isDragging = true;
                    }
                }
            }
        }
        if (isDragging)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("UnitGrid"))
                {
                    if (otherGrid == null || otherGrid != hit.collider.GetComponent<UnitGrid>())
                    {
                        originGrid.ChageColor(false);
                        otherGrid.ChageColor(false);
                        otherGrid = hit.collider.GetComponent<UnitGrid>();
                        otherGrid.ChageColor(isDragging);
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            if (unitInfo != null)
            {
                if (otherGrid != null && otherGrid.IsUnit)
                {
                    UnitInfo temp = unitInfo;
                    originGrid.UnitMove(otherGrid.UnitInfo);
                    otherGrid.UnitMove(temp);
                }
                else
                {
                    otherGrid.UnitMove(originGrid.UnitInfo);
                    originGrid.UnitInfo = null;
                }
            }

            originGrid.ChageColor(isDragging);
            otherGrid.ChageColor(isDragging);
            originGrid = null;
            otherGrid = null;
            unitInfo = null;
        }
    }
}
