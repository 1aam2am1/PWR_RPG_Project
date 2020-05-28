using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> _keys = new List<TKey> { };
    [SerializeField]
    private List<TValue> _values = new List<TValue> { };

    //Unity doesn't know how to serialize a Dictionary
    public Dictionary<TKey, TValue> _myDictionary = new Dictionary<TKey, TValue>();

    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in _myDictionary)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        _myDictionary = new Dictionary<TKey, TValue>();

        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            _myDictionary.Add(_keys[i], _values[i]);
    }
}
