using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{ 
    Equipment,
    Food,
    Quest,
    Gold
}


[CreateAssetMenu]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
}
