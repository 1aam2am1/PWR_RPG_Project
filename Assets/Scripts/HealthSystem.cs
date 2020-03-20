﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float m_MaxHealth = 100f;
    [SerializeField] public float Health { get; private set; } = 100f;
    [SerializeField] private float m_HealthRecovery = .1f;




    [Header("Events")]
    [Space]

    public UnityEvent OnDeathEvent;


    private void Awake()
    {
        if (OnDeathEvent == null)
            OnDeathEvent = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Health >= 0.1f)
        {
            Health += m_HealthRecovery * Time.fixedDeltaTime;
        }

        if (Health > m_MaxHealth)
        {
            Health = m_MaxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health < 0.1f)
        {
            Health = 0f;
            OnDeathEvent.Invoke();
        }
    }
}
