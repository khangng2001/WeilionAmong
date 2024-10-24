using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    #region Fields
    private Vector2 _playerMovement;
    #endregion
    
    public Vector2 PlayerMovement => _playerMovement;
    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }
    
    private void OnEnable()
    {
        _playerControls.Player.Movement.performed += OnMoving;
        _playerControls.Player.Movement.canceled += OnStopMoving;
        _playerControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Player.Movement.performed -= OnMoving;
        _playerControls.Player.Movement.canceled -= OnStopMoving;
        _playerControls.Disable();
    }

    private void OnStopMoving(InputAction.CallbackContext callback)
    {
        _playerMovement = Vector2.zero;
    }
    
    private void OnMoving(InputAction.CallbackContext callback)
    {
        _playerMovement = callback.ReadValue<Vector2>();
    }
}