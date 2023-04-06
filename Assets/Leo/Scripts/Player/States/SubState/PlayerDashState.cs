using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAnimState
{
    protected float dashStartTime;
    protected float moveInput;
    protected float defaultGravity;
    protected int amountOfDashesLeft;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DecreaseAmountOfDahsesLeft();
        player.DisableCollision();
        defaultGravity = player.PlayerRb2.gravityScale;
        player.PlayerRb2.gravityScale = 0;
        dashStartTime = Time.time;
        player.Dash(player.DashDirection());
        player.DashParticles(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.WaitForDashCD();
        player.EnableCollision();
        player.PlayerRb2.gravityScale = defaultGravity;
        player.DashParticles(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        moveInput = player.InputHandler.MovementInput;

        if (Time.time > dashStartTime + playerData.dashTime && player.InCollision() == false)
        {
            stateMachine.ChangeState(player.FinishDashState);
        }
    }

    public bool CanDash()
    {
        if (amountOfDashesLeft > 0 && player.canDash)
            return true;
        else
            return false;
    }

    public void ResetAmountOfDashesLeft() => amountOfDashesLeft = playerData.amountOfDashes;

    public void DecreaseAmountOfDahsesLeft() => amountOfDashesLeft--;
}
