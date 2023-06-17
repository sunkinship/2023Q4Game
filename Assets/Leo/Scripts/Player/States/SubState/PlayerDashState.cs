using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAnimState
{
    protected float dashStartTime;
    protected float moveInput;
    public float defaultGravity;
    protected int amountOfDashesLeft;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ENTER DASH");
        DecreaseAmountOfDahsesLeft();
        player.DisableCollision();
        player.PlayerRb2.gravityScale = 0;
        dashStartTime = Time.time;
        player.Dash(player.DashDirection());
        player.DashParticles(true);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT DASH");
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

    public override void DoChecks()
    {
        base.DoChecks();
        if (player.CheckHazard())
        {
            player.FinishDashState.Exit();
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public void ResetAmountOfDashesLeft() => amountOfDashesLeft = playerData.amountOfDashes;

    public void DecreaseAmountOfDahsesLeft() => amountOfDashesLeft--;
}