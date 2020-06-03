using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttackMode : AttackAction
{
    private GameObject myPrefab;

    private float speed = 10;
    private float timeToDamage = 0;

    private Ref<Item> _Item;
    public Ref<Item> Item
    {
        get => _Item;
        set
        {
            if (_Item != null)
            {
                _Item.OnValueChange -= OnValueChange;
            }

            _Item = value;
            if (_Item != null)
            {
                _Item.OnValueChange += OnValueChange;
            }
        }
    }

    public override void TimeNotAttack(float time)
    {
        timeToDamage += time;
    }

    public override void Attack(float time)
    {
        timeToDamage += time;

        if (timeToDamage <= 0f)
        {
            return;
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 distance = mousePosition - (Vector2)transform.position;
        //float angle = Vector3.Angle(transform.position, mousePosition);
        distance.Normalize();

        GameObject newBall = Instantiate(myPrefab, transform.position, Quaternion.identity);
        BulletMovment m = newBall.GetComponent<BulletMovment>();
        m._speedX = distance.x * speed;
        m._speedY = distance.y * speed;
        m.Run();

        timeToDamage = -0.25f;
    }

    // Start is called before the first frame update
    public void Awake()
    {
        Item = transform.parent.GetComponent<InventorySystem>().equipment[2];
        myPrefab = Resources.Load<GameObject>("Bullet");
    }

    void OnValueChange()
    {
        if (Item.Item != null)
        {
            //here get info
        }
    }
}
