using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] Image draggableItem;

    private ItemSlot draggedSlot;

    private void Awake()
    {
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;

        inventory.OnBeginDragHandlerEvent += BeginDrag;
        equipmentPanel.OnBeginDragHandlerEvent += BeginDrag;

        inventory.OnEndDragHandlerEvent += EndDrag;
        equipmentPanel.OnEndDragHandlerEvent += EndDrag;

        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;

        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }
    private void Equip(ItemSlot itemSlot)
    {
        Item equipableItem = itemSlot.item;
        if (equipableItem != null)
        {
            Equip(equipableItem);
        }
    }
    private void Unequip(ItemSlot itemSlot)
    {
        Item equipableItem = itemSlot.item;
        if (equipableItem != null)
        {
            Unequip(equipableItem);
        }
    }
    public void Equip(Item item)
    {
        if (inventory.RemoveItem(item))
        {
            Item previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }
    public void Unequip(Item item)
    {
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            inventory.AddItem(item);
        }
    }
    private void BeginDrag(ItemSlot itemSlot)
    {
        if (itemSlot.item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.item.itemIcon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }
    private void EndDrag(ItemSlot itemSLot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }
    private void Drag(ItemSlot itemSLot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }
    private void Drop(ItemSlot dropItemSlot)
    {
        if (dropItemSlot.CanReceiveItem(draggedSlot.item) && draggedSlot.CanReceiveItem(dropItemSlot.item))
        {
            Item draggedItem = draggedSlot.item;
            draggedSlot.item = dropItemSlot.item;
            dropItemSlot.item = draggedItem;
        }
    }

}
