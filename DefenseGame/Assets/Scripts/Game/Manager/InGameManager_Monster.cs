using UnityEngine;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] PathInfo monsterPathInfo;
    private int monsterAriveCount = 0;
    public bool IsMonsterArive => monsterAriveCount > 0;

    private void MonsterSpawn()
    {
        if (monsterPool.Count <= 0)
        {
            MonsterCreate();
        }
        int monsterDataIndex = GameDataManager.Instance.waveData[GameIndex.Wave + waveIndex].Summon;
        Monster monster = monsterPool.Dequeue();
        monster.Initialize(GameDataManager.Instance.monsterData[monsterDataIndex], MonsterType.Nomal, gameView.monsterUI.SetMonsterHP());
        monster.Spawn();

        nowMonsterSpawnCount++;
        monsterAriveCount++;
        gameView.SetMonsterCount(monsterAriveCount);
    }
    public void MonsterDespawn()
    {
        monsterAriveCount--;
        gameView.SetMonsterCount(monsterAriveCount);
    }

    private void BossSpwan()
    {
        if (monsterPool.Count <= 0)
        {
            MonsterCreate();
        }
        int monsterDataIndex = GameDataManager.Instance.waveData[GameIndex.Wave + waveIndex].Summon;
        Monster monster = monsterPool.Dequeue();
        monster.Initialize(GameDataManager.Instance.monsterData[monsterDataIndex], MonsterType.Boss, gameView.monsterUI.testBossHp);//gameView.monsterUI.SetBossMonsterHP());
        monster.Spawn();

        nowMonsterSpawnCount++;
        monsterAriveCount++;
        gameView.SetMonsterCount(monsterAriveCount);

        gameView.waringPanel.SetInfo(null, waveIndex, waveTime);
    }
}
