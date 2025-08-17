using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitRectChanger))]
public class UnitRectChangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnitRectChanger unitRectChanger = (UnitRectChanger)target;
        if (GUILayout.Button("변경하기", GUILayout.Height(50)))
        {
            unitRectChanger.UnitUIChange();
        }
    }
}

