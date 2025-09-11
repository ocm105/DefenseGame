using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    [SerializeField] RectTransform[] girdPos;
    private Vector2[] anchorPos;
    public Vector2[] GirdPos
    {
        get
        {
            anchorPos = new Vector2[girdPos.Length];
            for (int i = 0; i < girdPos.Length; i++)
            {
                anchorPos[i] = girdPos[i].anchoredPosition;
            }
            return anchorPos;
        }
    }
}
