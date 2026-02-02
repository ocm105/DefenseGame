using UnityEngine;

public partial class UnitBase : MonoBehaviour
{
    private const int MaxUnit = 3;
    [HideInInspector] public int UnitIndex = -1;
    public UnitData UnitData { get; private set; }
    private UnitModel[] unitModel = new UnitModel[MaxUnit];

    public int UnitCount { get; private set; } = 0;
    public bool isFull { get { return UnitCount >= MaxUnit; } }
    public bool isMove { get; set; } = false;

    public UnitGrid grid;

    [SerializeField] GameObject[] unitPositions;
    [SerializeField] SpriteRenderer levelSprite;
    [SerializeField] Transform upgradePos;

    private int level = 1;

    private void OnDisable()
    {
        AttackCancel();
        OnClick(false);
    }
    public void Init()
    {
        UnitIndex = -1;
        UnitData = null;
        UnitCount = 0;
        level = 1;
        grid.UnitBase = null;
        this.gameObject.SetActive(false);

        for (int i = 0; i < unitModel.Length; i++)
        {
            if (unitModel[i] == null) return;
            unitModel[i].Delete();
            Destroy(unitModel[i].gameObject);
            unitModel[i] = null;
        }
    }
    public void SetData(int index)
    {
        UnitIndex = index;
        UnitData = GameDataManager.Instance.unitData[index];
        InitAttackInfo(UnitData);
        levelSprite.sprite = Resources.Load<Sprite>($"Image/Level/{level}");
        UnitUpdate().Forget();
    }
    public void UnitCreate()
    {
        if (UnitCount < MaxUnit)
        {
            string address = UnitResource.GetPrefab(UnitData.Resource);
            GameObject loadObject = Resources.Load<GameObject>(address);
            GameObject unit = Instantiate(loadObject, unitPositions[UnitCount].transform);
            unitModel[UnitCount] = unit.GetComponent<UnitModel>();
        }
        else
            unitModel[UnitCount].Init();

        UnitCount++;
        this.gameObject.SetActive(true);
    }
    public void SetPosition(Transform parent)
    {
        this.transform.parent = parent;
        this.transform.localPosition = Vector2.zero;
    }

    public void OnClick(bool isOn = true)
    {
        atkRange.gameObject.SetActive(isOn);

        var entry = InGameManager.Instance;
        var view = entry.gameView;

        if (isOn)
        {
            view.UnitStatusActive(true, UnitData);
            view.unitUI.SetPosition(upgradePos.position);

            view.unitUI.SetUpgrade_Interactable(isFull && level < entry.gameSetting.maximumUnitLevel);
            view.unitUI.SetUpgrade_Action(OnUpgrade);

            view.unitUI.SetSell_Action(OnSell);
        }
        else
            view.UnitStatusActive(false);

        view.unitUI.SetActive(isOn);
    }
    private void OnUpgrade()
    {
        UnitIndex++;
        var entry = InGameManager.Instance;

        if (entry.IsSameUnit(UnitIndex))
        {
            entry.UnitCreate(UnitIndex);
            entry.ResetUnitClick();
            entry.UnitCountUpdate(-3);
            Init();
            OnClick(false);
        }
        else
        {
            level++;
            SetData(UnitIndex);

            UnitCount = 1;
            for (int i = UnitCount; i < unitModel.Length; i++)
            {
                unitModel[i].Delete();
            }

            entry.UnitCountUpdate(-2);
            OnClick(true);
        }
    }

    private void OnSell()
    {
        if (UnitCount < 1) return;
        var entry = InGameManager.Instance;
        var lv = level - 1;

        if (UnitCount <= 1)
        {
            AttackCancel();
            entry.ResetUnitClick();
            Init();
            OnClick(false);
        }
        else
        {
            UnitCount--;
            unitModel[UnitCount].Delete();
        }
        entry.UnitCountUpdate(-1);
        entry.SpendGold(entry.gameSetting.unitSell[lv]);
    }
}
