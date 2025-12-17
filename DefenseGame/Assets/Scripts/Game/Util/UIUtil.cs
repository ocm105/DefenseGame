using UnityEngine;

public class UIUtil : MonoBehaviour
{
    [SerializeField] RectTransform ui;
    protected Camera mainCam;

    public void SetMainCamera(Camera mainCam)
    {
        this.mainCam = mainCam;
    }
    public virtual void SetActive(bool active)
    {
        ui.gameObject.SetActive(active);
    }
    public virtual void SetPosition(Vector3 target)
    {
        ui.position = mainCam.WorldToScreenPoint(target);
    }
}
