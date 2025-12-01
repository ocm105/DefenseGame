using UnityEngine;

public partial class InGameManager : MonoBehaviour
{
    [SerializeField] PathInfo monsterPathInfo;

    /// <summary> 몬스터 초기화 </summary>
    private void MonsterInit(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = monsterPathInfo.MonsterCreatePoint.position;
        obj.transform.rotation = Quaternion.identity;
        monsterPool.Enqueue(obj);
    }
    /// <summary> 몬스터 스폰 </summary>
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
    /// <summary> 몬스터 죽었을 때 함수 </summary>
    private void MonsterDie(Monster monster)
    {
        MonsterInit(monster.gameObject);
        GoldSet(monster.monsterData.GOLD);
    }
}
