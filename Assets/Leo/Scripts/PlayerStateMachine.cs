using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
        if (IsAnimState(CurrentState))
            ((PlayerAnimState)CurrentState).StartAnimation();
    }

    public void ChangeState(PlayerState newState)
    {
        //Debug.Log("WAS " + CurrentState.ToString());
        CurrentState.Exit();
        SwitchState(newState);
        CurrentState.Enter();
        //Debug.Log("CURRENT " + CurrentState.ToString());
    }

    private void SwitchState(PlayerState newState)
    {
        if (IsAnimState(newState))
        {
            if (IsAnimState(CurrentState))
                ((PlayerAnimState)CurrentState).StopAnimation();
            CurrentState = newState;
            ((PlayerAnimState)CurrentState).StartAnimation();
        }
        else
            CurrentState = newState;
    }

    private bool IsAnimState(PlayerState state)
    {
        return state is PlayerAnimState;
    }
}
