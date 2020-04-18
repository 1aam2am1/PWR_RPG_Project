using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public bool hideEquipment;
    public bool hideInventory;


    public Item[] startingEquipment;
    public Item[] startingInventory;



    [System.NonSerialized] public Ref<Item>[] equipment;
    [System.NonSerialized] public Ref<Item>[] inventory;

    public void Awake()
    {
        if (startingEquipment == null)
            startingEquipment = new Item[5];
        //startingEquipment.Resize(5);

        if (startingInventory == null)
            startingInventory = new Item[12];
        //startingInventory.Resize(12);

        if (equipment == null)
            equipment = new Ref<Item>[5].Populate();

        if (inventory == null)
            inventory = new Ref<Item>[12].Populate();
    }
    // Start is called before the first frame update
    void Start()
    {
        SyncFromStart();

        if (Application.isEditor)
        {
            foreach (var e in equipment)
            {
                e.OnValueChange += SyncToSave;
            }
            foreach (var e in inventory)
            {
                e.OnValueChange += SyncToSave;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].Item == null)
            {
                inventory[i].Item = item;
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].Item == null)
            {
                return false;
            }
        }
        return true;
    }

    public void SyncFromStart()
    {
        for (int i = 0; i < equipment.Length; i++)
        {
            equipment[i].Item = startingEquipment[i];
        }

        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].Item = startingInventory[i];
        }
    }

    public void SyncToSave()
    {
        for (int i = 0; i < equipment.Length; i++)
        {
            startingEquipment[i] = equipment[i].Item;
        }

        for (int i = 0; i < inventory.Length; i++)
        {
            startingInventory[i] = inventory[i].Item;
        }
    }
}

public static class Extensions
{
    public static T[] Populate<T>(this T[] arr) where T : new()
    {
        for (int i = 0; i < arr.Length; ++i)
        {
            if (arr[i] == null)
                arr[i] = new T();
        }

        return arr;
    }

    public static List<T> Resize<T>(this List<T> list, int sz) where T : new()
    {
        int cur = list.Count;
        if (sz < cur)
            list.RemoveRange(sz, cur - sz);
        else if (sz > cur)
        {
            if (sz > list.Capacity)//this bit is purely an optimisation, to avoid multiple automatic capacity changes.
                list.Capacity = sz;
            for (; cur < sz; ++cur)
            {
                list.Add(new T());
            }

        }
        return list;
    }
}