using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Weapon1,
    Weapon2,
    Chest,
    Boots,
    Accessory1,
    Accessory2,
    Accessory3
}

[CreateAssetMenu]
public class EquipableItem : Item
{
    public int stat1Bonus;
    public int stat2Bonus;
    public int stat3Bonus;
    public int stat4Bonus;
    [Space]
    public float stat1PercentBonus;
    public float stat2PercentBonus;
    public float stat3PercentBonus;
    public float stat4PercentBonus;
    [Space]
    public EquipmentType equipmentType;

}
