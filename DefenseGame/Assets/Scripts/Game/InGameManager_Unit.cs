using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] GridInfo unitGridInfo;
    private UnitControl characterControl;
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
            for (int i = 0; i < pointerResults.Count; i++)
            {
                // 클릭한게 unit일때
                if (pointerResults[i].gameObject.CompareTag("Unit"))
                {
                    // 이전에 클릭한 unit이 있을 때
                    if (characterControl != null)
                    {
                        // 같은 unit을 클릭하지 않았을 때
                        if (characterControl.gameObject != pointerResults[i].gameObject)
                        {
                            characterControl.OnClick(false);
                            characterControl = pointerResults[i].gameObject.GetComponent<UnitControl>();
                        }
                        characterControl.OnClick(true);
                    }
                }
                else    // unit을 클릭하지 안았을 때
                {
                    if (characterControl != null)
                    {
                        characterControl.OnClick(false);
                        characterControl = null;
                    }
                }
            }
        }
    }
}
