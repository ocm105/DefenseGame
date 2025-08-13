using System.Linq;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    [SerializeField] RectTransform[] girdPos;
    public RectTransform[] GirdPos { get { return girdPos; } }
    private int[] gridValue;
    public int[] GridValue { get { return gridValue; } }

    private void Start()
    {
        gridValue = new int[girdPos.Length];
    }

    public void ChangeGridValue(Vector2 grid)
    {
        for (int i = 0; i < girdPos.Length; i++)
        {
            if (girdPos[i].anchoredPosition == grid)
            {
                gridValue[i] = 1;
            }
        }
    }
}
