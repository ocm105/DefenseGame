using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform dragObject;
    [SerializeField] RectTransform attackRange;
    [SerializeField] Vector2 gridCellSize;      // grid 크기 (1유닛)
    [SerializeField] Vector2 dragClampPos;      // Drag 한정위치
    private Vector2 snapPos;                    // snap 위치
    private Vector2 dragPos;                    // Drag 하고 있는 위치
    private Vector2 offset;                     // 클릭시 Pivot 과 ClickPoint의 격차
    private float gridOffsetX;
    private RectTransform unitRect;

    private bool isDragging = false;
    private void Awake()
    {
        unitRect = this.GetComponent<RectTransform>();
    }
    /// <summary> Drag 시작 </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        dragPos = eventData.position;
        offset = dragObject.anchoredPosition - new Vector2(dragPos.x, dragPos.y);
        dragObject.gameObject.SetActive(isDragging);
    }
    /// <summary> Drag 중 </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            dragPos = eventData.position + offset;

            snapPos.y = Mathf.Round(dragPos.y / gridCellSize.y) * gridCellSize.y;

            gridOffsetX = snapPos.y == 0 ? 0 : gridCellSize.x * 0.5f;
            snapPos.x = Mathf.Round((dragPos.x - gridOffsetX) / gridCellSize.x) * gridCellSize.x + gridOffsetX;

            dragObject.anchoredPosition = new Vector2
            (
                Mathf.Clamp(snapPos.x, -dragClampPos.x - gridOffsetX, dragClampPos.x + gridOffsetX),
                Mathf.Clamp(snapPos.y, -dragClampPos.y, dragClampPos.y)
            );
        }
    }
    /// <summary> Drag 끝 </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        unitRect.anchoredPosition = dragObject.anchoredPosition;
        attackRange.anchoredPosition = unitRect.anchoredPosition;
        dragObject.gameObject.SetActive(isDragging);
    }

}
