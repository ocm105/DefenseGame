using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class CreateAnimationClip : MonoBehaviour
{
    [Header("Sprite")]
    public Sprite spriteResource;

    [Header("Image")]
    public Image imgResource;

    [Header("Animation Mode")]
    public AnimationMode animationMode = AnimationMode.Idle;

    public bool isSprite;

    public void CreateAnim()
    {
#if UNITY_EDITOR
        if (spriteResource == null)
            return;

        string path = AssetDatabase.GetAssetPath(spriteResource);
        Object[] allAssets = AssetDatabase.LoadAllAssetsAtPath(path);
        var sprites = allAssets.OfType<Sprite>().OrderBy(s => s.name).ToArray();

        if (sprites.Length == 0)
            return;

        AnimationClip clip = new AnimationClip();
        clip.frameRate = 12;

        ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[12];
        switch (animationMode)
        {
            case AnimationMode.Idle:
                {
                    keyframes[0] = new ObjectReferenceKeyframe
                    {
                        time = 0 / clip.frameRate,
                        value = sprites[0]
                    };
                    keyframes[2] = new ObjectReferenceKeyframe
                    {
                        time = 2 / clip.frameRate,
                        value = sprites[1]
                    };
                    keyframes[5] = new ObjectReferenceKeyframe
                    {
                        time = 5 / clip.frameRate,
                        value = sprites[0]
                    };
                }
                break;
            case AnimationMode.Attack:
                {
                    keyframes[0] = new ObjectReferenceKeyframe
                    {
                        time = 0 / clip.frameRate,
                        value = sprites[0]
                    };
                    keyframes[4] = new ObjectReferenceKeyframe
                    {
                        time = 4 / clip.frameRate,
                        value = sprites[1]
                    };
                    keyframes[7] = new ObjectReferenceKeyframe
                    {
                        time = 7 / clip.frameRate,
                        value = sprites[2]
                    };
                    keyframes[10] = new ObjectReferenceKeyframe
                    {
                        time = 10 / clip.frameRate,
                        value = sprites[3]
                    };
                }
                break;
        }


        EditorCurveBinding binding = new EditorCurveBinding
        {
            type = isSprite ? typeof(SpriteRenderer) : typeof(Image),
            path = "",
            propertyName = "m_Sprite"
        };

        AnimationUtility.SetObjectReferenceCurve(clip, binding, keyframes);

        string clipPath = $"Assets/Animations/Unit/HS/Anim/{spriteResource.name}.anim";

        AssetDatabase.CreateAsset(clip, clipPath);

        Debug.Log($"Clip 积己 肯丰: {clipPath}");

        string savePath = $"Assets/Animations/Unit/HS/Anim/{spriteResource.name}.anim";
        clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(clipPath);
        AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath(clipPath);
        var rootStateMachine = controller.layers[0].stateMachine;

        
        string stateName = animationMode.ToString();
        AnimatorState state = rootStateMachine.AddState(stateName);
        state.motion = clip;

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"积己 肯丰: {clipPath}");
#endif
    }
}
public enum AnimationMode
{
    Idle = 0,
    Attack
}

