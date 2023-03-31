using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerInAirState : PlayerMoveState
{
    protected float playerYVecloity;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.PlayerAnim.SetFloat("YVelocity", 0);
        player.ResetJumpCount();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        playerYVecloity = player.CurrentVelocity.y;
        player.PlayerAnim.SetFloat("YVelocity", playerYVecloity);

        if (player.InputHandler.JumpHoldInput && player.IsGrounded() == false)
        {
            player.ChangeJumpPower();
            player.HeldJump();
        }
        else if (playerYVecloity < 0.01f && player.IsGrounded())
        {
            if (input != 0)
                stateMachine.ChangeState(player.WalkState);
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetXVelocity(input);
    }
}
