using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Task
{
    public class Player : MonoBehaviour
    {
        public Button useBtn;
        public Transform popupParent;
        
        public Action<EnumTask> TaskDone;
        
        private Interactable _curInteractable;

        private int _maxTasks = 3;
        private int _amountTasksDone = 0;

        #region TEST

        private void Awake()
        {
            ResourceLoader.LoadResourceByFolder<GameObject>("Popup/Task");
        }

        #endregion

        private void Start()
        {
            useBtn.onClick.AddListener(OnUseBtnClicked);
            useBtn.interactable = false;
        }

        public void OnTaskFinished(EnumTask task)
        {
            _amountTasksDone++;
            
            TaskDone?.Invoke(task);
        }



        #region INTERACT WITH INTERACTABLE AREA

        private void OnUseBtnClicked()
        {
            if (_curInteractable == null)   return;

            _curInteractable.OnPlayerInteracted(this);
        }

        private void ActiveUseBtn()
        {
            useBtn.interactable = true;
        }
        
        private void DeActiveUseBtn()
        {
            useBtn.interactable = false;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Interactable>(out var interactable))
            {
                _curInteractable = interactable;
                
                //active UseButton
                ActiveUseBtn();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Interactable>(out var interactable))
            {
                _curInteractable = null;
                
                //deActive UseButton
                DeActiveUseBtn();
            }
        }

        #endregion
    }
}