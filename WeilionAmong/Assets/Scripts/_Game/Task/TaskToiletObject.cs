using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniTask
{
    public class TaskToiletObject : Handler.InputHandler
    {
        private Animator _animator;

        private static readonly int IsScared = Animator.StringToHash("IsScared");
        
        public Image Image { get; private set; }
        public bool IsInToilet { get; set; } = false;

        private void Start()
        {
            Image = GetComponent<Image>();
            _animator = GetComponent<Animator>();

            SetScared(false);
        }

        public void SetScared(bool isScared)
        {
            _animator.SetBool(IsScared, isScared);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
        
            Image.raycastTarget = false;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (isDragSupport && !IsInToilet)
            {
                transform.position = Input.mousePosition;
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
        
            Image.raycastTarget = true;

            if (IsInToilet)
                Image.raycastTarget = false;
        }
    }
}