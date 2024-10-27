using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniTask
{
    public class TaskBedObject : Handler.InputHandler
    {
        [Space(7)]
        [SerializeField] private bool isPillow = false;
    
        public bool IsPillow => isPillow;
        public Image Image { get; private set; }
    
        public bool IsTaskObjectDone { get; set; } = false;

        private void Start()
        {
            Image = GetComponent<Image>();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
        
            Image.raycastTarget = false;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (isDragSupport && !IsTaskObjectDone)
            {
                transform.position = Input.mousePosition;
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
        
            Image.raycastTarget = true;
        }
    }
}