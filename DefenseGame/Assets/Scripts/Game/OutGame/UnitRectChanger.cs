using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UnitRectChanger : MonoBehaviour
{
    [SerializeField] GameObject[] units;

    public void UnitUIChange()
    {
        GameObject unit;
        for (int i = 0; i < units.Length; i++)
        {
            unit = Instantiate(units[i], this.transform);

            SpriteRenderer[] sr = unit.GetComponentsInChildren<SpriteRenderer>();
            Material mat;
            for (int j = 0; j < sr.Length; j++)
            {
                Image image = sr[j].AddComponent<Image>();
                image.sprite = sr[j].sprite;
                if (image.sprite == null)
                    sr[j].gameObject.SetActive(false);

                image.color = sr[j].color;
                mat = new Material(sr[j].sharedMaterial);
                image.material = mat;

                DestroyImmediate(sr[j]);
            }
        }
    }
}
