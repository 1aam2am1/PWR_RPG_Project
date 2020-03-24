using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] Image image;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragHandlerEvent;
    public event Action<ItemSlot> OnEndDragHandlerEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(0, 0, 0, 0);


    private Item _item;
    public Item item 
    { 
        get { return _item; }
        set
        {
            _item = value;
            if (item == null)
            {
                image.color = disabledColor;
            }
            else
            {
                image.sprite = _item.itemIcon;
                image.color = normalColor;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();

    }

    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }

    Vector2 originalPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragHandlerEvent != null)
            OnBeginDragHandlerEvent(this);
        //originalPosition = image.transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragHandlerEvent != null)
            OnEndDragHandlerEvent(this);
        //image.transform.position = originalPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
            OnDragEvent(this);
        //image.transform.position = Input.mousePosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent!= null)
            OnDropEvent(this);
    }
}
