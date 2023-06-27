using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinishDashState : PlayerAnimState
{
    protected float moveInput;

    public PlayerFinishDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Exit()
    {
        base.Exit();
        player.WaitForDashCD();
        RemoveDashProperties();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        moveInput = player.InputHandler.MovementInput;

        if (isAnimationFinished)
        {
            if (player.IsGrounded())
            {
                if (moveInput != 0)
                    stateMachine.ChangeState(player.WalkState);
                else
                    stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public void RemoveDashProperties()
    {
        player.EnableCollision();
        player.PlayerRb2.gravityScale = playerData.defaultGravity;
        player.DashParticles(false);
    }
}