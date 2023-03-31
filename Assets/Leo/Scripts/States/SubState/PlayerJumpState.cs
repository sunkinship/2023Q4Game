using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerMoveState
{
    protected float playerYVecloity;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.Jump();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        playerYVecloity = player.CurrentVelocity.y;
        player.PlayerAnim.SetFloat("YVelocity", playerYVecloity);

        //jumpStartTime += Time.deltaTime;

        //if (player.InputHandler.JumpHoldInput && player.IsGrounded() == false)
        //{
        //    player.HeldJump();
        //}
        //else
        if (playerYVecloity < 0.01f && player.IsGrounded())
        {
            //Debug.Log("exit jump");
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            //Debug.Log("go to in air");
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetXVelocity(input);
    }
}
