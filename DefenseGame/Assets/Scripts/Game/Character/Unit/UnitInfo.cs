using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    [HideInInspector] public int UnitIndex = -1;
    public UnitData UnitData { get; private set; }
    public UnitGrid grid;

    [SerializeField] GameObject[] unitPositions;
    private List<Unit> unitList = new List<Unit>();
    public int UnitCount { get; private set; } = 0;
    public bool isFull { get { return UnitCount >= 3; } }
    public bool isMove { get; set; } = false;

    [SerializeField] SpriteRenderer levelSprite;
    [SerializeField] Transform upgradePos;
    [SerializeField] UnitAttackTrigger atkTrigger;
    public UnitAttackTrigger AtkTrigger { get { return atkTrigger; } }
    private SpriteRenderer atkRangeSpr;
    private int level = 1;

    private void Awake()
    {
        atkRangeSpr = atkTrigger.GetComponent<SpriteRenderer>();
    }
    private void OnDisable()
    {
        OnClick(false);
    }
    public void Init()
    {
        UnitIndex = -1;
        UnitData = null;
        level = 1;
        UnitCount = 0;
        for (int i = 0; i < unitList.Count; i++)
        {
            unitList[i].Delete();
            Destroy(unitList[i].gameObject);
        }
        unitList.Clear();
        grid.UnitInfo = null;
        this.gameObject.SetActive(false);
    }
    public void SetData(int index)
    {
        UnitIndex = index;
        UnitData = GameDataManager.Instance.unitData[index];
        atkTrigger.transform.localScale = new Vector3(UnitData.Range, UnitData.Range);
        levelSprite.sprite = Resources.Load<Sprite>(StringExtension.StringMerge("Image/Level/", level.ToString()));
    }
    public void UnitCreate()
    {
        if (unitList.Count < 3)
        {
            string address = UnitResource.GetPrefab(UnitData.Resource);
            GameObject loadObject = Resources.Load<GameObject>(address);
            GameObject unit = Instantiate(loadObject, unitPositions[UnitCount].transform);
            unitList.Add(unit.GetComponent<Unit>());
            unitList[UnitCount].unitInfo = this;
        }
        else
            unitList[UnitCount].Create();

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
        atkRangeSpr.enabled = isOn;

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
            for (int i = UnitCount; i < unitPositions.Length; i++)
            {
                unitList[i].Delete();
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
            entry.ResetUnitClick();
            Init();
            OnClick(false);
        }
        else
        {
            UnitCount--;
            unitList[UnitCount].Delete();
        }
        entry.UnitCountUpdate(-1);
        entry.SpendGold(entry.gameSetting.unitSell[lv]);
    }
}
