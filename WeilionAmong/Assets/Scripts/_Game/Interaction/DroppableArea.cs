using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Handler
{
    public class DroppableArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public virtual void OnDrop(PointerEventData eventData)
        {
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
        }
    }
}