using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    #region Action readers
    private PlayerInput _playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    #endregion 

    #region Inputs
    public Vector2 RawMovementInput { get; private set; }
    public float InputX { get; private set; }
    public float InputY { get; private set; }
    public bool JumpInput;
    public bool JumpHoldInput;
    #endregion

    #region Input Options
    [SerializeField]
    private float inputHoldTime = .2f;
    private float jumpInputStartTime;
    #endregion

    #region Smooth movement input
    private Vector2 currentMovementInput;
    private Vector2 smoothInputVelocity;
    [SerializeField]
    private float smoothInputSpeed = .2f;
    #endregion

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        moveAction = _playerInput.actions["Movement"];
        jumpAction = _playerInput.actions["Jump"];
        currentMovementInput = Vector2.zero;
    }
    private void Update()
    {
        RawMovementInput = moveAction.ReadValue<Vector2>();
        currentMovementInput = Vector2.SmoothDamp(currentMovementInput, RawMovementInput, ref smoothInputVelocity, smoothInputSpeed);
        Debug.Log(currentMovementInput);
        InputX = currentMovementInput.x;
        InputY = currentMovementInput.y;

        CheckInputHoldTime();
    }

    #region Input event callbacks
    //public void OnMoveInput(InputAction.CallbackContext context)
    //{
    //    RawMovementInput = context.ReadValue<Vector2>();
    //    Debug.Log(currentMovementInput);
    //    InputX = (RawMovementInput * Vector2.right).x;
    //    InputY = (RawMovementInput * Vector2.up).y;
    //}
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            jumpInputStartTime = Time.time;
        }
        if (context.performed)
        {
            JumpHoldInput = true;
        }
        if (context.canceled)
        {
            JumpHoldInput = false;
        }
    }
    #endregion

    #region Setter
    public void UseJump()
    {
        JumpInput = false;
        JumpHoldInput = false;
    }
    #endregion

    #region Checks
    private void CheckInputHoldTime()
    {
        if(JumpInput && !JumpHoldInput && Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    #endregion

}
