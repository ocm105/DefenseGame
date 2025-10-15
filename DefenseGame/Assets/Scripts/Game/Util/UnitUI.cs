using UnityEngine;

public class UnitUI : MonoBehaviour
{
    [SerializeField] UnitUpgrade unitUpgrade;
    public UnitUpgrade UnitUpgrade { get { return unitUpgrade; } }
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        unitUpgrade.mainCam = mainCam;
    }
}
