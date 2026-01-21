using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "ScriptableObjects/GameSetting")]
public class GameSetting : ScriptableObject
{
    [Header("기본 값")]
    [Tooltip("게임 시작 시간")] public float startTime = 3;
    [Tooltip("기본 소지금")] public int startGold = 100;
    [Tooltip("시작 Wave")] public int startWave = 1;

    [Header("Wave 정보")]
    [Tooltip("시간")] public float waveTime = 15;
    [Tooltip("최대치")] public int maxmumWave = 30;
    [Tooltip("몬스터 숫자")] public int waveMonsterCount = 10;

    [Header("Boss 정보")]
    [Tooltip("시간")] public float bossTime = 20;
    [Tooltip("Wave")] public int bossWave = 5;

    [Header("Unit 정보")]
    [Tooltip("최대치")] public int maximumUnitCount = 17;
    [Tooltip("레벨 최대치")] public int maximumUnitLevel = 3;
    [Tooltip("뽑기 비용")] public int unitGold = 20;
    [Tooltip("팔기 비용")] public int[] unitSell;

    [Header("Monster 정보")]
    [Tooltip("최대치")] public int maximumMonsterCount = 80;
    [Tooltip("소환 텀")] public float monsterSpawnTime = 0.5f;

    [Header("2성 스텟 배수")] public float star2 = 3.5f;
    [Header("3상 스탯 배수")] public float star3 = 12f;

    private bool isBossWave = false;

    /// <summary> 보스 웨이브 검사 </summary>
    public bool IsBossWave(int wave)
    {
        isBossWave = wave % 5 == 0;
        return isBossWave;
    }
    /// <summary> 현재 최대 웨이브인지 </summary>
    public bool IsMaxmumWave(int wave)
    {
        return maxmumWave <= wave;
    }
    public float WaveTime(int wave)
    {
        return isBossWave ? bossTime : waveTime;
    }
}
