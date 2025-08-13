using UnityEngine;

public class PathInfo : MonoBehaviour
{
    [SerializeField] RectTransform monsterCreatePoint;
    public RectTransform MonsterCreatePoint { get { return monsterCreatePoint; } }
    [SerializeField] RectTransform[] monsterMovePath;                   // 몬스터 이동 경로
    public RectTransform[] MonsterMovePath { get { return monsterMovePath; } }
}
