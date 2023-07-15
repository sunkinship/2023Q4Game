using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

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
    public PlayerBounceState BounceState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }
    #endregion

    #region Components
    public Rigidbody2D PlayerRb2 { get; private set; }
    public Collider2D PlayerCollider { get; private set; }
    public SpriteRenderer PlayerSr { get; private set; }
    public Animator PlayerAnim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    #endregion

    #region Player References
    public PlayerData playerData;
    public Vector2 CurrentVelocity { get; private set; }

    [Header("Collision Check")]
    [SerializeField]
    private Transform feetPos;
    [SerializeField]
    private Transform bodyPos;
    #endregion

    #region Other Player Values
    public float JumpTimeCounter { get; private set; }

    private float jumpPowerModifier;

    private bool facingRight;

    [HideInInspector]
    public bool canDash;

    private bool isDead;
    #endregion

    #region GameObject References
    [HideInInspector]
    public CameraScript cameraScript;

    [HideInInspector]
    public CheckPoint currentCheckPoint;
    #endregion

    #region Particles
    [Header("Particles")]
    [SerializeField]
    private ParticleSystem jumpBurstParticle;
    [SerializeField]
    private ParticleSystem jumpFollowParticle;
    [SerializeField]
    private ParticleSystem doubleJumpParticle;
    [SerializeField]
    private ParticleSystem landParticle;
    [SerializeField]
    private ParticleSystem dashParticle;
    #endregion
 
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        WalkState = new PlayerWalkState(this, StateMachine, playerData, "Walk", playerData.walkClip);
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "Jump", playerData.jumpClip, playerData.doubleJumpClip);
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "Jump");
        DashState = new PlayerDashState(this, StateMachine, playerData, "Dash");
        LandState = new PlayerLandState(this, StateMachine, playerData, "Land");
        FinishDashState = new PlayerFinishDashState(this, StateMachine, playerData, "Finish Dash");
        BounceState = new PlayerBounceState(this, StateMachine, playerData, "Jump");
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "Death");
    }

    private void Start()
    {
        PlayerRb2 = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<Collider2D>();
        PlayerSr = GetComponent<SpriteRenderer>();
        PlayerAnim = GetComponent<Animator>();
        InputHandler = GameObject.FindGameObjectWithTag("Input").GetComponent<PlayerInputHandler>();
        cameraScript = Camera.main.GetComponent<CameraScript>();  

        facingRight = true;
        canDash = true;
        PlayerRb2.gravityScale = playerData.defaultGravity;
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        //print("Current State " + StateMachine.CurrentState.ToString());
        CurrentVelocity = PlayerRb2.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Ground Check
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(feetPos.position, playerData.groundCheckRadius, playerData.ground);
    }
    #endregion

    #region Walk
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
    #endregion

    #region Jump
    public void Jump()
    {
        JumpTimeCounter = playerData.maxJumpTime;
        PlayerRb2.velocity = new Vector2(CurrentVelocity.x, playerData.jumpStrength);
        CurrentVelocity = PlayerRb2.velocity;
    }

    public void HeldJump()
    {
        if (JumpTimeCounter > 0)
        {
            PlayerRb2.velocity = new Vector2(CurrentVelocity.x, playerData.jumpStrength + jumpPowerModifier);
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

    public void ResetJumpTimer() => JumpTimeCounter = 0;

    public bool CanDoubleJump()
    {
        return NotTooCloseToGround() && NotTooCloseToBounce();
    }

    private bool NotTooCloseToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, playerData.ground);
        if (hit.collider != null)
        {
            return hit.distance >= 1;
        }
        return true;
    }

    private bool NotTooCloseToBounce()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 3f, playerData.bounce);
        if (hit.collider != null)
        {
            return hit.distance >= 3f;
        }
        return true;
    }
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

    #region Bounce
    public bool OnBounce()
    {
        return Physics2D.OverlapCircle(feetPos.position, playerData.groundCheckRadius, playerData.bounce);
    }

    public void Bounce(bool jumpInput)
    {
        DashState.ResetAmountOfDashesLeft();
        if (jumpInput == false)
        {
            PlayerRb2.velocity = new Vector2(CurrentVelocity.x, playerData.bounceStrengh);
            CurrentVelocity = PlayerRb2.velocity;
        }
        else
        {
            PlayerRb2.velocity = new Vector2(CurrentVelocity.x, playerData.bounceStrengh + playerData.jumpStrength / 2);
            CurrentVelocity = PlayerRb2.velocity;
        }
    }

    public void ResetDoubleJump()
    {
        JumpState.SetAmountOfJumpsLeft(0);
        StartCoroutine(WaitToResetDoubleJump());
    }

    private IEnumerator WaitToResetDoubleJump()
    {
        yield return new WaitForSeconds(0.2f);
        JumpState.SetAmountOfJumpsLeft(1); 
    }
    #endregion

    #region Gravity and Player Control
    public void TurnOffGravity() => PlayerRb2.gravityScale = 0;

    public void ResetGravity() => PlayerRb2.gravityScale = playerData.defaultGravity;

    public void DisableCollision() => PlayerCollider.enabled = false;

    public void EnableCollision() => PlayerCollider.enabled = true;
    #endregion

    #region Animation
    private void AnimationTrigger() => ((PlayerAnimState)StateMachine.CurrentState).AnimationTrigger();

    private void AnimationFinishTrigger() => ((PlayerAnimState)StateMachine.CurrentState).AnimationFinishTrigger();
    #endregion

    #region Check Points and Cam Bound Check
    public void CheckCheckPoint()
    {
        Collider2D col = Physics2D.OverlapCircle(bodyPos.position, playerData.collisionCheckRadius, playerData.checkPoint);
        if (col == null)
            return;
        else
            SetCheckPoint(col.gameObject);
    }

    public void CheckCamChange()
    {
        Collider2D col = Physics2D.OverlapCircle(bodyPos.position, playerData.collisionCheckRadius, playerData.camChange);
        if (col == null)
            return;
        else
            col.GetComponent<ChangeCameraBounds>().UpdateCamBounds();
    }

    private void SetCheckPoint(GameObject newCheckPoint) => currentCheckPoint = newCheckPoint.GetComponent<CheckPoint>();
    #endregion

    #region Death, Respawn, and Hazards
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead == false && collision.gameObject.layer == 9)
        {
            isDead = true;
            StateMachine.ChangeState(DeathState);
        }       
    }

    public bool CheckHazard()
    {
        Collider2D col = Physics2D.OverlapCircle(bodyPos.position, playerData.collisionCheckRadius, playerData.hazard);
        if (col == null)
            return false;
        else
        {
            isDead = true;
            StateMachine.ChangeState(DeathState);
            return true;
        }   
    }

    public void FreezePlayer()
    {
        PlayerRb2.velocity = Vector2.zero;
        CurrentVelocity = PlayerRb2.velocity;
    }  

    private void HidePlayer() => PlayerSr.enabled = false;

    private void ShowPlayer() => PlayerSr.enabled = true;

    private bool Respawn()
    {
        transform.position = currentCheckPoint.respawnPoint.position;
        ResetGravity();
        EnableCollision();
        if (currentCheckPoint.faceRight)
            FlipPlayer(1);
        else
            FlipPlayer(0);
        isDead = false;
        ShowPlayer();
        cameraScript.EnableCameraFollow();
        return true;
    }

    public void DeathTransition()
    {
        HidePlayer();
        Transition.Instance.TriggerFadeBoth("StartQuickBlack", "EndQuickBlack", Respawn);
    }
    #endregion

    #region Dialogue and Scene Change Check
    public void CheckDialogueStart()
    {
        Collider2D col = Physics2D.OverlapCircle(bodyPos.position, playerData.collisionCheckRadius, playerData.startDialogue);
        if (col == null)
            return;
        else
        {
            Destroy(col.gameObject);
            StartCoroutine(WaitToIdleBeforeStart());
        }
    }

    private IEnumerator WaitToIdleBeforeStart()
    {
        while (StateMachine.CurrentState != IdleState)
        {
            PlayerInputHandler.Instance.DisableInput();
            yield return null;
        }
        Dialogue.Instance.NextDialogueSequence();
    }

    public void CheckSceneChange()
    {
        Collider2D col = Physics2D.OverlapCircle(bodyPos.position, playerData.collisionCheckRadius, playerData.sceneChange);
        if (col == null)
            return;
        else
        {
            Destroy(col.gameObject);
            col.GetComponent<SceneTransition>().ChangeScenes();
        }
    }
    #endregion

    #region Particles
    public void PlayJumpParticles()
    {
        jumpBurstParticle.Play();
        jumpFollowParticle.Play();
    }

    public void PlayDoubleJumpParticles()
    {
        doubleJumpParticle.Play();
    }

    public void PlayLandParticles()
    {
        landParticle.Play();
    }

    public void DashParticles(bool play)
    {
        if (play)
        {
            if (facingRight)
                dashParticle.transform.localScale = new Vector2(1, 1);
            else
                dashParticle.transform.localScale = new Vector2(-1, 1);
            dashParticle.Play();
        }
        else
            dashParticle.Stop();
    }
    #endregion
}