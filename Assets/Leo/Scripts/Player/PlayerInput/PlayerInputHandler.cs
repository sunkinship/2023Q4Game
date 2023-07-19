using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance;

    private PlayerInput playerInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerInput = GetComponent<PlayerInput>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float MovementInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpHoldInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool PauseInput { get; private set; }
    public bool CancelInput { get; private set; }


    private float JumpInputStartTime;

    private float JumpInputHoldTime = 0.2f;


    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    #region Move Input
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        float moveVal = context.ReadValue<float>();

        if (moveVal > 0)
            MovementInput = 1;
        else if (moveVal < 0)
            MovementInput = -1;
        else
            MovementInput = 0;

        //print(MovementInput);
    }
    #endregion

    #region Jump Input
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
        if (Time.time >= JumpInputStartTime + JumpInputHoldTime)
        {
            JumpInput = false;
        }
    }
    #endregion

    #region Dash Input
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
        }

        if (context.canceled)
        {
            DashInput = false;
        }
    }

    public void UseDashInput() => DashInput = false;
    #endregion

    #region Interact Input
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractInput = true;
        }
    }

    public void UseInteractInput() => InteractInput = false;
    #endregion

    #region Pause Input
    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PauseInput = true;
        }

        if (context.canceled)
        {
            PauseInput = false;
        }
    }

    public void UsePauseInput() => PauseInput = false;
    #endregion

    #region Cancel Input
    public void OnCancelInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CancelInput = true;
        }

        if (context.canceled)
        {
            CancelInput = false;
        }
    }

    public void UseCancelInput() => CancelInput = false;
    #endregion

    #region Switch Action Maps / Enable Disable
    public void SwitchActionMap(string mapName)
    {
        //print("switch to " + mapName);
        playerInput.SwitchCurrentActionMap(mapName);
    }

    public void EnableInput()
    {
        //print("enable");
        playerInput.ActivateInput();
    }

    public void DisableInput()
    {
        //print("disable");
        playerInput.DeactivateInput();
    }
    #endregion
}
