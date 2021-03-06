﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Item))]
public class ItemMyEditor : Editor
{
    Item m_item;

    private void OnEnable()
    {
        m_item = target as Item;
        OnTypeChange();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        ItemType itemType = (ItemType)EditorGUILayout.EnumPopup("Item Type", m_item.itemType);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Item type");
            m_item.itemType = itemType;
            EditorUtility.SetDirty(m_item);

            OnTypeChange();
        }

        EditorGUI.BeginChangeCheck();
        string newName = EditorGUILayout.TextField("Name", m_item.itemName);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Name");
            m_item.itemName = newName;
            EditorUtility.SetDirty(m_item);
        }

        EditorGUI.BeginChangeCheck();
        string newDescription = EditorGUILayout.TextField("Description", m_item.itemDescription);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Description");
            m_item.itemDescription = newDescription;
            EditorUtility.SetDirty(m_item);
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Source Image");
        Sprite newIcon = (Sprite)EditorGUILayout.ObjectField(m_item.itemIcon, typeof(Sprite), allowSceneObjects: false);
        EditorGUILayout.EndHorizontal();
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Icon");
            m_item.itemIcon = newIcon;
            EditorUtility.SetDirty(m_item);
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Equipped sprite");
        Sprite newEquippedSprite = (Sprite)EditorGUILayout.ObjectField(m_item.itemSpriteEquipped, typeof(Sprite), allowSceneObjects: false);
        EditorGUILayout.EndHorizontal();
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Equipped Sprite");
            m_item.itemSpriteEquipped = newEquippedSprite;
            EditorUtility.SetDirty(m_item);
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUIUtility.labelWidth = 100;
        List<string> keys = new List<string>(m_item.itemStatistics._myDictionary.Keys);
        foreach (string key in keys)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(key);
            float newValue = EditorGUILayout.FloatField(m_item.itemStatistics._myDictionary[key]);
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed value");
                m_item.itemStatistics._myDictionary[key] = newValue;
                EditorUtility.SetDirty(m_item);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    //small hack to search all directories and not only Editor directory
    private static readonly string appendix = GetAppendix();
    private static string GetAppendix()
    {
        string name = typeof(WeaponStatistics).AssemblyQualifiedName;
        return name.Replace("WeaponStatistics", "Statistics");
    }

    void OnTypeChange()
    {
        var enumName = m_item.itemType.ToString();

        string name = enumName + appendix;

        Type t = Type.GetType(name);

        if (t == null)
        {
            Debug.LogError("Class: " + name + " don't exists. Can't get dictionary data");
        }
        else
        {
            object list = Activator.CreateInstance(t);

            if (list is StatisticsList l)
            {
                var oldStatistics = m_item.itemStatistics;
                m_item.itemStatistics = new Item.SerializableDictionary(l.dictionary);

                if (oldStatistics != null)
                {
                    foreach (var item in oldStatistics._myDictionary)
                    {
                        if (m_item.itemStatistics._myDictionary.ContainsKey(item.Key))
                        {
                            m_item.itemStatistics._myDictionary[item.Key] = item.Value;
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Class: " + name + " isn't child of StatisticsList. Can't get dictionary data");
            }
        }
    }
}
