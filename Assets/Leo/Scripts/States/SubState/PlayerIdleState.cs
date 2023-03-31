using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("idle");
        player.SetXVelocity(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (moveInput != 0f && jumped == false)
        {
            stateMachine.ChangeState(player.WalkState);
        }
    }
}
