using System;
using Handler;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MiniTask
{
    [RequireComponent(typeof(Image))]
    public class TaskBirdDroppableArea : DroppableArea
    {
        [SerializeField] private TaskBird task ;
        
        [Space(7)]
        [SerializeField] private Animator animator;
        [SerializeField] private float timeChewingDone = 1.5f;

        private bool _isChewing;
        private float _timer;
        
        private static readonly int FoodNear = Animator.StringToHash("FoodNear");
        private static readonly int Feed = Animator.StringToHash("Feed");

        private void FixedUpdate()
        {
            if (_isChewing)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                    IsChewing(false);
            }
        }

        private void IsChewing(bool isChewing)
        {
            _isChewing = isChewing;
            animator.SetBool(Feed, isChewing);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            var droppedObject = eventData.pointerDrag;

            if (droppedObject == null)  return;
            
            if (!droppedObject.name.Equals("Food")) return;

            _timer = timeChewingDone;
            IsChewing(true);
            animator.SetBool(FoodNear, false);
            
            task.TaskCheck();
        }
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            var droppedObject = eventData.pointerDrag;

            if (droppedObject == null)  return;

            if (!droppedObject.name.Equals("Food")) return;
            
            animator.SetBool(FoodNear, true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (!_isChewing)
                animator.SetBool(FoodNear, false);
        }
    }
}