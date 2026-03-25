using UnityEngine;
using UnityEngine.EventSystems;

public class TestManager : MonoBehaviour
{
    public GameObject obj;


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // 1. UI 클릭 우선 확인 (UI 클릭 시 3D 로직 무시)
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    {
                        
                        if (EventSystem.current.IsPointerOverGameObject(PointerInputModule.kMouseLeftId))
                        //if (EventSystem.current.IsPointerOverGameObject(PointerInputModule.kFakeTouchesId))
                        {
                            Debug.Log("UI를 클릭했습니다.");
                            return;
                        }

                        // 2. 3D 오브젝트 레이캐스트
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out RaycastHit hit))
                        {
                            // 클릭된 오브젝트 처리
                            Debug.Log($"3D 오브젝트 클릭: {hit.transform.name}");

                            // 특정 컴포넌트나 태그로 분기 처리 가능
                            if (hit.collider.CompareTag("Player")) { /* ... */ }
                        }
                    }
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }

        }
    }
    public void OnClick()
    {
        Debug.Log("ButtonClick");
    }
}
