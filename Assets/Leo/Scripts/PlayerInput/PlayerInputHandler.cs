using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float MovementInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpHoldInput { get; private set; }
    public bool DashInput { get; private set; }



    private float JumpInputStartTime;

    private float InputHoldTime = 0.2f;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        float moveVal = context.ReadValue<float>();

        if (moveVal > 0)
            MovementInput = 1;
        else if (moveVal < 0)
            MovementInput = -1;
        else
            MovementInput = 0;

        print(MovementInput);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            JumpInput = true;
            JumpInputStartTime = Time.time;
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

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= JumpInputStartTime + InputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
        }
    }

    public void UseDashInput() => DashInput = false;
}
