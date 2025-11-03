using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour
{
    private MonsterControl monsterControl;
    public MonsterData monsterData { get; set; }
    public float speed = 1f;

    private float hp;
    public float HPvalue { get { return hp; } }
    public MonsterHp monsterHp { get; set; }

    private void Awake()
    {
        monsterControl = this.GetComponent<MonsterControl>();
    }
    public void Spawn()
    {
        hp = monsterData.HP;
        monsterControl.MonsterStart();
        monsterControl.ChangeMonsterState(MonsterState.Arive);
        monsterHp.SetHp(1f);
        monsterHp.SetActive(true);
    }
    public void MonserHpSet(float damage)
    {
        hp -= damage;
        monsterHp.SetHp(Mathf.Clamp01(hp / monsterData.HP));
    }
}
