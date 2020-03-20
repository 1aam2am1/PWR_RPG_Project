using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyHealthAttribute : MonoBehaviour
{
    public float m_Damage = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthSystem system = collision.gameObject.GetComponent<HealthSystem>();
        if (system != null)
        {
            system.TakeDamage(m_Damage);
        }
    }
}
