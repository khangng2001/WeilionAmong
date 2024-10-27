using UnityEngine;
using UnityEngine.EventSystems;

namespace Handler
{
    public class InputHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] protected bool isClickSupport;
        [SerializeField] protected bool isDragSupport;
        
        protected bool _isDragging;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
        }
    }
}