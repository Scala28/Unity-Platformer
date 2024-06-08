using System;
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
    public bool JumpInput { get; private set; }
    public bool JumpHoldInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool[] AttackInputs { get; private set; }
    #endregion

    #region Input Options
    [SerializeField]
    private float inputHoldTime = .2f;
    private float jumpInputStartTime;
    private float dashInputStartTime;
    private float attackInputStartTime;
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

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }
    private void Update()
    {
        //RawMovementInput = moveAction.ReadValue<Vector2>();
        currentMovementInput = Vector2.SmoothDamp(currentMovementInput, RawMovementInput, ref smoothInputVelocity, smoothInputSpeed);
        InputX = currentMovementInput.x;
        InputY = currentMovementInput.y;

        CheckJumpInputHoldTime();
        CeckDashInputHoldTime();
    }

    #region Input event callbacks
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        //Debug.Log(RawMovementInput);
        //currentMovementInput = Vector2.SmoothDamp(currentMovementInput, RawMovementInput, ref smoothInputVelocity, smoothInputSpeed);
        //InputX = currentMovementInput.x;
        //InputY = currentMovementInput.y;
    }
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
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            dashInputStartTime = Time.time;
        }
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.sword] = true;
            attackInputStartTime = Time.time;
        }else if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.sword] = false;
        }
    }
    #endregion

    #region Setter
    public void UseJump() => JumpInput = false;
    public void UseJumpHold() => JumpHoldInput = false;
    public void UseDashInput() => DashInput = false;
    #endregion

    #region Checks
    private void CheckJumpInputHoldTime()
    {
        if(JumpInput && Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    private void CeckDashInputHoldTime()
    {
        if (DashInput && Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }

    #endregion

}
public enum CombatInputs
{
    sword
}
