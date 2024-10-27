using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Handler
{
    public class DroppableArea : MonoBehaviour, IDropHandler
    {
        public virtual void OnDrop(PointerEventData eventData)
        {
        }
    }
}