using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Item _Item;

    public Item Item
    {
        get => _Item;
        set
        {
            _Item = value;
            OnValueChange();
        }
    }


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        OnValueChange();
    }

    private void OnValueChange()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Item == null)
        {
            if (spriteRenderer.color != Color.clear)
                spriteRenderer.color = Color.clear;
        }
        else
        {
            if (spriteRenderer.sprite != Item.itemIcon)
                spriteRenderer.sprite = Item.itemIcon;

            if (spriteRenderer.color != Color.white)
                spriteRenderer.color = Color.white;
        }
    }
}
