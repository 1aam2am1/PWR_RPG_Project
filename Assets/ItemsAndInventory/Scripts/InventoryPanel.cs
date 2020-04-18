using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] List<Item> startingItems;
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
        SetStartingItems();
    }

    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

        SetStartingItems();
    }

    private void SetStartingItems()
    {
        int i = 0;
        for (; i < startingItems.Count && i < itemSlots.Length; i++)
            itemSlots[i].item = startingItems[i];

        for (; i < itemSlots.Length; i++)
            itemSlots[i].item = null;
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
