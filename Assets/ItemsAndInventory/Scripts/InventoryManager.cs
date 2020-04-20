using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventoryPanel inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] Image draggableItem;

    private ItemSlot draggedSlot;

    private void Awake()
    {
        if (inventory == null)
            inventory = transform.Find("Inventory").GetComponent<InventoryPanel>();

        if (equipmentPanel == null)
            equipmentPanel = transform.Find("Equipment/EquipmentPanel").GetComponent<EquipmentPanel>();

        if (draggableItem == null)
            draggableItem = transform.Find("DraggableItem").GetComponent<Image>();

        inventory.OnDoubleLeftClickEvent += Use;
        //equipmentPanel.OnDoubleLeftClickEvent += Unequip;

        inventory.OnDoubleRightClickEvent += ThrowOut;
        equipmentPanel.OnDoubleRightClickEvent += Unequip;

        inventory.OnBeginDragHandlerEvent += BeginDrag;
        equipmentPanel.OnBeginDragHandlerEvent += BeginDrag;

        inventory.OnEndDragHandlerEvent += EndDrag;
        equipmentPanel.OnEndDragHandlerEvent += EndDrag;

        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;

        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }

    private void Use(ItemSlot itemSlot)
    {

        if (itemSlot.item != null)
        {
            if (itemSlot.item.itemType == ItemType.Helmet ||
                itemSlot.item.itemType == ItemType.Chest ||
                itemSlot.item.itemType == ItemType.Weapon ||
                itemSlot.item.itemType == ItemType.Shield ||
                itemSlot.item.itemType == ItemType.Boots)
            {
                Debug.Log("Start using as eqiupment");
                Equip(itemSlot);
                Debug.Log("Item  used as equipment");
            }

            else if (itemSlot.item.itemType == ItemType.Food)
            {
                Debug.Log("Start using as consumable");
                Consume(itemSlot);
                Debug.Log("Item  used as consumable");
            }
        }
    }
    private void ThrowOut(ItemSlot itemSlot)
    {
        if (itemSlot.item != null)
        {
            //function
            inventory.RemoveItem(itemSlot.item);
            Debug.Log("Item thrown out");
        }
    }
    private void Consume(ItemSlot itemSlot)
    {

        //function updating player stats
        inventory.RemoveItem(itemSlot.item);
        Debug.Log("Item consumed");
    }
    private void Equip(ItemSlot itemSlot)
    {
        Item item = itemSlot.item;
        if (item != null)
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
            Debug.Log("Item equipped");
        }
    }
    private void Unequip(ItemSlot itemSlot)
    {
        Item item = itemSlot.item;
        if (item != null)
        {
            if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
            {
                inventory.AddItem(item);
            }
            Debug.Log("Item unequipped");
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
        if (draggedSlot != null)
        {
            if (dropItemSlot.CanReceiveItem(draggedSlot.item) &&
               draggedSlot.CanReceiveItem(dropItemSlot.item))
            {
                Item draggedItem = draggedSlot.item;
                draggedSlot.item = dropItemSlot.item;
                dropItemSlot.item = draggedItem;
            }
        }
    }

    public void Connect(InventorySystem inventorySystem)
    {
        inventory.Connect(inventorySystem);
        equipmentPanel.Connect(inventorySystem);

    }

}
