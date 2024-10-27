using System;
using System.Collections;
using System.Collections.Generic;
using Task;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskBedObject : Handler.InputHandler
{
    [SerializeField] private Image image;
    
    [Space(7)]
    [SerializeField] private bool isPillow = false;
    
    public bool IsPillow => isPillow;
    public Image Image => image;
    
    public bool IsTaskObjectDone { get; set; } = false;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        
        image.raycastTarget = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        
        image.raycastTarget = true;
    }
}