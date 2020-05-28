using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    
    private float angle;

    [SerializeField]
    private Sprite leftArrow;
    [SerializeField]
    private Sprite rightArrow;
    [SerializeField]
    private bool left = false;


    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    /****  Transparency
    private bool isActive = false;
    private bool makeTransparent = false;
    private bool killMe = false;
    private float currentTime = 0;
    private Renderer[] renderers;

    public float fadingSpeed = 1f;
    public float startFadingAfterSeconds = 5f;
    */
    private Vector2 initPos;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        if(_rb == null)
        {
            Debug.LogError("Arrow rigid body is null");
        }
       
       // renderers = gameObject.GetComponentsInChildren<Renderer>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_spriteRenderer == null)
        {
            Debug.LogError("Arrow sprite renderer is null");
        }

        if (left)
        {
            _spriteRenderer.sprite = leftArrow;
            _rb.AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
        }

        if (!left)
        {
            _spriteRenderer.sprite = rightArrow;
            _rb.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = _rb.velocity;
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        if (left)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180 + angle));
        } else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
       
        
        /*if (isActive)
        {
            TimeUpdate();
        }

        if (makeTransparent)
        {
            MakeTransparent();
        }

        if (killMe)
        {
            Destroy(gameObject, 5.0f);
        }
        */

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        } else
        {
            float dif = Mathf.Abs(initPos.x - transform.position.x);
            if(dif > 1)
            {
                Destroy(this.gameObject);
            }
           
        }

    }

    /*
    private void MakeTransparent()
    {
        foreach (Renderer renderer in renderers)
        {
            Color color = renderer.material.color;
           // if (color.a > 0)
                //color.a = Mathf.MoveTowards(color.a, 0, Time.deltaTime * fadingSpeed);
            renderer.material.color = color;

            if (color.a <= 0)
            {
                makeTransparent = false;
                killMe = true;
            }
        }
    }

    private void TimeUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= startFadingAfterSeconds)
        {
            isActive = false;
            currentTime = 0;
            makeTransparent = true;
        }
    }*/
}
