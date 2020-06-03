using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAction : AttackAction
{
    private EagleMovement m_eagle;
    private float timeToDamage = 0;

    public float m_Damage = 1f;

    public override void Attack(float time)
    {
        timeToDamage += time;
        HealthSystem system = m_eagle.target.GetComponent<HealthSystem>();

        if (system != null && timeToDamage >= 0f)
        {
            system.TakeDamage(m_Damage);
            timeToDamage = -1f;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        m_eagle = GetComponent<EagleMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
