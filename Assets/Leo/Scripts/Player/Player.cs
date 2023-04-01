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
    public PlayerLandState LandState { get; private set; }
    public PlayerFinishDashState FinishDashState { get; private set; }
    #endregion

    #region Components
    public Rigidbody2D PlayerRb2 { get; private set; }
    public Collider2D PlayerCollider { get; private set; }
    public SpriteRenderer PlayerSr { get; private set; }
    public Animator PlayerAnim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    #endregion

    public PlayerData playerData;
    public Vector2 CurrentVelocity { get; private set; }

    [SerializeField]
    private Transform feetPos;
    [SerializeField]
    private Transform bodyPos;

    public float JumpTimeCounter { get; private set; }

    private float jumpPowerModifier;

    private bool facingRight;

    [HideInInspector]
    public bool canDash;

    [HideInInspector]
    public GameObject currentCheckPoint;

    [SerializeField]
    private Fade Fade;



    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        WalkState = new PlayerWalkState(this, StateMachine, playerData, "Walk");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "Jump");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "Jump");
        DashState = new PlayerDashState(this, StateMachine, playerData, "Dash");
        LandState = new PlayerLandState(this, StateMachine, playerData, "Land");
        FinishDashState = new PlayerFinishDashState(this, StateMachine, playerData, "Finish Dash");
    }

    private void Start()
    {
        PlayerRb2 = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<Collider2D>();
        PlayerSr = GetComponent<SpriteRenderer>();
        PlayerAnim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        facingRight = true;
        canDash = true;
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        //print("Current State " + StateMachine.CurrentState.ToString());
        CurrentVelocity = PlayerRb2.velocity;
        StateMachine.CurrentState.LogicUpdate();
        //print("dash " + InputHandler.DashInput);
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
            facingRight = false;
            PlayerSr.flipX = true;
        }
        else if (direction > 0)
        {
            facingRight = true;
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

    #region Dash
    public void Dash(int direction)
    {
        PlayerRb2.velocity = new Vector2(playerData.dashSpeed * direction, 0);
        CurrentVelocity = PlayerRb2.velocity;
    }

    public bool InCollision()
    {
        return Physics2D.OverlapCircle(bodyPos.position, playerData.collisionCheckRadius, playerData.ground);
    }

    public int DashDirection()
    {
        if (facingRight)
            return 1;
        else
            return - 1;
    }

    public void DisableCollision()
    {
        PlayerCollider.enabled = false;
    }

    public void EnableCollision()
    {
        PlayerCollider.enabled = true;
    }

    public void WaitForDashCD()
    {
        canDash = false;
        StartCoroutine(Wait(playerData.dashCD));
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        canDash = true;
    }
    #endregion

    #region Animation
    private void AnimationTrigger() => ((PlayerAnimState)StateMachine.CurrentState).AnimationTrigger();

    private void AnimationFinishTrigger() => ((PlayerAnimState)StateMachine.CurrentState).AnimationFinishTrigger();
    #endregion

    #region Hazard and Check Points
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Fade.TriggerFade();
            HidePlayer();
            Respawn();
            StartCoroutine(WaitForFade());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Check Point"))
        {
            SetCheckPoint(collision.gameObject);
        }
    }

    public void SetCheckPoint(GameObject newCheckPoint)
    {
        //print("check point set " + newCheckPoint.name);
        currentCheckPoint = newCheckPoint;
    }

    private void Respawn()
    {
        //print("reset to " + currentCheckPoint.name);
        transform.position = currentCheckPoint.transform.position;
    }

    private IEnumerator WaitForFade()
    {
        while (Fade.IsDone() == false)
        {
            yield return null;
        }
        ShowPlayer();
    }

    private void HidePlayer()
    {
        PlayerSr.enabled = false;
    }

    private void ShowPlayer()
    {
        PlayerSr.enabled = true;
    }
    #endregion
}