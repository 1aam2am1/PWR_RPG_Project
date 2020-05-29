using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailCombat : MonoBehaviour
{
    [SerializeField]
    private GameObject _deadSnailPrefab;
    [SerializeField]
    private GameObject _shellPrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Snail snail = collision.collider.GetComponent<Snail>();
        if(snail!=null)
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                Debug.Log(point.normal);
                Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);
                if(point.normal.y >= 0.9f)
                {
                    float jumpVelocity = 10;
                    Rigidbody2D myBody = this.GetComponent<Rigidbody2D>();
                    Vector2 velocity = myBody.velocity;
                    velocity.y = jumpVelocity;
                    myBody.velocity = velocity;
                    snail.Hurt();
                    GameObject deadSnail = Instantiate(_deadSnailPrefab, transform.position, Quaternion.identity);
                    GameObject shell = Instantiate(_shellPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    float jumpVelocity = 10;
                    Rigidbody2D myBody = this.GetComponent<Rigidbody2D>();
                    Vector2 velocity = myBody.velocity;
                    velocity.x = point.normal.x * jumpVelocity;
                    velocity.y = point.normal.y * jumpVelocity;
                    myBody.velocity = velocity;
                }
            }
        }
    }
}
