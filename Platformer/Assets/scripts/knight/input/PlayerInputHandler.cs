using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public float NormInputX { get; private set; }
    public float NormInputY { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        Debug.Log(RawMovementInput);
        NormInputX = (RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (RawMovementInput * Vector2.up).normalized.y;
    }
}
