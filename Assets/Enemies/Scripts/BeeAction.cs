using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAction : AtackAction
{
    [SerializeField]
    private GameObject explosion;

    public override void Atack(float time)
    {
        Vector2 posToSpawn = transform.position;
        GameObject newBall = Instantiate(explosion, posToSpawn, Quaternion.identity);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
