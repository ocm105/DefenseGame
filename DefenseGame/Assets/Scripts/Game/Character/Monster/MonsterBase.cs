using System;
using System.Linq;
using UnityEngine;

public class MonsterBase : ChracterBase
{
    public MonsterData rawData;
    public MonsterModel model { get; private set; }
    private Transform[] paths;
    private int pathIndex = 0;

    private Action<MonsterBase> onDespawn;
    public bool IsReach() => pathIndex >= paths.Length;

    public void Init(MonsterData rawData)
    {
        this.rawData = rawData;
        originHP = rawData.hP;
        currentHp = originHP;
        pathIndex = 0;

        if (model == null)
        {
            var item = ModelTable.Instance.monsterModelTable.Where(x => x.id == rawData.strID.ToString()).FirstOrDefault();
            var go = GameObject.Instantiate(item.prefab, this.transform);
            model = go.GetComponent<MonsterModel>();
        }
        model.Init(this);
    }
    public void OnSpawn(Transform[] paths, Action<MonsterBase> onDespawn)
    {
        this.paths = paths;
        this.onDespawn = onDespawn;
    }
    public override void OnDamage(float damage)
    {
        if (isDead) return;

        currentHp -= damage;

        if (isDead)
        {
            onDead?.Invoke();
            model.onDead?.Invoke();
            onDespawn?.Invoke(this);
        }
    }
    public void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, paths[pathIndex].position, rawData.speed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, paths[pathIndex].position) < 0.01f) pathIndex++;
    }

}
