using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] GameView view;
    private Button button;

    private UnitBase unitMono;
    private bool isUnit = false;

    private void Awake()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClick_Btn);
    }

    private void OnClick_Btn()
    {
        if (isUnit == false)
        {
            view.Set_UnitSpawnUI(true, this.transform);
            view.Set_UnitSpawnEvent(Spawn);
        }
        else
        {

        }
    }
    private void Spawn()
    {
        if (isUnit == true || unitMono != null) return;

        UnitData data = GameDataManager.Instance.unitData.Where(x => x.Index == 2000010).FirstOrDefault();
        unitMono = view.characterFactory.GetUnit(data, this.transform);
        isUnit = true;
    }
    private void DeSpawn()
    {
        if (isUnit == false || unitMono == null) return;
        view.characterFactory.ReturnUnit(unitMono);
        unitMono = null;
        isUnit = false;
    }
}
