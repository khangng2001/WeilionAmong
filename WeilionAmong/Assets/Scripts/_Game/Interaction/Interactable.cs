using MiniTask;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private EnumInteractable interactableType;

    [SerializeField] protected EnumTask taskType;

    public void OnPlayerInteracted(Player player)
    {
        if (interactableType == EnumInteractable.Task)
        {
            //instantiate equivalent UI
            GameObject popup = ResourceLoader.Get<GameObject>("Popup_Task_" + taskType.ToString());
            GameObject ui = Instantiate(popup, player.popupParent);
            
            //set data for UI
            ITask task = ui.GetComponent<ITask>();
            task.Player = player;
            
            
            
            return;
        }

        if (interactableType == EnumInteractable.Report)
        {
            //
            
            return;
        }
        
        if (interactableType == EnumInteractable.Vent)
        {
            //go into Vent
            
        }
    }
}