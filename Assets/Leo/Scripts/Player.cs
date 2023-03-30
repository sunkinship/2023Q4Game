using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }

    public Rigidbody2D PlayerRb2 { get; private set; }
    public SpriteRenderer PlayerSr { get; private set; }
    public Animator PlayerAnim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    public PlayerData playerData;



    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        WalkState = new PlayerWalkState(this, StateMachine, playerData, "Walk");
    }

    private void Start()
    {
        PlayerRb2 = GetComponent<Rigidbody2D>();
        PlayerSr = GetComponent<SpriteRenderer>();
        PlayerAnim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        StateMachine.Initialize(IdleState);
    }

    //private void FixedUpdate()
    //{
    //    float x = Input.GetAxis("Horizontal");
    //    rb2.velocity = new Vector2(speed * x, rb2.velocity.y);
    //    if (Input.GetButton("Jump") && IsGrounded())
    //    {
    //        rb2.AddForce(new Vector2(0, jumpPower));
    //    }
    //    bool moving = Mathf.Abs(x) > 0;
    //    if (moving)
    //    {
    //        sr.flipX = x < 0;
    //    }
    //    animator.SetBool("Moving", moving);

    //    Debug.Log(rb2.velocity.y);

    //    bool crouch = Input.GetButton("Crouch");
    //    animator.enabled = !crouch;
    //    sr.sprite = CUBEEEE;

    //    GetComponents<BoxCollider2D>()[0].enabled = !crouch;
    //    GetComponents<BoxCollider2D>()[1].enabled = crouch;

    //    animator.SetFloat("yVelocity", rb2.velocity.y);
    //    animator.SetBool("Grounded", isGround);
    //    if (rb2.velocity.y > 0)
    //    {
    //        animator.SetBool("Jumping", true);
    //    }
    //    else if (rb2.velocity.y < 0)
    //    {
    //        animator.SetBool("Jumping", false);
    //    }

    //}

    private void Update()
    {
        print("Current State " + StateMachine.CurrentState.ToString() + " Input " + InputHandler.movementInput);
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocity(float direction)
    {
        PlayerRb2.velocity = new Vector2(direction * playerData.speed, PlayerRb2.velocity.y);
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
}
