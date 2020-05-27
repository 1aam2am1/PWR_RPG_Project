using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttachment : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;

    public bool flipX
    {
        get => m_spriteRenderer.flipX;
        set
        {
            if (m_spriteRenderer.flipX != value)
            {
                transform.localPosition = new Vector3(transform.localPosition.x * -1, transform.localPosition.y, transform.localPosition.z);
                m_spriteRenderer.flipX = value;
            }
        }
    }

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

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Item = transform.parent.GetComponent<InventorySystem>().equipment[2];
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Better flip X, Y and mouse angle
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 distance = mousePosition - (Vector2)transform.position;
        //float angle = Vector3.Angle(transform.position, mousePosition);
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        //if facing left
        if (flipX)
        {
            angle += 180;
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //transform.LookAt(mousePosition);
        Debug.DrawRay(transform.position, distance, Color.red);
    }

    void OnValueChange()
    {
        if (Item.Item != null)
        {
            m_spriteRenderer.sprite = Item.Item.itemSpriteEquipped != null ? Item.Item.itemSpriteEquipped : Item.Item.itemIcon;
        }
        else
        {
            m_spriteRenderer.sprite = null;
        }
    }
}
