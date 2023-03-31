using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerInAirState : PlayerMoveState
{
    protected float yVecloity;
    protected bool jumpInput;
    protected bool holdJumpInput;
    protected bool canLongJump;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canLongJump = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.PlayerAnim.SetFloat("YVelocity", 0);
        player.ResetJumpCount();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        yVecloity = player.CurrentVelocity.y;
        player.PlayerAnim.SetFloat("YVelocity", yVecloity);
        jumpInput = player.InputHandler.JumpInput;
        holdJumpInput = player.InputHandler.JumpHoldInput;

        CheckIfReleasedJump();

        if (canLongJump && player.InputHandler.JumpHoldInput && !player.IsGrounded())
        {
            player.ChangeJumpPower();
            player.HeldJump();
        }
        else if (jumpInput && player.JumpState.CanJump() && !player.IsGrounded())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (yVecloity < 0.01f && player.IsGrounded())
        {
            if (input != 0)
                stateMachine.ChangeState(player.WalkState);
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetXVelocity(input);
    }

    private void CheckIfReleasedJump()
    {
        if (holdJumpInput == false || yVecloity <= 0)
            canLongJump = false;
    }
}
