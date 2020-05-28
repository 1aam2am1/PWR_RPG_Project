using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallMovement : MonoBehaviour
{
    [SerializeField]
    private float _speedX = 8.0f;
    [SerializeField]
    private float _speedY = 1.0f;
    
    [SerializeField]
    private GameObject explosion;

    private float _alpha = 30.0f;
    

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _render;
    private Rigidbody2D _player;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        if(_rigidbody2D == null)
        {
            Debug.LogError("Cannon ball rigidbody is null");
        }
        _render = GetComponent<SpriteRenderer>();
        if(_render == null)
        {
            Debug.LogError("Cannon ball sprite renderer is null");
        }
        _anim = GetComponent<Animator>();
        if (_render == null)
        {
            Debug.LogError("Cannon ball animator is null");
        }
        _player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        if (_render == null)
        {
            Debug.LogError("Player is null");
        }

        float a = _alpha * Mathf.Deg2Rad;
        float movementX = -_speedX * Mathf.Cos(a);
        float movementY = _speedY * Mathf.Sin(a);

        _rigidbody2D.AddForce(new Vector2(movementX, movementY) * 4, ForceMode2D.Impulse);
    }



    /*private void OnBecameVisible()
    {
        float a = _alpha * Mathf.Deg2Rad;
        float movementX = -_speedX * Mathf.Cos(a);
        float movementY = _speedY * Mathf.Sin(a);// - (500 * Mathf.Pow(Time.deltaTime, 2))/2;
                                                                  //transform.Translate(new Vector2(movementX, movementY));
        _rigidbody2D.AddForce(new Vector2(movementX, movementY)*4, ForceMode2D.Impulse);
    }*/

    void Freeze()
    {
        //_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {
           _player.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.angularVelocity = 0;

        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        _render.enabled = false;

        _anim.SetTrigger("OnCannonCollision");*/


        _render.enabled = false;
        Vector2 posToSpawn = transform.position;
        GameObject newBall = Instantiate(explosion, posToSpawn, Quaternion.identity);
        Destroy(this.gameObject);
    }

    // }

    void Unfreeze()
    {
       // _player.constraints = RigidbodyConstraints2D.None;
    }
    void DestroyAfterAnim()
    {
        //Destroy(this.gameObject);
        //_player.constraints = RigidbodyConstraints2D.None;

    }

}
