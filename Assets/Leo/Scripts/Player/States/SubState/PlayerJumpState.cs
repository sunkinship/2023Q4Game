using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerMoveState
{
    protected int totalJumps;
    protected float playerYVecloity;
    private int amountOfJumpsLeft;
    protected AudioClip jumpClip, doubleClip;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, AudioClip jumpClip, AudioClip doubleClip) : base(player, stateMachine, playerData, animBoolName)
    {
        totalJumps = playerData.amountOfJumps; 
        amountOfJumpsLeft = playerData.amountOfJumps;
        this.jumpClip = jumpClip;
        this.doubleClip = doubleClip;
    }

    public override void Enter()
    {
        base.Enter();
        DecreaseAmountOfJumpsLeft();
        player.Jump();
        PlaySound();
        stateMachine.ChangeState(player.InAirState);
    }

    public bool CanJump()
    {
        if (totalJumps > 1 && amountOfJumpsLeft > 0 && player.CanDoubleJump())
            return true;
        else
            return false;
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;

    public void SetAmountOfJumpsLeft(int jumps) => amountOfJumpsLeft = jumps;

    public int GetAmountOfJumpsLeft()
    {
        return amountOfJumpsLeft;
    }

    public virtual void PlaySound()
    {
        if (player.PlayerAnim.GetBool("Double Jump") == false)
        {
            AudioManager.Instance.PlaySFX(jumpClip);
        }
        else
        {
            AudioManager.Instance.PlaySFX(doubleClip);
        }
    }
}