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
        monster.Initialize(GameDataManager.Instance.monsterData[monsterDataIndex], gameView.MonsterUI.SetMonsterHP());
        monster.Spawn();
        monsterAriveCount++;
    }
    public void MonsterDespawn(Monster monster)
    {
        monsterAriveCount--;
        MonsterRefresh(monster);
    }
}
