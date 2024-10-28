using System.Collections;
using System.Collections.Generic;
using MiniTask;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MiniTask
{
    public class TaskBird : Interactable, ITask
    {
        [SerializeField] private GameObject taskDoneImg;
        
        [SerializeField] private TextMeshProUGUI textWormAmount;
        [SerializeField] private int maxWormsEaten = 5;
        private int _currentWormsEaten;

        private Button _taskButton;

        public Player Player { get; set; }
        
        private void OnEnable()
        {
            _taskButton = GetComponent<Button>();
            _taskButton.onClick.AddListener(ClosePopup);
            
            taskDoneImg.SetActive(false);

            _currentWormsEaten = 0;
            UpdateTextAmount();
        }

        private void OnDisable()
        {
            _taskButton.onClick.RemoveListener(ClosePopup);
        }
    
        public void TaskCheck()
        {
            _currentWormsEaten++;
            
            UpdateTextAmount();

            if (_currentWormsEaten >= maxWormsEaten)
                TaskFinished();
        }

        private void TaskFinished()
        {
            taskDoneImg.SetActive(true);
            
            //execute 
            Player.OnTaskFinished(taskType);

            StartCoroutine(C_CloseTask());
        }

        private void UpdateTextAmount()
        {
            textWormAmount.text = $"{_currentWormsEaten} / {maxWormsEaten}";
        }

        public void ClosePopup()
        {
            Destroy(gameObject);
        }

        private IEnumerator C_CloseTask()
        {
            yield return new WaitForSeconds(0.5f);
            ClosePopup();
        }
    }
}