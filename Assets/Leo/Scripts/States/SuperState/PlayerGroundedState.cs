using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerMoveState
{
    protected bool jumped;
    protected float yVelocity;
    protected bool grounded;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        grounded = true;
        player.DashState.ResetAmountOfDashesLeft();
        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
        grounded = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        jumped = player.InputHandler.JumpInput;
        yVelocity = player.PlayerRb2.velocity.y;

        if (player.IsGrounded() == false && yVelocity < 0)
        {
            player.JumpState.DecreaseAmountOfJumpsLeft();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (jumped)
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public bool IsGrounded()
    {
        return grounded;
    }
}
