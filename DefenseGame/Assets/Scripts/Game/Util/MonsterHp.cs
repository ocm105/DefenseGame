using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHp : MonoBehaviour
{
    [SerializeField] Image hp;
    private RectTransform rect;
    public Camera mainCam { get; set; }
    public bool IsUse { get; private set; }

    private void Awake()
    {
        rect = this.GetComponent<RectTransform>();
    }
    public void SetActive(bool active)
    {
        IsUse = active;
        this.gameObject.SetActive(active);
    }
    public void SetPosition(Vector3 target)
    {
        rect.position = mainCam.WorldToScreenPoint(target);
    }
    public void SetHp(float _hp)
    {
        hp.fillAmount = _hp;
    }
}
