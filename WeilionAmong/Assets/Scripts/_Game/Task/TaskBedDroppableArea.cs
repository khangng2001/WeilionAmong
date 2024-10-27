using Handler;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniTask
{
    [RequireComponent(typeof(Image))]
    public class TaskBedDroppableArea : DroppableArea
    {
        [SerializeField] private TaskBed taskBed;
    
        [SerializeField] private bool isPillowPlace;
    
        private bool _isFull;

        public override void OnDrop(PointerEventData eventData)
        {
            var droppedObject = eventData.pointerDrag;

            if (droppedObject == null)  return;
        
            var taskBedObject = droppedObject.GetComponent<TaskBedObject>();
        
            if (taskBedObject == null) return;
        
        
            if (taskBedObject.IsPillow && isPillowPlace && !_isFull)
            {
                _isFull = true;
            
                taskBedObject.Image.raycastTarget = false;
            
                Transform t = taskBedObject.transform;
                t.transform.SetParent(transform);
                t.transform.localPosition = Vector3.zero;
            
                //
            
                taskBedObject.IsTaskObjectDone = true;
                
                //
                taskBed.TaskCheck();
            
                return;
            }

            if (!taskBedObject.IsPillow && !isPillowPlace)
            {
                droppedObject.SetActive(false);
            
                taskBedObject.IsTaskObjectDone = true;
                
                //
                taskBed.TaskCheck();
            }
        }
    }
}