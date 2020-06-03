using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovment : MonoBehaviour
{
    [NonSerialized]
    public float _speedX = 1.0f;
    [NonSerialized]
    public float _speedY = 1.0f;

    [SerializeField]
    private GameObject explosion;
    public float _scale = 1;


    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _render;
    private Rigidbody2D _player;
    private Animator _anim;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null)
        {
            Debug.LogError("Cannon ball rigidbody is null");
        }
        _render = GetComponent<SpriteRenderer>();
        if (_render == null)
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
        if (_scale <= 0.01f)
        {
            _scale = 1f;
        }
    }

    private void Update()
    {
        Vector2 v = _rigidbody2D.velocity;
        float angle = Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 posToSpawn = transform.position;
        GameObject newBall = Instantiate(explosion, posToSpawn, Quaternion.identity);
        newBall.transform.localScale = new Vector3(_scale, _scale, _scale);
        Destroy(gameObject);
    }

    public void Run()
    {
        _rigidbody2D.AddForce(new Vector2(_speedX, _speedY), ForceMode2D.Impulse);
    }

}
