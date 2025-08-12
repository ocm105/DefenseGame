using UnityEngine;

public partial class UnitControl : MonoBehaviour
{
    [SerializeField] Vector2 gridCellSize;      // grid 크기 (1유닛)
    [SerializeField] Vector2 dragClampPos;      // Drag 한정위치
    private Vector2 snapPos;                    // snap 위치
    private Camera mainCamera;
    private Vector2 dragPos;                    // Drag 하고 있는 위치
    private Vector3 offset;                     // 클릭시 Pivot 과 ClickPoint의 격차
    private bool isDragging = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;
        dragPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = dragObject.transform.position - new Vector3(dragPos.x, dragPos.y);
    }

    private void OnMouseUp()
    {
        isDragging = false;

        this.transform.position = dragObject.transform.position;
        dragObject.transform.localPosition = Vector2.zero;
    }
    private void Update()
    {
        if (isDragging)
        {
            dragPos = mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;

            snapPos.x = Mathf.Round(dragPos.x / gridCellSize.x) * gridCellSize.x;
            snapPos.y = Mathf.Round(dragPos.y / gridCellSize.y) * gridCellSize.y;
            dragObject.transform.position = new Vector2
            (
                Mathf.Clamp(snapPos.x, -dragClampPos.x, dragClampPos.x),
                Mathf.Clamp(snapPos.y, -dragClampPos.y, dragClampPos.y)
            );
        }
    }
}
