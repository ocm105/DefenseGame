using System;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    private string LevelSource = "Image/Level/";
    [HideInInspector] public InGameManager inGameManager;
    [HideInInspector] public int UnitIndex = -1;
    private UnitData unitData;
    public UnitData UnitData { get { return unitData; } }
    public Sprite unitSprite { get; private set; }

    [SerializeField] GameObject[] unitPositions;
    public bool isFull
    {
        get
        {
            for (int i = 0; i < unitPositions.Length; i++)
            {
                if (unitPositions[i].transform.childCount <= 0)
                    return false;
            }
            return true;
        }
    }

    [SerializeField] Transform rangePos;
    public UnitAttackTrigger AtkTrigger { get; private set; }
    [SerializeField] GameObject rangeObject;
    [SerializeField] SpriteRenderer level;
    [SerializeField] Button upgradBtn;

    private void Awake()
    {
        AtkTrigger = rangePos.GetComponent<UnitAttackTrigger>();
        upgradBtn.onClick.AddListener(OnUpgrade);
    }
    private void OnDisable()
    {
        OnClick(false);
    }

    /// <summary> Unit 정보 설정 </summary>
    public void SetData(int index)
    {
        UnitIndex = index;
        unitData = GameDataManager.Instance.unitData[index];
        rangePos.localScale = new Vector3(unitData.Range, unitData.Range);
        level.sprite = Resources.Load<Sprite>($"{LevelSource}{unitData.Level}");
    }
    public void SetPosition(Transform parent)
    {
        this.transform.parent = parent;
        this.transform.localPosition = Vector2.zero;
    }
    public void UnitCreate()
    {
        unitSprite = Resources.Load<Sprite>(UnitResource.GetImage(unitData.Level, unitData.Resource));

        for (int i = 0; i < unitPositions.Length; i++)
        {
            if (unitPositions[i].transform.childCount <= 0)
            {
                Instantiate(Resources.Load<GameObject>(UnitResource.GetPrefab(unitData.Level, unitData.Resource)), unitPositions[i].transform);
                break;
            }
        }
        this.gameObject.SetActive(true);
    }

    /// <summary> Unit 클릭 </summary>
    public void OnClick(bool isOn = true)
    {
        rangeObject.SetActive(isOn);
        if (isFull) upgradBtn.gameObject.SetActive(isOn);
    }
    private void OnUpgrade()
    {
        for (int i = 0; i < unitPositions.Length; i++)
        {
            Destroy(unitPositions[i].transform.GetChild(0).gameObject);
        }
        unitData.AttackSpeed *= 0.5f;
        upgradBtn.interactable = false;
        inGameManager.UnitUpdate(this);
    }
}
