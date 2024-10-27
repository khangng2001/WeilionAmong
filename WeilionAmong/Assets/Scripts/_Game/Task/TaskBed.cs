using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MiniTask
{
    public class TaskBed : Interactable, ITask
    {
        [SerializeField] private GameObject taskDoneImg;
        
        [SerializeField] private List<TaskBedObject> taskObjects = new();
        
        private Button _taskButton;
        
        public Player Player { get; set; }

        private void OnEnable()
        {
            _taskButton = GetComponent<Button>();
            _taskButton.onClick.AddListener(ClosePopup);
            
            taskDoneImg.SetActive(false);
        }

        private void OnDisable()
        {
            _taskButton.onClick.RemoveListener(ClosePopup);
        }

        public void TaskCheck()
        {
            foreach (var taskObject in taskObjects)
            {
                if (!taskObject.IsTaskObjectDone)
                    return;

                if (taskObject == taskObjects[^1])
                    TaskFinished();
            }
        }

        private void TaskFinished()
        {
            taskDoneImg.SetActive(true);
            
            //execute 
            Player.OnTaskFinished(taskType);

            StartCoroutine(C_CloseTask());
        }

        public void ClosePopup()
        {
            this.gameObject.SetActive(false);
        }

        private IEnumerator C_CloseTask()
        {
            yield return new WaitForSeconds(0.5f);
            ClosePopup();
        }
    }
}