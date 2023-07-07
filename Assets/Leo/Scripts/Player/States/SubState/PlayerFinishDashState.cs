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
        RemoveAllDashProperties();
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

    public void RemoveAllDashProperties()
    {
        player.EnableCollision();
        player.ResetGravity();
        RemoveDashParticles();
    }

    public void RemoveDashParticles() => player.DashParticles(false);
}