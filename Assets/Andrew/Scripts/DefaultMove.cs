using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMove : MonoBehaviour 
{
    public bool isGround;
    private Rigidbody2D rb2;
    private SpriteRenderer sr;
    private Animator animator;
    public float jumpPower;
    public float speed;
    public float yVelocity;
 
    public Sprite CUBEEEE;

    void Start() {
        rb2 = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        yVelocity = rb2.velocity.y;
    }

    void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        rb2.velocity = new Vector2(speed * x, rb2.velocity.y);
        if (Input.GetButton("Jump") && IsGrounded()) {
            rb2.AddForce(new Vector2(0, jumpPower));
        }
        bool moving = Mathf.Abs(x) > 0;
        if (moving) {
            sr.flipX = x < 0;
        }
        animator.SetBool("Moving", moving);

        Debug.Log(rb2.velocity.y);

        bool crouch = Input.GetButton("Crouch");
        animator.enabled = !crouch;
        sr.sprite = CUBEEEE;

        GetComponents<BoxCollider2D>()[0].enabled = !crouch;
        GetComponents<BoxCollider2D>()[1].enabled = crouch;

        animator.SetFloat("yVelocity", rb2.velocity.y);
        animator.SetBool("Grounded", isGround);
        if(rb2.velocity.y > 0)
        {
            animator.SetBool("Jumping", true);
        } else if(rb2.velocity.y < 0)
        {
            animator.SetBool("Jumping", false);
        }
      
    }

    private bool IsGrounded()
    {
        return rb2.velocity.y == 0;
    }
}
