using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour
{
    [HideInInspector] public InGameManager inGameManager;
    private MonsterControl monsterControl;
    [SerializeField] Transform hp;
    public float HPvalue { get { return hp.localScale.x; } }
    public MonsterData monsterData { get; set; }
    public float speed = 1f;

    private float monsterHP;

    private void Awake()
    {
        monsterControl = this.GetComponent<MonsterControl>();
    }
    public void Spawn()
    {
        monsterHP = monsterData.HP;
        monsterControl.MonsterStart();
        monsterControl.ChangeMonsterState(MonsterState.Arive);
    }

    public void MonserHpSet(float damage)
    {
        monsterHP -= damage;
        hp.localScale = new Vector3(Mathf.Clamp(monsterHP / monsterData.HP, 0f, 1f), 1);
    }
}
