using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxValue : MonoBehaviour
{
    [SerializeField] protected float m_MaxValue = 100f;
    [SerializeField] private float m_Value = 100f;
    public float Value { get => m_Value; protected set => m_Value = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
