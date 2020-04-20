using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerItemPicker : MonoBehaviour
{
    private InventorySystem inventory;

    protected Collider2D[] hitBuffer = new Collider2D[16];

    private GameObject _item;
    private GameObject item
    {
        get => _item;
        set
        {
            if (value != item)
            {
                if (item != null)
                    item.GetComponent<SpriteOutline>().enabled = false;
            }
            _item = value;
            if (item != null)
            {
                //here display item (mark)
                item.GetComponent<SpriteOutline>().enabled = true;
            }
        }
    }

    public float radius = 3;

    // Start is called before the first frame update
    void Awake()
    {
        inventory = GetComponent<InventorySystem>();
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && item != null)
        {
            Pick();
        }
    }

    private void FixedUpdate()
    {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, radius, hitBuffer, LayerMask.GetMask("Drop Item"));

        var preItem = item;
        if (count != 0)
        {
            item = hitBuffer.Take(count).Where(n => n.GetComponent<ItemGameObject>() != null).OrderBy(n =>
            {
                var dir = n.transform.position - transform.position;
                return dir.sqrMagnitude;
            }).First().gameObject;
        }
        else
        {
            item = null;
        }
    }

    void Pick()
    {
        Item i = item.GetComponent<ItemGameObject>().Item;
        if (inventory.AddItem(i))
        {
            //TODO: Create queue that will place and remove items from to list
            Destroy(item);
            item = null;
        }
    }
}
