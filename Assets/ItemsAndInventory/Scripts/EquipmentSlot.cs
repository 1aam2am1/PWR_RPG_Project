using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    public ItemType equipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + "Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;

        return item.itemType == equipmentType;
    }
}
