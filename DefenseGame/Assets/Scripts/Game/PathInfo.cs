using UnityEngine;

public class PathInfo : MonoBehaviour
{
    [SerializeField] Transform monsterCreatePoint;
    public Transform MonsterCreatePoint { get { return monsterCreatePoint; } }
    [SerializeField] Transform[] monsterMovePath;                   // 몬스터 이동 경로
    public Transform[] MonsterMovePath { get { return monsterMovePath; } }
}
