using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ref<T> where T : ScriptableObject
{
    private T m_GetT;

    public event Action OnValueChange;

    public T GetT
    {
        get => m_GetT;
        set
        {
            if (m_GetT != value)
            {
                m_GetT = value;
                if (Reference != null)
                {
                    Reference.GetT = value;
                }
                OnValueChange?.Invoke();
            }
        }
    }

    public T Item { get => GetT; set => GetT = value; }

    [System.NonSerialized] private Ref<T> m_reference = null;
    public Ref<T> Reference
    {
        get => m_reference;
        set
        {
            if (m_reference == value) { return; }

            var tym = m_reference;
            m_reference = null;
            if (tym != null)
            {
                tym.Reference = null;
            }

            m_reference = value;
            if (m_reference != null)
            {
                m_reference.Reference = this;
                m_reference.Item = Item;
            }
        }
    }
}
