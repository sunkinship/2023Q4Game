using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerAnimState
{
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.cameraScript.DisableCameraFollow();
        player.transform.SetParent(null);
        player.DisableCollision();
        player.TurnOffGravity();
        player.FreezePlayer();
        player.cameraScript.StartShake();
    }

    public override void DoChecks()
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            player.DeathTransition();
            stateMachine.ChangeState(player.IdleState);
        }
    }
}