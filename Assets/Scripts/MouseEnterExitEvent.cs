using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterExitEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler OnMouseEnterHandler;
    public event EventHandler OnMouseExitHandler;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnterHandler?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExitHandler?.Invoke(this, EventArgs.Empty);
    }
}
