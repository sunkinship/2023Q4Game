using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerGroundedState
{
    protected AudioClip walkClip;

    public PlayerWalkState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, AudioClip walkClip) : base(player, stateMachine, playerData, animBoolName)
    {
        this.walkClip = walkClip;
    }

    public override void Enter()
    {
        base.Enter();
        player.FinishDashState.RemoveAllDashProperties();
        PlaySound();
    }

    public override void Exit()
    {
        base.Exit();
        StopSound();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (moveInput == 0f && jumped == false)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetXVelocity(moveInput);
    }

    public virtual void PlaySound()
    {
        AudioManager.Instance.PlaySFX(walkClip);
    }

    public virtual void StopSound()
    {
        AudioManager.Instance.StopSFX();
    }
}
