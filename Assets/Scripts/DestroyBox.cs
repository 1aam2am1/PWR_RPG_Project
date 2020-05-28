using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    private SpriteRenderer _render;
    [SerializeField]
    private GameObject explosion;
    
    void Start()
    {
        _render = GetComponent<SpriteRenderer>();
        if(_render == null)
        {
            Debug.LogError("Renderer is null");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "CannonBall")
        {
            _render.enabled = false;
            Vector2 posToSpawn = transform.position;
            GameObject newBall = Instantiate(explosion, posToSpawn, Quaternion.identity);
       
            Destroy(this.gameObject);
        }
    }
}
