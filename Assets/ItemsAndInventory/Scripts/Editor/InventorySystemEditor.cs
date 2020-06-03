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
        m_item.Awake();
    }

    public override void OnInspectorGUI()
    {
        EditorGUIUtility.labelWidth = 100;

        EditorGUI.BeginChangeCheck();
        var he = EditorGUILayout.Toggle("Hide Equipment", m_item.hideEquipment);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Hide Equipment asset");
            m_item.hideEquipment = he;
            EditorUtility.SetDirty(m_item);
        }

        int count = m_item.startingEquipment.Length;
        for (int i = 0; i < count && !m_item.hideEquipment; ++i)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(EquipmentNames[i]);

            Item newValue = (Item)EditorGUILayout.ObjectField(m_item.startingEquipment[i], typeof(Item), allowSceneObjects: false);
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Equipment asset");
                m_item.startingEquipment[i] = newValue;
                EditorUtility.SetDirty(m_item);
            }
        }

        if (!m_item.hideEquipment) { EditorGUILayout.Separator(); }

        EditorGUI.BeginChangeCheck();
        he = EditorGUILayout.Toggle("Hide Inventory", m_item.hideInventory);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Hide Inventory asset");
            m_item.hideInventory = he;
            EditorUtility.SetDirty(m_item);
        }

        EditorGUIUtility.labelWidth = 100;
        count = m_item.startingInventory.Length;
        for (int i = 0; i < count && !m_item.hideInventory; ++i)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Element " + i.ToString());
            Item newValue = (Item)EditorGUILayout.ObjectField(m_item.startingInventory[i], typeof(Item), allowSceneObjects: false);
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Item asset");
                m_item.startingInventory[i] = newValue;
                EditorUtility.SetDirty(m_item);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
