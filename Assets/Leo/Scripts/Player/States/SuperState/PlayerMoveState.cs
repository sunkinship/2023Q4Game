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
            player.PlayerAnim.SetBool("Double Jump", false);
            if (player.JumpState.GetAmountOfJumpsLeft() >= 2)
                player.JumpState.DecreaseAmountOfJumpsLeft();
            stateMachine.ChangeState(player.DashState);
        }
        else if (player.OnBounce())
        {
            player.PlayerAnim.SetBool("Double Jump", false);
            stateMachine.ChangeState(player.BounceState);
        }
    }
}