using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using static Codice.Client.BaseCommands.Import.Commit;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

[CustomEditor(typeof(CreateAnimationClip))]
public class CreateAnimationClipEditor : Editor
{
    private bool toggleValue = false;
    private bool first = false;
    private bool second = false;
    private bool third = false;
    private int selected = 1;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CreateAnimationClip script = (CreateAnimationClip)target;

        EditorGUILayout.Space();
        if (GUILayout.Button("애니메이션 클립 생성", GUILayout.Height(30)))
        {
            script.CreateAnim();
        }
    }
}
