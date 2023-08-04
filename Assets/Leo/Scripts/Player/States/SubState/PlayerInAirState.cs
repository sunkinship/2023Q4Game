using System.Collections;
using System.Collections.Generic;
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
        player.FinishDashState.RemoveAllDashProperties();
    }

    public override void Exit()
    {
        base.Exit();
        player.PlayerAnim.SetFloat("YVelocity", 0);
        player.ResetJumpTimer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        yVecloity = player.CurrentVelocity.y;
        player.PlayerAnim.SetFloat("YVelocity", yVecloity);
        jumpInput = player.InputHandler.JumpInput;
        holdJumpInput = player.InputHandler.JumpHoldInput;

        CheckIfReleasedJump();
        if (isAnimationFinished)
            player.PlayerAnim.SetBool("Double Jump", false);

        if (canLongJump && holdJumpInput && !player.IsGrounded())
        {
            player.ChangeJumpPower();
            player.HeldJump();
        }
        else if (jumpInput && player.JumpState.CanJump() && !player.IsGrounded())
        {
            player.InputHandler.UseJumpInput();
            player.PlayerAnim.SetBool("Double Jump", true);
            player.PlayDoubleJumpParticles();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (yVecloity < 0.01f && player.IsGrounded())
        {
            player.PlayerAnim.SetBool("Double Jump", false);
            player.PlayLandParticles();
            if (moveInput != 0)
                stateMachine.ChangeState(player.WalkState);
            else
                stateMachine.ChangeState(player.LandState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetXVelocity(moveInput);
    }

    private void CheckIfReleasedJump()
    {
        if (holdJumpInput == false || yVecloity <= 0)
            canLongJump = false;
    }
}
