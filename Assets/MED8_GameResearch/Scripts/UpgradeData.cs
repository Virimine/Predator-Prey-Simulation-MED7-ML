using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade")]
public class UpgradeData : ScriptableObject {
    public string upgradeName;
    [TextArea] public string description;
    public int cost;
    public UpgradeType type;

    public FurnitureType furnitureType;     // Chair, Table, Sofa (for class/interface)
    public FurnitureStyle styleType;         // Modern, Boho, Scandi (for styles)
}

public enum UpgradeType {
    UnlockClass, UnlockStyle, UnlockInterface
}
