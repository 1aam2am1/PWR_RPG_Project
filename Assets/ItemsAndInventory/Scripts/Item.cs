using System;
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

public class FoodStatistics : StatisticsList
{
    public FoodStatistics()
    {
        dictionary = new Dictionary<string, float>()
        {
            {"hpBonus", 1f}
        };
    }
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
    [Space]
    public string itemDescription;
    public Sprite itemIcon;

    public Sprite itemSpriteEquipped;

    [Serializable]
    public class SerializableDictionary : SerializableDictionary<string, float>
    {
        public SerializableDictionary()
        {
        }

        public SerializableDictionary(IDictionary<string, float> dictionary)
        {
            _myDictionary = new Dictionary<string, float>(dictionary);
        }
    }
    public SerializableDictionary itemStatistics;


    public Item()
    {
        itemType = ItemType.Food;
        itemStatistics = new SerializableDictionary();
    }

    public Item(ItemType type)
    {
        itemType = type;
        itemStatistics = new SerializableDictionary();
    }
}
