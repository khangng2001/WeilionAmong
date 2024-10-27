using System;
using UnityEngine;
using UnityEngine.UI;

namespace Task
{
    public class TaskBed : Interactable, ITask
    {
        public Button taskButton;

        public Player Player { get; set; }

        public void Start()
        {
            taskButton.onClick.AddListener(CloseTask);
        }

        public void Update()
        {
            
        }
        

        private void IsTaskFinish(bool isFinished)
        {
            //execute 
            Player.OnTaskFinished(taskType);
        }

        public void CloseTask()
        {
            //
            
            
            this.gameObject.SetActive(false);
        }
    }
}