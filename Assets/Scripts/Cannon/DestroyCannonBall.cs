using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCannonBall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "CannonBall")
        {
            GameObject ball = collision.gameObject;
            Animator anim = ball.GetComponent<Animator>();
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            anim.SetTrigger("OnCannonCollision");
            Destroy(this.gameObject, 1.0f);
        }
    }
}
