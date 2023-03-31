using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    #region Player States
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    #endregion

    #region Components
    public Rigidbody2D PlayerRb2 { get; private set; }
    public SpriteRenderer PlayerSr { get; private set; }
    public Animator PlayerAnim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    #endregion

    public PlayerData playerData;
    public Vector2 CurrentVelocity { get; private set; }

    [SerializeField]
    private Transform feetPos;

    public float JumpTimeCounter { get; private set; }

    private float jumpPowerModifier;



    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        WalkState = new PlayerWalkState(this, StateMachine, playerData, "Walk");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "Jump");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "Jump");
        DashState = new PlayerDashState(this, StateMachine, playerData, "Dash");
    }

    private void Start()
    {
        PlayerRb2 = GetComponent<Rigidbody2D>();
        PlayerSr = GetComponent<SpriteRenderer>();
        PlayerAnim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        //print("Current State " + StateMachine.CurrentState.ToString());
        CurrentVelocity = PlayerRb2.velocity;
        StateMachine.CurrentState.LogicUpdate();
        //print(InputHandler.MovementInput);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetXVelocity(float velocityX)
    {
        PlayerRb2.velocity = new Vector2(velocityX * playerData.walkSpeed, CurrentVelocity.y);
        CurrentVelocity = PlayerRb2.velocity;
    }

    public void FlipPlayer(float direction)
    {
        if (direction < 0)
        {
            PlayerSr.flipX = true;
        }
        else if (direction > 0)
        {
            PlayerSr.flipX = false;
        }
    }

    #region Jump
    public bool IsGrounded()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 20, playerData.ground);
        //if (hit.collider != null)
        //{
        //    return hit.distance <= 0.4;
        //}
        //return false;
        return Physics2D.OverlapCircle(feetPos.position, playerData.groundCheckRadius, playerData.ground);
    }

    public void Jump()
    {
        JumpTimeCounter = playerData.maxJumpTime;
        PlayerRb2.velocity = new Vector2(CurrentVelocity.x, playerData.jumpPower);
        CurrentVelocity = PlayerRb2.velocity;
    }

    public void HeldJump()
    {
        if (JumpTimeCounter > 0)
        {
            PlayerRb2.velocity = new Vector2(CurrentVelocity.x, playerData.jumpPower + jumpPowerModifier);
            CurrentVelocity = PlayerRb2.velocity;
            JumpTimeCounter -= Time.deltaTime;
        }
    }

    public void ChangeJumpPower()
    {
        if (JumpTimeCounter >= playerData.maxJumpTime - (playerData.maxJumpTime * .2))
            return;
        else if (JumpTimeCounter >= playerData.maxJumpTime - (playerData.maxJumpTime * .4))
            jumpPowerModifier = 3;
        else if (JumpTimeCounter >= playerData.maxJumpTime - (playerData.maxJumpTime * .6))
            jumpPowerModifier = 5;
        else if (JumpTimeCounter >= playerData.maxJumpTime - (playerData.maxJumpTime * .8))
            jumpPowerModifier = 3;
        else
            jumpPowerModifier = 1;

        jumpPowerModifier = 0;
    }

    public void ResetJumpCount() => JumpTimeCounter = 0;
    #endregion

    public void Dash()
    {
        PlayerRb2.velocity = new Vector2(playerData.dashSpeed, 0);
        CurrentVelocity = PlayerRb2.velocity;
    }
}
