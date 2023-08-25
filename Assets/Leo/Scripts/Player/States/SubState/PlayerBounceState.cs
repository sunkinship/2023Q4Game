using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounceState : PlayerMoveState
{
    protected AudioClip bounceClip;

    public PlayerBounceState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, AudioClip bounceClip) : base(player, stateMachine, playerData, animBoolName)
    {
        this.bounceClip = bounceClip;
    }

    public override void Enter()
    {
        base.Enter();

        PlaySound();
        player.Bounce(player.InputHandler.JumpHoldInput);
        stateMachine.ChangeState(player.InAirState);
    }

    public override void Exit()
    {
        player.ResetDoubleJump();
    }

    public virtual void PlaySound()
    {
        AudioManager.Instance.PlaySFX(bounceClip);
    }
}