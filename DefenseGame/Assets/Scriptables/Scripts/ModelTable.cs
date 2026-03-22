using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModelTableInfo
{
    public string id;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "ModelTable", menuName = "ScriptableObjects/ModelTable")]
public class ModelTable : ScriptableObject
{
    private static ModelTable instance = null;
    public static ModelTable Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load("Scriptables/ModelTable") as ModelTable;
            }
            return instance;
        }
    }

    [Header("¿Ø¥÷")]
    public List<ModelTableInfo> unitModelTable;

    [Header("∏ÛΩ∫≈Õ")]
    public List<ModelTableInfo> monsterModelTable;
}
