using UnityEngine;
using UnityEngine.EventSystems;

public partial class UnitControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] GameObject dragObject;
    [SerializeField] Vector2 gridCellSize;      // grid 크기 (1유닛)
    [SerializeField] Vector2 dragClampPos;      // Drag 한정위치
    private Vector2 snapPos;                    // snap 위치
    private Vector2 dragPos;                    // Drag 하고 있는 위치
    private Vector2 offset;                     // 클릭시 Pivot 과 ClickPoint의 격차
    private float gridOffsetX;
    private RectTransform unitRect;
    private RectTransform dragObjectRect;

    private bool isDragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        dragPos = eventData.position;
        offset = dragObjectRect.anchoredPosition - new Vector2(dragPos.x, dragPos.y);
        dragObject.SetActive(isDragging);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            dragPos = eventData.position + offset;

            snapPos.y = Mathf.Round(dragPos.y / gridCellSize.y) * gridCellSize.y;

            gridOffsetX = snapPos.y == 0 ? 0 : gridCellSize.x * 0.5f;
            snapPos.x = Mathf.Round((dragPos.x - gridOffsetX) / gridCellSize.x) * gridCellSize.x + gridOffsetX;

            dragObjectRect.anchoredPosition = new Vector2
            (
                Mathf.Clamp(snapPos.x, -dragClampPos.x - gridOffsetX, dragClampPos.x + gridOffsetX),
                Mathf.Clamp(snapPos.y, -dragClampPos.y, dragClampPos.y)
            );
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        unitRect.anchoredPosition = dragObjectRect.anchoredPosition;
        attackRangeImage.rectTransform.anchoredPosition = unitRect.anchoredPosition;
        dragObject.SetActive(isDragging);
    }

}
