using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public Ref<Item>[] equipment = new Ref<Item>[5].Populate();

    public Ref<Item>[] inventory = new Ref<Item>[12].Populate();

#if UNITY_EDITOR
    [System.NonSerialized] public bool hideEquipment = true;
    [System.NonSerialized] public bool hideInventory = true;
#endif

    // Awake is called after constructor
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

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
}

public static class Extensions
{
    public static T[] Populate<T>(this T[] arr) where T : new()
    {
        for (int i = 0; i < arr.Length; ++i)
        {
            arr[i] = new T();
        }

        return arr;
    }
}