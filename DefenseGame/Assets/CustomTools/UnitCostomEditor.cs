using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitCostom))]
public class UnitCostomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnitCostom unitRectChanger = (UnitCostom)target;
        if (GUILayout.Button("변경하기", GUILayout.Height(50)))
        {
            unitRectChanger.UnitUIChange();
        }
        if (GUILayout.Button("순서 변경하기", GUILayout.Height(50)))
        {
            unitRectChanger.ChangeSibling();
        }
        if (GUILayout.Button("SpritePos 삭제", GUILayout.Height(50)))
        {
            unitRectChanger.DeletScript();
        }
    }
}

