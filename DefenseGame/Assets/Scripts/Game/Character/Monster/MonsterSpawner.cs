using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform[] paths;
    private float spawnInterval = 1f;

    private List<MonsterBase> monsterMonos = new();
    private List<MonsterBase> removeMonos = new();
    private CancellationTokenSource spawnToken;

    public void OnSpawn()
    {
        if (spawnToken == null)
            spawnToken = new CancellationTokenSource();

        SpawnAsync().Forget();
    }
    private void DeSpawn(MonsterBase monster)
    {
        monsterMonos.Remove(monster);
        GameView.Instance.ReturnMonsterFactroy(monster);
    }
    private async UniTask SpawnAsync()
    {
        MonsterData data = GameDataManager.Instance.monsterData.Where(x => x.strID == "Slime").FirstOrDefault();
        MonsterBase monster = null;
        for (int i = 0; i < 20; i++)
        {
            monster = GameView.Instance.GetMonster(data, paths[0].position, this.transform);
            monster.OnSpawn(paths, DeSpawn);
            monsterMonos.Add(monster);
            await UniTask.WaitForSeconds(spawnInterval, cancellationToken: spawnToken.Token);
        }
    }
    private void SpawnCancle()
    {
        if (spawnToken != null)
        {
            spawnToken.Cancel();
            spawnToken.Dispose();
            spawnToken = null;
        }
    }

    private void Update()
    {
        if (monsterMonos.Count <= 0) return;

        removeMonos.Clear();
        foreach (var monster in monsterMonos)
        {
            monster.Move();
            if (monster.IsReach())
                removeMonos.Add(monster);
        }

        foreach (var monster in removeMonos)
        {
            DeSpawn(monster);
        }
    }
}
