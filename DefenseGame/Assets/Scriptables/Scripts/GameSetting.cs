using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "ScriptableObjects/GameSetting")]
public class GameSetting : ScriptableObject
{
    [Tooltip("게임 시작 카운트")] public float startTime = 3;
    [Tooltip("Wave 카운트")] public float waveTime = 15;
    [Tooltip("Wave 맥시멈")] public int maxmumWave = 30;
    [Tooltip("보스 카운트")] public float bossTime = 20;
    [Tooltip("유닛 2성 배수")] public float star2 = 3.5f;
    [Tooltip("유닛 3성 배수")] public float star3 = 12f;
    [Tooltip("유닛 뽑기 골드")] public int unitGold;
    [Tooltip("유닛 맥시멈 ")] public int maximumUnitCount = 17;
    [Tooltip("몬스터 맥시멈 ")] public int maximumMonsterCount = 80;
    [Tooltip("Wave 몬스터 갯수 ")] public int waveMonsterCount = 10;
    [Tooltip("몬스터 소환 시간 ")] public float monsterSpawnTime = 0.5f;
}
