using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
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
            GameView.Instance.Set_UnitSpawnUI(true, this.transform);
            GameView.Instance.Set_UnitSpawnEvent(Spawn);
        }
        else
        {
        }
    }
    private void Spawn()
    {
        if (isUnit == true || unitMono != null) return;

        var data = GameDataManager.Instance.unitData.Where(x => x.strID == "ElfWarrior").FirstOrDefault();
        unitMono = GameView.Instance.GetUnit(data, this.transform);
        isUnit = true;
    }
    private void DeSpawn()
    {
        if (isUnit == false || unitMono == null) return;
        GameView.Instance.ReturnUnitFactroy(unitMono);
        unitMono = null;
        isUnit = false;
    }
}
