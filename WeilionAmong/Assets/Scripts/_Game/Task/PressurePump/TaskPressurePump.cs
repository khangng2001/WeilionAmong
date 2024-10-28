using System.Collections;
using DG.Tweening;
using MiniTask;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Task.PressurePump
{
    public class TaskPressurePump : MonoBehaviour, ITask
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private GameObject _donePanel;
        [SerializeField] private RectTransform _pressurePump; 
        
        private readonly float _pressureThreshold = 95f; 
        private float _currentPressure = 0f; 
        private float _decreasingSpeed = 10f;
        private float _pumpForce = 8f;
        
        private bool _isDone = false;

        private float _pumpTravelingDistance = 20f;
        private float _pumpTravelingTime = 0.1f;
        
        public Player Player { get; set; }
        public void TaskCheck()
        {
            
        }

        public void ClosePopup()
        {
            
        }

        public void Update()
        {
            _slider.value = _currentPressure;
            DecreasingPressure();
            CheckingPressure(_currentPressure);
        }

        public void CloseTask()
        {
            
        }

        private void CheckingPressure(float pressure)
        {
            if (!(pressure >= _pressureThreshold) || _isDone) return;
            pressure = _pressureThreshold;
            _isDone = true;
            StartCoroutine(DeclareWin());
        }

        private IEnumerator DeclareWin()
        {
            yield return new WaitForSeconds(0.5f);
            _donePanel.SetActive(true);
        }
        
        public void OnPumping()
        {
            if (!_isDone)
            {
                _currentPressure += _pumpForce;
            }
            AnimatePumping();
        }
        
        private void DecreasingPressure()
        {
            if (_isDone) return;
            _currentPressure -= _decreasingSpeed * Time.deltaTime;
            _currentPressure = Mathf.Clamp(_currentPressure, 0f, _pressureThreshold);
        }

        private void AnimatePumping()
        {
            _pressurePump.DOAnchorPosY(_pressurePump.anchoredPosition.y - _pumpTravelingDistance, _pumpTravelingTime)
                .SetLoops(2, LoopType.Yoyo) 
                .SetEase(Ease.InOutSine); 
        }
    }
}
