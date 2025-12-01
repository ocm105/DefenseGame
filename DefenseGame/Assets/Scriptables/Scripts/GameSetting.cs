using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "ScriptableObjects/GameSetting")]
public class GameSetting : ScriptableObject
{
    [Header("게임 시작 카운트")] public float startTime = 3;
    [Header("Wave 카운트")] public float waveTime = 15;
    [Header("Wave 맥시멈")] public int maxmumWave = 30;
    [Header("보스 카운트")] public float bossTime = 20;
    [Header("유닛 2성 배수")] public float star2 = 3.5f;
    [Header("유닛 3성 배수")] public float star3 = 12f;
    [Header("유닛 뽑기 골드")] public int unitGold;
    [Header("유닛 맥시멈")] public int maximumUnitCount = 17;
    [Header("몬스터 맥시멈")] public int maximumMonsterCount = 80;
    [Header("Wave 몬스터 갯수")] public int waveMonsterCount = 10;
    [Header("몬스터 소환 시간")] public float monsterSpawnTime = 0.5f;
}
