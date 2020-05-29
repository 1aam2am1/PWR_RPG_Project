using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float snailSpeed = 10;
    public LayerMask snailMask;
    Rigidbody2D snailBody;
    Transform myTrans;
    float myWidth, myHeight;

    [SerializeField]
    private GameObject _deadSnailPrefab;
    [SerializeField]
    private GameObject _shellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        myTrans = this.transform;
        snailBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
//Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
       // Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.1f);

        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, snailMask);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.1f, snailMask);
       // Debug.Log(isBlocked);
        if(!isGrounded || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        // Always move forward
        Vector2 myVel = snailBody.velocity;
        myVel.x = - myTrans.right.x * snailSpeed;
        snailBody.velocity = myVel;
    }

    public Vector2 toVector2(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }
    public void Hurt()
    {
        //GameObject deadSnail = Instantiate(_deadSnailPrefab, transform.position, Quaternion.identity);
        //GameObject shell = Instantiate(_shellPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
