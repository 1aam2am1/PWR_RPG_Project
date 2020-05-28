using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPushback : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    public float arrowForceX;
    [SerializeField]
    public float cannonForceX;


    public int spikeDmg;
    public float time = 0;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogError("Player rigidbody is null");
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("Player sprite renderer is null");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Arrow"))
        {
            if (time < Time.time)
            {
                _rb.velocity = new Vector2(0, 0);
                
                if(_spriteRenderer.flipX == false)
                {
                    if(arrowForceX > 0.0f)
                    {
                        arrowForceX = arrowForceX * (-1);
                    }
                    
                }

                if (_spriteRenderer.flipX == true)
                {
                    if (arrowForceX < 0.0f)
                    {
                        arrowForceX = arrowForceX * (-1);
                    }

                }

                Vector2 pushBack = new Vector2(arrowForceX, 0);
                _rb.AddForce(pushBack, ForceMode2D.Impulse);
                time = Time.time + 0.2f;
            }
        }
        
        if ((collision.gameObject.tag == "CannonBall"))
        {
            if (time < Time.time)
            {
                _rb.velocity = new Vector2(0, 0);

                if (_spriteRenderer.flipX == false)
                {
                    if (cannonForceX > 0.0f)
                    {
                        cannonForceX = cannonForceX * (-1);
                    }

                }

                if (_spriteRenderer.flipX == true)
                {
                    if (cannonForceX < 0.0f)
                    {
                        cannonForceX = cannonForceX * (-1);
                    }

                }

                Vector2 pushBack = new Vector2(cannonForceX, 0);
                _rb.AddForce(pushBack, ForceMode2D.Impulse);
                time = Time.time + 0.2f;
            }
        }
    }
}
