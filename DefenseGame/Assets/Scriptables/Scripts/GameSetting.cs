using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "ScriptableObjects/GameSetting")]
public class GameSetting : ScriptableObject
{
    [Header("게임 시작 시간")] public float startTime = 3;
    [Header("기본 소지금")] public int startGold = 100;
    [Header("시작 Wave")] public int startWave = 1;

    [Header("Wave 시간")] public float waveTime = 15;
    [Header("Wave 최대치")] public int maxmumWave = 30;
    [Header("Wave당 몬스터 숫자")] public int waveMonsterCount = 10;

    [Header("보스 시간")] public float bossTime = 20;
    [Header("보스 Wave")] public int bossWave = 5;

    [Header("유닛 뽑기 비용")] public int unitGold = 20;
    [Header("유닛 최대치")] public int maximumUnitCount = 17;

    [Header("몬스터 최대치")] public int maximumMonsterCount = 80;
    [Header("몬스터 소환 사이시간")] public float monsterSpawnTime = 0.5f;

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
