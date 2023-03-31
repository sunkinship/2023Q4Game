using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAnimState
{
    protected float dashStartTime;
    protected float moveInput;
    protected float defaultGravity;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        defaultGravity = player.PlayerRb2.gravityScale;
        player.DisableCollision();
        player.PlayerRb2.gravityScale = 0;
        dashStartTime = Time.time;
        player.Dash(player.DashDirection());
    }

    public override void Exit()
    {
        base.Exit();
        player.EnableCollision();
        player.PlayerRb2.gravityScale = defaultGravity;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        moveInput = player.InputHandler.MovementInput;

        if (Time.time > dashStartTime + playerData.dashTime && player.InCollision() == false)
        {
            if (player.IsGrounded())
            {
                if (moveInput != 0)
                    stateMachine.ChangeState(player.WalkState);
                else
                    stateMachine.ChangeState(player.IdleState);
            }
            else
                stateMachine.ChangeState(player.InAirState);
        }
    }
}
