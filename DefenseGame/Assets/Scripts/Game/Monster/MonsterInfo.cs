using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour
{
    private MonsterControl monsterControl;
    [SerializeField] Slider hpSlider;
    public float HPvalue { get { return hpSlider.value; } set { hpSlider.value = value; } }
    public float hp = 100f;
    public float def = 0f;
    public int gold = 10;
    public float speed = 200f;

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
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }
}
