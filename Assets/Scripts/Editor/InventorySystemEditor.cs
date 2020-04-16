using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventorySystem))]
public class InventorySystemEditor : Editor
{
    InventorySystem m_item;

    string[] EquipmentNames =
    {
        "Helmet",
        "Chest",
        "Weapon",
        "Shield",
        "Boots"
    };

    private void OnEnable()
    {
        m_item = target as InventorySystem;
    }

    public override void OnInspectorGUI()
    {
        EditorGUIUtility.labelWidth = 100;
        m_item.hideEquipment = EditorGUILayout.Toggle("Hide Equipment", m_item.hideEquipment);

        int count = m_item.equipment.Length;
        for (int i = 0; i < count && !m_item.hideEquipment; ++i)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(EquipmentNames[i]);
            Item newValue = (Item)EditorGUILayout.ObjectField(m_item.equipment[i].item, typeof(Item), allowSceneObjects: false);
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Equipment asset");
                m_item.equipment[i].item = newValue;
            }
        }

        if (!m_item.hideEquipment) { EditorGUILayout.Separator(); }

        m_item.hideInventory = EditorGUILayout.Toggle("Hide Inventory", m_item.hideInventory);

        EditorGUIUtility.labelWidth = 100;
        count = m_item.inventory.Length;
        for (int i = 0; i < count && !m_item.hideInventory; ++i)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Element " + i.ToString());
            Item newValue = (Item)EditorGUILayout.ObjectField(m_item.inventory[i].item, typeof(Item), allowSceneObjects: false);
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Item asset");
                m_item.inventory[i].item = newValue;
            }
        }
    }
}
