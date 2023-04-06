using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerJumpState : PlayerMoveState
{
    protected float playerYVecloity;
    private int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        DecreaseAmountOfJumpsLeft();
        player.Jump();
        stateMachine.ChangeState(player.InAirState);
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
            return true;
        else
            return false;
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}