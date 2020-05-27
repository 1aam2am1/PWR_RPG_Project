using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyHealthAttribute : MonoBehaviour
{
    public float m_Damage = 1f;
    public float m_KnockBack = 0f;

    private List<GameObject> m_Colliding;

    private void Awake()
    {
        m_Colliding = new List<GameObject>();
    }
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

        if (system != null && !m_Colliding.Contains(collision.gameObject))
        {
            system.TakeDamage(m_Damage);

            m_Colliding.Add(collision.gameObject);

            var rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                var v = (Vector2)(collision.transform.position - transform.position);
                rigidbody.AddForce(m_KnockBack * v.normalized, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_Colliding.Remove(collision.gameObject);
    }
}
