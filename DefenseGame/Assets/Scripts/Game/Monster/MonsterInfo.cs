using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour
{
    [HideInInspector] public InGameManager inGameManager;
    private MonsterControl monsterControl;
    [SerializeField] Slider hpSlider;
    public float HPvalue { get { return hpSlider.value; } set { hpSlider.value = value; } }
    public MonsterData monsterData { get; set; }
    public float speed = 100f;

    private void Awake()
    {
        monsterControl = this.GetComponent<MonsterControl>();
    }
    public void Spawn()
    {
        MonserInfoSet();
        monsterControl.MonsterStart();
        monsterControl.ChangeMonsterState(MonsterState.Arive);
    }

    /// <summary> Moster 정보 설정 </summary>
    public void MonserInfoSet()
    {
        hpSlider.maxValue = monsterData.HP;
        hpSlider.value = monsterData.HP;
    }
}
