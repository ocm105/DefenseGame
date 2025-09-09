using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitCostom : MonoBehaviour
{
    [SerializeField] GameObject[] units;

    public void UnitUIChange()
    {
        for (int i = 0; i < units.Length; i++)
        {
            SpriteRenderer[] sr = units[i].GetComponentsInChildren<SpriteRenderer>();
            Material mat;
            for (int j = sr.Length - 1; j >= 0; j--)
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
    public void ChangeSibling()
    {
        for (int i = 0; i < units.Length; i++)
        {
            Transform[] tr = units[i].GetComponentsInChildren<Transform>();
            for (int j = 0; j < tr.Length; j++)
            {
                if (tr[j].name == "BodySet")
                {
                    tr[j].SetAsLastSibling();
                }
                else if (tr[j].name == "ArmSet")
                {
                    tr[j].SetSiblingIndex(1);
                }
                else if (tr[j].name == "P_Head")
                {
                    tr[j].SetSiblingIndex(0);
                }
                else if (tr[j].name == "ArmR")
                {
                    tr[j].SetSiblingIndex(0);
                }

            }
        }
    }

    public void DeletScript()
    {
        for (int i = 0; i < units.Length; i++)
        {
            GameObject obj = Instantiate(units[i], this.transform);
            SpritePos[] spritePos = obj.GetComponentsInChildren<SpritePos>();
            for (int j = 0; j < spritePos.Length; j++)
            {
                DestroyImmediate(spritePos[j]);
            }
        }
    }
}
