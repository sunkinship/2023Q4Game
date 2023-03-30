using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimState : PlayerState
{
    protected string animBoolName;

    public PlayerAnimState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData)
    {
        this.animBoolName = animBoolName;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public virtual void StartAnimation()
    {
        player.PlayerAnim.SetBool(animBoolName, true);
    }

    public virtual void StopAnimation()
    {
        player.PlayerAnim.SetBool(animBoolName, false);
    }
}
