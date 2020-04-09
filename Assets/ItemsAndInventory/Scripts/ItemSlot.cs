using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image image;
    [SerializeField] Tooltip tooltip;

    //public event Action<ItemSlot> OnPointerEnterEvent;
    //public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnDoubleLeftClickEvent;
    public event Action<ItemSlot> OnDoubleRightClickEvent;
    public event Action<ItemSlot> OnBeginDragHandlerEvent;
    public event Action<ItemSlot> OnEndDragHandlerEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;


    private Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (item == null)
            {
                image.color = Color.clear;
            }
            else
            {
                image.sprite = _item.itemIcon;
                image.color = Color.white;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right && eventData.clickCount == 2)
        {
            OnDoubleRightClickEvent?.Invoke(this);
        }

        if (eventData != null && eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
        {
            OnDoubleLeftClickEvent?.Invoke(this);
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (tooltip == null)
            tooltip = FindObjectOfType<Tooltip>();

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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragHandlerEvent != null)
            OnEndDragHandlerEvent(this);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
            OnDragEvent(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(item);
        
        tooltip.transform.position = this.transform.position;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
