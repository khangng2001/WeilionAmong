using System.Collections;
using System.Collections.Generic;
using MiniTask;
using UnityEngine;
using UnityEngine.UI;

namespace MiniTask
{
    public class TaskToilet : Interactable, ITask
    {
        [SerializeField] private GameObject taskDoneImg;
        
        [SerializeField] private Button flushBtn;
        [SerializeField] private List<TaskToiletObject> taskObjects = new();
        
        private Button _taskButton;
        
        public Player Player { get; set; }

        private void OnEnable()
        {
            _taskButton = GetComponent<Button>();
            _taskButton.onClick.AddListener(ClosePopup);
            
            taskDoneImg.SetActive(false);
            
            flushBtn.onClick.AddListener(OnFlushBtnClicked);
        }

        private void OnDisable()
        {
            _taskButton.onClick.RemoveListener(ClosePopup);
            
            flushBtn.onClick.RemoveListener(OnFlushBtnClicked);
        }

        private void OnFlushBtnClicked()
        {
            foreach (var taskObject in taskObjects)
            {
                if (taskObject.IsInToilet)
                    taskObject.gameObject.SetActive(false);
            }
            
            TaskCheck();
        }
        
        public void TaskCheck()
        {
            foreach (var taskObject in taskObjects)
            {
                if (taskObject.gameObject.activeSelf)
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