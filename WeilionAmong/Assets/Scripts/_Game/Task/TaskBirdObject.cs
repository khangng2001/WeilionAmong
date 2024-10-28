using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniTask
{
    public class TaskBirdObject : Handler.InputHandler
    {
        [SerializeField] private GameObject wormGo;
        [SerializeField] private Image wormImg;
        
        private Transform _wormTransform;

        private void Start()
        {
            _wormTransform = wormGo.transform;
            wormGo.SetActive(false);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
        
            _wormTransform.position = Input.mousePosition;
            
            wormGo.SetActive(true);
            wormImg.raycastTarget = false;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (isDragSupport)
            {
                _wormTransform.position = Input.mousePosition;
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
        
            wormGo.SetActive(false);
            
            wormImg.raycastTarget = false;
        }
    }
}