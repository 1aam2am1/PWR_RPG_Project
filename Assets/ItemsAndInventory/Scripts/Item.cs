using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Quest,
    Gold,
    Helmet,
    Weapon,
    Shield,
    Chest,
    Boots
}

public class StatisticsList
{
    public Dictionary<string, float> dictionary;
}

public class QuestStatistics : StatisticsList { }
public class GoldStatistics : StatisticsList { }
public class WeaponStatistics : StatisticsList
{
    public WeaponStatistics()
    {
        dictionary = new Dictionary<string, float>()
        {
            {"attack", 1f},
            {"deffence", 0f}
        };
    }
}
public class HelmetStatistics : StatisticsList
{
    public HelmetStatistics()
    {
        dictionary = new Dictionary<string, float>()
        {
            {"attack", 1f},
            {"deffence", 0f},
            {"magic deffence", 5f}
        };
    }
}
public class ShieldStatistics : HelmetStatistics { }
public class ChestStatistics : HelmetStatistics { }
public class BootsStatistics : HelmetStatistics { }


[CreateAssetMenu]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;

    public Dictionary<string, float> itemStatistics;

    public Item()
    {
        itemType = ItemType.Food;
        itemStatistics = new Dictionary<string, float>();
    }
}
