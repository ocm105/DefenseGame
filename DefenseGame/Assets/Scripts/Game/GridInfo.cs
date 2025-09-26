using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    [SerializeField] UnitGrid[] unitGrids;
    public UnitGrid[] UnitGrids { get { return unitGrids; } }
}
