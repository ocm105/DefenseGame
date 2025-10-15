using UnityEngine;

public class UIUtil : MonoBehaviour
{
    [SerializeField] RectTransform ui;
    public Camera mainCam { get; set; }

    public virtual void SetActive(bool active)
    {
        ui.gameObject.SetActive(active);
    }
    public virtual void SetPosition(Vector3 target)
    {
        ui.position = mainCam.WorldToScreenPoint(target);
    }
}
