using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    [HideInInspector] public int UnitIndex = -1;
    public UnitData UnitData { get; private set; }

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
    public UnitUpgrade unitUpgrade { get; set; }
    private int level = 1;

    private void Awake()
    {
        atkRangeSpr = atkTrigger.GetComponent<SpriteRenderer>();
    }
    private void OnDisable()
    {
        OnClick(false);
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
        if (isFull)
        {
            unitUpgrade.SetPosition(upgradePos.position);
            unitUpgrade.SetUpgrade_Action(OnUpgrade);
        }
        unitUpgrade.SetActive(isFull && isOn);
        if (isOn)
            InGameManager.Instance.UnitStatusOpen(UnitData);
        else
            InGameManager.Instance.UnitStatusClose();
    }
    private void OnUpgrade()
    {
        level++;
        SetData(UnitIndex + level - 1);
        InGameManager.Instance.UnitStatusOpen(UnitData);
        unitUpgrade.SetActive(false);

        UnitCount = 1;
        for (int i = UnitCount; i < unitPositions.Length; i++)
        {
            unitList[i].Delete();
        }
    }
}
