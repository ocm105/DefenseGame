using UnityEngine;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] PathInfo monsterPathInfo;

    private void MonsterInit(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = monsterPathInfo.MonsterCreatePoint.position;
        obj.transform.rotation = Quaternion.identity;
        monsterPool.Enqueue(obj);
    }
    private void MonsterSpawn()
    {
        if (monsterPool.Count <= 0)
        {
            MonsterCreate();
        }
        int monsterDataIndex = GameDataManager.Instance.waveData[GameIndex.Wave + waveIndex].Summon;
        GameObject obj = monsterPool.Dequeue();
        Monster monster = obj.GetComponent<Monster>();
        monster.monsterData = GameDataManager.Instance.monsterData[monsterDataIndex];
        monster.monsterHp = gameView.MonsterUI.SetMonsterHP();
        monster.Spawn();
        obj.SetActive(true);
    }
    private void MonsterDie(Monster monster)
    {
        MonsterInit(monster.gameObject);
        GoldSet(monster.monsterData.GOLD);
    }
}
