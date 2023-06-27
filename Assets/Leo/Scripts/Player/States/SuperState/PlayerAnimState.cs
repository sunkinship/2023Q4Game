using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimState : PlayerState
{
    protected string animBoolName;
    protected bool isAnimationFinished;

    public PlayerAnimState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData)
    {
        this.animBoolName = animBoolName;
    }

    public override void Enter()
    {
        base.Enter();
        isAnimationFinished = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        player.CheckDialogueStart();
        player.CheckCheckPoint();
        player.CheckCamChange();
        player.CheckSceneChange();
    }

    #region Animation
    public virtual void StartAnimation()
    {
        player.PlayerAnim.SetBool(animBoolName, true);
    }

    public virtual void StopAnimation()
    {
        player.PlayerAnim.SetBool(animBoolName, false);
    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    #endregion
}