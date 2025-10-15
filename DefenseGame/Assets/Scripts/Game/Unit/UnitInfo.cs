using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    private string LevelSource = "Image/Level/";
    [HideInInspector] public int UnitIndex = -1;
    public UnitData UnitData { get; private set; }

    [SerializeField] GameObject[] unitPositions;
    private List<Unit> unitList = new List<Unit>();
    public int UnitCount { get; private set; } = 0;
    public bool isFull { get { return UnitCount >= 3; } }

    [SerializeField] SpriteRenderer level;
    [SerializeField] Transform upgradePos;
    [SerializeField] UnitAttackTrigger atkTrigger;
    public UnitAttackTrigger AtkTrigger { get { return atkTrigger; } }
    private SpriteRenderer atkRangeSpr;
    public UnitUpgrade unitUpgrade { get; set; }

    private void Awake()
    {
        atkRangeSpr = atkTrigger.GetComponent<SpriteRenderer>();
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
        atkTrigger.transform.localScale = new Vector3(UnitData.Range, UnitData.Range);
        level.sprite = Resources.Load<Sprite>($"{LevelSource}{UnitData.Level}");
    }
    public void SetPosition(Transform parent)
    {
        this.transform.parent = parent;
        this.transform.localPosition = Vector2.zero;
    }
    public void UnitCreate()
    {
        if (unitList.Count < 3)
        {
            GameObject unit = Instantiate(Resources.Load<GameObject>(UnitResource.GetPrefab(UnitData.Resource)), unitPositions[UnitCount].transform);
            unitList.Add(unit.GetComponent<Unit>());
            unitList[UnitCount].unitInfo = this;
        }
        else
            unitList[UnitCount].Create();

        UnitCount++;
        this.gameObject.SetActive(true);
    }

    /// <summary> Unit 클릭 </summary>
    public void OnClick(bool isOn = true)
    {
        atkRangeSpr.enabled = isOn;
        level.gameObject.SetActive(isOn);

        if (isFull)
        {
            unitUpgrade.SetPosition(upgradePos.position);
            unitUpgrade.SetUpgrade_Action(OnUpgrade);
        }
        unitUpgrade.SetActive(isFull && isOn);
    }
    private void OnUpgrade()
    {
        UnitCount = 1;
        for (int i = UnitCount; i < unitPositions.Length; i++)
        {
            unitList[i].Delete();
        }
        UnitData.AttackSpeed *= 0.5f;
        InGameManager.Instance.UnitStatusOpen(this);
        unitUpgrade.SetActive(false);
    }
}
