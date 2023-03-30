using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float movementInput { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<float>();
        //Debug.Log(movementInput);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            Debug.Log("Start Jump");
        }

        if (context.performed)
        {
            Debug.Log("Hold Jump");
        }

        if (context.canceled) 
        {
            Debug.Log("Stop Jump");
        }
    }
}
