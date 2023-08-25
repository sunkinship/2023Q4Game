using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerAnimState
{
    protected AudioClip deathClip;

    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, AudioClip deathClip) : base(player, stateMachine, playerData, animBoolName)
    {
        this.deathClip = deathClip;
    }

    public override void Enter()
    {
        base.Enter();

        PlaySound();
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

    public virtual void PlaySound()
    {
        AudioManager.Instance.PlaySFX(deathClip);
    }
}