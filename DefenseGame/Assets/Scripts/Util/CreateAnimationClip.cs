using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class CreateAnimationClip : MonoBehaviour
{
    [Header("Sprite")]
    public Sprite spriteIdleResource;
    public Sprite spriteAttackResource;

    [Header("Image")]
    public Image imgIdleResource;
    public Image imgAttackResource;

    public string animatorName;

    public bool isSprite;

    public void CreateAnim()
    {
#if UNITY_EDITOR
        var idleClip = CreateClip(spriteIdleResource, AnimationMode.Idle);
        var attackClip = CreateClip(spriteAttackResource, AnimationMode.Attack);

        string idleStr = AnimationMode.Idle.ToString();
        string attackStr = AnimationMode.Attack.ToString();

        //clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(clipPath);
        string animatorPath = $"Assets/Animations/Unit/HS/Anim/{animatorName}.controller";
        AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath(animatorPath);
        var rootStateMachine = controller.layers[0].stateMachine;

        AnimatorState idleState = rootStateMachine.AddState(idleStr);
        idleState.motion = idleClip;
        AnimatorState attackState = rootStateMachine.AddState(attackStr);
        attackState.motion = attackClip;

        AnimatorStateTransition attackToIdle = attackState.AddTransition(idleState);
        attackToIdle.hasExitTime = true;
        attackToIdle.exitTime = 1f;
        attackToIdle.duration = 0f;

        AnimatorStateTransition idleToAttack = idleState.AddTransition(attackState);
        idleToAttack.hasExitTime = true;
        idleToAttack.exitTime = 1f;
        idleToAttack.duration = 0f;

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"생성 완료: {animatorPath}");
#endif
    }

    public void SliceSprite(Texture2D texture)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

        // 1. 기본 설정
        importer.spriteImportMode = SpriteImportMode.Multiple;
        importer.isReadable = true; // 픽셀 데이터 읽기 허용

        int sliceWidth = 64;
        int sliceHeight = 64;

        List<SpriteMetaData> metas = new List<SpriteMetaData>();

        // 2. 64x64 좌표 계산 (좌하단 기준)
        for (int y = texture.height; y >= sliceHeight; y -= sliceHeight)
        {
            for (int x = 0; x < texture.width; x += sliceWidth)
            {
                SpriteMetaData meta = new SpriteMetaData
                {
                    rect = new Rect(x, y - sliceHeight, sliceWidth, sliceHeight),
                    name = $"{texture.name}_{metas.Count}",
                    alignment = (int)SpriteAlignment.Center // 피벗 설정
                };
                metas.Add(meta);
            }
        }

        // 3. 데이터 적용 및 리임포트
        importer.spritesheet = metas.ToArray();
        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();

        AssetDatabase.Refresh();
    }

    private AnimationClip CreateClip(Sprite spriteResource, AnimationMode mode)
    {
        if (spriteResource == null)
            return null;


        string path = AssetDatabase.GetAssetPath(spriteResource);
        Object[] allAssets = AssetDatabase.LoadAllAssetsAtPath(path);
        var sprites = allAssets.OfType<Sprite>().OrderBy(s => s.name).ToArray();

        SliceSprite(spriteResource.texture);

        if (sprites.Length == 0)
            return null;

        AnimationClip clip = new AnimationClip();
        clip.frameRate = 12;

        ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[12];
        switch (mode)
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
                AnimationEvent animEvent = new AnimationEvent
                {
                    time = 4 / clip.frameRate,
                    functionName = "Attack"
                };
                AnimationUtility.SetAnimationEvents(clip, new AnimationEvent[] { animEvent });
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

        Debug.Log($"Clip 생성 완료: {clipPath}");
        return clip;
    }
}
public enum AnimationMode
{
    Idle = 0,
    Attack
}

