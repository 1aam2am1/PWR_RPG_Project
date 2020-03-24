using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Weapon,
    Shield,
    Chest,
    Boots
}

[CreateAssetMenu]
public class EquipableItem : Item
{
    public int attackBonus;
    public int hpBonus;
    public int defence3Bonus;
    [Space]
    public EquipmentType equipmentType;

}
