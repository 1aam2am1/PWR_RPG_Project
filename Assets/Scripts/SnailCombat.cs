using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailCombat : MonoBehaviour
{
    [SerializeField]
    private GameObject _deadSnailPrefab;
    [SerializeField]
    private GameObject _shellPrefab;
    public float m_Damage = 1f;
    public float m_KnockBack = 10f;
    // private Snail snail;
    private void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D playerRigid = collision.collider.GetComponent<Rigidbody2D>();
            HealthSystem system = collision.gameObject.GetComponent<HealthSystem>();
            Snail snail = this.gameObject.GetComponent<Snail>();
            if (playerRigid != null && system != null)
            {
                foreach (ContactPoint2D point in collision.contacts)
                {
                    //Debug.Log(point.normal);
                    Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);
                    Debug.Log(point.normal.y);
                    if (point.normal.y <= -0.9f)
                    {
                        float jumpVelocity = 15;

                        playerRigid.AddForce(jumpVelocity * Vector2.up, ForceMode2D.Impulse);
                        snail.Hurt();
                        GameObject deadSnail = Instantiate(_deadSnailPrefab, transform.position, Quaternion.identity);
                        GameObject shell = Instantiate(_shellPrefab, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        /*float jumpVelocity = 10;
                        Vector2 velocity = playerRigid.velocity;
                        velocity.x = point.normal.x * jumpVelocity;
                        velocity.y = point.normal.y * jumpVelocity;
                        playerRigid.velocity = velocity;*/
                        var v = (Vector2)(collision.transform.position - transform.position);
                        playerRigid.AddForce(m_KnockBack * v.normalized, ForceMode2D.Impulse);
                        system.TakeDamage(m_Damage);
                    }
                }
            }
            else
            {
                Debug.LogError("Player Rigidbody or HealthSystem is null");
            }
        }
    }
}
