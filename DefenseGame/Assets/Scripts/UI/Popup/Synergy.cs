using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Synergy : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI count;
    [SerializeField] TextMeshProUGUI name;

    public void SetInfo(SynergyType type, int _count)
    {
        image.sprite = Resources.Load<Sprite>($"Image/Synergy/{type.ToString()}");
        count.text = _count.ToString();
        name.text = type.ToString();
    }
}
