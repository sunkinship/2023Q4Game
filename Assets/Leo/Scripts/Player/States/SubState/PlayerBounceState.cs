using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounceState : PlayerMoveState
{
    public PlayerBounceState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Bounce(player.InputHandler.JumpInput);
        stateMachine.ChangeState(player.InAirState);
    }

    public override void Exit()
    {
        player.WaitToResetDoubleJump();
    }
}