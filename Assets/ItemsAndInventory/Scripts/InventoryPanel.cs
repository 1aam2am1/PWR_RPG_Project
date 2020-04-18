using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<ItemSlot> OnDoubleLeftClickEvent;
    public event Action<ItemSlot> OnDoubleRightClickEvent;
    public event Action<ItemSlot> OnBeginDragHandlerEvent;
    public event Action<ItemSlot> OnEndDragHandlerEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {

            itemSlots[i].OnDoubleLeftClickEvent += OnDoubleLeftClickEvent;
            itemSlots[i].OnDoubleRightClickEvent += OnDoubleRightClickEvent;
            itemSlots[i].OnBeginDragHandlerEvent += OnBeginDragHandlerEvent;
            itemSlots[i].OnEndDragHandlerEvent += OnEndDragHandlerEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }
    }

    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
    }

    public void Connect(InventorySystem inventory)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (inventory != null)
            {
                itemSlots[i].RefItem.Reference = inventory.inventory[i];
            }
            else
            {
                itemSlots[i].RefItem.Reference = null;
            }

        }
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                itemSlots[i].item = item;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == item)
            {
                itemSlots[i].item = null;
                return true;
            }
        }
        return false;
    }
    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                return false;
            }
        }
        return true;
    }

}
