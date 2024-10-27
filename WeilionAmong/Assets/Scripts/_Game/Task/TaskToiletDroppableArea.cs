using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniTask
{
    //[RequireComponent(typeof(Image))]
    public class TaskToiletDroppableArea : Handler.DroppableArea
    {
        public override void OnDrop(PointerEventData eventData)
        {
            var droppedObject = eventData.pointerDrag;

            if (droppedObject == null)  return;

            var taskObject = droppedObject.GetComponent<TaskToiletObject>();
            
            if (taskObject == null) return;
            
            taskObject.Image.raycastTarget = false;

            taskObject.IsInToilet = true;
            taskObject.SetScared(true);
        }
    }
}