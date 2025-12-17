using UnityEngine;

public class UnitUI : MonoBehaviour
{
    [SerializeField] UnitUpgrade unitUpgrade;
    public UnitUpgrade UnitUpgrade { get { return unitUpgrade; } }

    private void Start()
    {
        unitUpgrade.SetMainCamera(Camera.main);
    }
}
