using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    private string unitSource = Constants.Character.Unit + "/Unit";
    private string LevelSource = "Image/Level/";
    [HideInInspector] public InGameManager inGameManager;
    [HideInInspector] public int UnitIndex = -1;
    private UnitData unitData;
    public UnitData UnitData { get { return unitData; } }

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

    [SerializeField] RectTransform unitParent;
    [SerializeField] RectTransform dragPos;
    [SerializeField] RectTransform rangePos;
    [SerializeField] RectTransform levelpos;
    [SerializeField] RectTransform upGradePos;
    public UnitAttackTrigger AtkTrigger { get; private set; }
    [SerializeField] GameObject rangeObject;
    [SerializeField] Image level;
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
    public void SetPosition(Vector2 pos)
    {
        unitParent.anchoredPosition = pos;
        dragPos.anchoredPosition = pos;
        // rangePos.anchoredPosition = pos;
        // levelpos.anchoredPosition = pos;
        // upGradePos.anchoredPosition = pos;
    }
    public async UniTaskVoid MovePosition(Vector2 pos)
    {
        while (Vector2.Distance(unitParent.anchoredPosition, pos) > 0.05f)
        {
            unitParent.anchoredPosition = Vector2.MoveTowards(unitParent.anchoredPosition, pos, Time.deltaTime * 1000);
            await UniTask.Yield();
        }
    }
    public void UnitCreate()
    {
        for (int i = 0; i < unitPositions.Length; i++)
        {
            if (unitPositions[i].transform.childCount <= 0)
            {
                Instantiate(Resources.Load<GameObject>(unitSource), unitPositions[i].transform);
                break;
            }
        }
        this.gameObject.SetActive(true);
    }

    /// <summary> Unit 클릭 </summary>
    public void OnClick(bool isOn = true)
    {
        rangeObject.SetActive(isOn);
        upgradBtn.gameObject.SetActive(isOn);
    }
    private void OnUpgrade()
    {
        unitData = GameDataManager.Instance.unitData[UnitIndex + 1];
        unitData.AttackSpeed *= 0.5f;
        upgradBtn.interactable = false;
        inGameManager.UnitUpdate(this);
    }
}
