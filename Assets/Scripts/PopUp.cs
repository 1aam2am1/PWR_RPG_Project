using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    //private Sprite renderer;
    private Rigidbody2D rigid;
    private Vector2 force;

    public float torqueMultiplier = 100;
    public Vector2 shardSpeedMultiplier = new Vector2(150, 500);
    // Start is called before the first frame update
    void Start()
    {
        //renderer = gameObject.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        float rndTorqueSpeed = (Random.value + 1) * torqueMultiplier;
        force = new Vector2((Random.value) * shardSpeedMultiplier.x, (Random.value) * shardSpeedMultiplier.y);

        //rigid.AddTorque(rndTorqueSpeed);
        float side = Random.value;

        if (side > 0.5)
            force.x *= (-1);
        else
        {
            force.x *= (1);
        }
        rigid.AddForce(force);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
