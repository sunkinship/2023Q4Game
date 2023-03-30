using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerAnimState
{
    protected float input;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        input = player.InputHandler.movementInput;
    }

    public override void Exit()
    {
        base.Exit();
        input = player.InputHandler.movementInput;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        input = player.InputHandler.movementInput;
        player.FlipPlayer(input);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
