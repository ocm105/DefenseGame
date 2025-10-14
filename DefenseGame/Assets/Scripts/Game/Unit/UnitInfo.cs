using System;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    private string LevelSource = "Image/Level/";
    [HideInInspector] public int UnitIndex = -1;
    public UnitData UnitData { get; private set; }
    public Sprite unitSprite { get; private set; }

    [SerializeField] GameObject[] unitPositions;
    public int UnitCount { get; private set; }
    public bool isFull { get { return UnitCount >= 3; } }

    [SerializeField] Transform rangePos;
    public UnitAttackTrigger AtkTrigger { get; private set; }
    [SerializeField] GameObject rangeObject;
    [SerializeField] SpriteRenderer level;

    private void Awake()
    {
        AtkTrigger = rangePos.GetComponent<UnitAttackTrigger>();
        UnitCount = 0;
    }
    private void OnDisable()
    {
        OnClick(false);
    }

    /// <summary> Unit 정보 설정 </summary>
    public void SetData(int index)
    {
        UnitIndex = index;
        UnitData = GameDataManager.Instance.unitData[index];
        rangePos.localScale = new Vector3(UnitData.Range, UnitData.Range);
        level.sprite = Resources.Load<Sprite>($"{LevelSource}{UnitData.Level}");
    }
    public void SetPosition(Transform parent)
    {
        this.transform.parent = parent;
        this.transform.localPosition = Vector2.zero;
    }
    public void UnitCreate()
    {
        if (isFull == false)
        {
            GameObject unit = Instantiate(Resources.Load<GameObject>(UnitResource.GetPrefab(UnitData.Resource)), unitPositions[UnitCount].transform);
            unit.GetComponent<Unit>().unitInfo = this;
            UnitCount++;
        }
        this.gameObject.SetActive(true);
    }

    /// <summary> Unit 클릭 </summary>
    public void OnClick(bool isOn = true)
    {
        rangeObject.SetActive(isOn);
        level.gameObject.SetActive(isOn);
    }
    private void OnUpgrade()
    {
        for (int i = 0; i < unitPositions.Length; i++)
        {
            Destroy(unitPositions[i].transform.GetChild(0).gameObject);
        }
        UnitData.AttackSpeed *= 0.5f;
        InGameManager.Instance.UnitUpdate(this);
    }
}
