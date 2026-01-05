using UnityEngine;

public class UIUtil : MonoBehaviour
{
    [SerializeField] RectTransform ui;
    protected Camera mainCam;

    public bool isUsed { get; protected set; }

    private void Awake()
    {
        this.mainCam = Camera.main;
    }
    
    public virtual void SetActive(bool active)
    {
        isUsed = active;
        ui.gameObject.SetActive(active);
    }
    public virtual void SetPosition(Vector3 target)
    {
        if(mainCam == null)
            this.mainCam = Camera.main;

        ui.position = mainCam.WorldToScreenPoint(target);
    }
}
