using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerAnimState
{
    protected float moveInput;
    protected bool dashInput;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        moveInput = player.InputHandler.MovementInput;
        dashInput = player.InputHandler.DashInput;

        player.FlipPlayer(moveInput);

        if (dashInput && player.DashState.CanDash())
        {
            player.InputHandler.UseDashInput();
            stateMachine.ChangeState(player.DashState);
        }
    }
}