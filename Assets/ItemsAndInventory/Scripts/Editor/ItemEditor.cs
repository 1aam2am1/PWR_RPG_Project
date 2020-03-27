using System.Collections;
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
        EditorGUI.BeginChangeCheck();
        ItemType itemType = (ItemType)EditorGUILayout.EnumPopup("Item Type", m_item.itemType);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Item type");
            m_item.itemType = itemType;

            OnTypeChange();
        }

        EditorGUI.BeginChangeCheck();
        string newName = EditorGUILayout.TextField("Name", m_item.itemName);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Name");
            m_item.itemName = newName;
        }

        EditorGUI.BeginChangeCheck();
        string newDescription = EditorGUILayout.TextField("Description", m_item.itemDescription);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Description");
            m_item.itemName = newDescription;
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
        }

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        EditorGUIUtility.labelWidth = 64;
        List<string> keys = new List<string>(m_item.itemStatistics.Keys);
        foreach (string key in keys)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(key);
            float newValue = EditorGUILayout.FloatField(m_item.itemStatistics[key]);
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed value");
                m_item.itemStatistics[key] = newValue;
            }
        }
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
                m_item.itemStatistics = new Dictionary<string, float>(l.dictionary);

                if (oldStatistics != null)
                {
                    foreach (var item in oldStatistics)
                    {
                        if (m_item.itemStatistics.ContainsKey(item.Key))
                        {
                            m_item.itemStatistics[item.Key] = item.Value;
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
