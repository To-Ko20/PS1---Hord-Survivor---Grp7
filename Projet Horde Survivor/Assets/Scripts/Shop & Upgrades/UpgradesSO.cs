using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New upgrade", menuName = "Scriptable Objects/UpgradesSO")]
public class UpgradesSO : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string upgradeDescription;
    
    [Header("Cost")]
    public List<ulong> cost;
    
    [Header("Effects")]
    public List<GameObject> effects;
}