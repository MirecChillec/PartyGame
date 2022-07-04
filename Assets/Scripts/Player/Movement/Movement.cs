using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip walk;
    public AudioClip jump;

    private Rigidbody2D rb;
    public float jumpPower = 3f;
    public float movementSpeed = 2f;

    public float fallMultiplier = 2.5f;
    public float lowerJumpMultiplier = 2f;

    public bool dootykZeme { get; private set; }
    public GameObject GroundCheck;

    private bool movingRight = false;
    private bool movingLeft = false;
    private bool isFalling = false;
    private bool isLowJumping = false;

    public bool facingRight { get; private set; }

    public Collider2D playerCollider;

    [SerializeField]private bool jumped;
    private bool dJumped = false;
    public GroundCheck gChecker;
    public ContactFilter2D filter;
    private SpriteRenderer sr;

    public bool isStunned { get; set; }
    public int stunCounter = 0;
    public float baseStunTime = 1f;

    public float altarPullSpeed;

    int lastHitId;
    public AnimationsControler animControl;
    ObjectControl OC;

    public bool jumping { get; private set; }

    ThrowableObject throwableObjectScript;

    void Awake()
    {
        OC = GetComponent<ObjectControl>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        jumped = false;
        jumping = false;
        dootykZeme = false;

        facingRight = true;

        throwableObjectScript = this.gameObject.GetComponent<ThrowableObject>();
        //throwableObjectScript.enabled = false;  //disables the throwableObject script at start as player isn't stunned at spawn
    }


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    void Update()
    {
        if (jumping && dootykZeme && !isStunned)
        {
            animControl.LandingAnimation();
        }

        if (dootykZeme && jumping)
        {
            jumping = false;
        }
        if (jumping && rb.velocity.y <= 0.1 && !isStunned)
        {
            animControl.TopJumpAnimation();
        }
        //checking for vertical velocity and multiplying it
        if (rb.velocity.y < 13)
        {
            isFalling = true;
            isLowJumping = false;
        }
        else if (rb.velocity.y > 13)
        {
            isFalling = false;
            isLowJumping = true;
        }
    }


    private void FixedUpdate()
    {
        if (!isStunned)
        {
            if (movingRight)
            {
                Vector2 newVelocity = rb.velocity;
                if (dootykZeme)
                {
                    newVelocity.x = movementSpeed;
                }
                else
                {
                    newVelocity.x = movementSpeed * 0.75f;
                }
                rb.velocity = newVelocity;
                //transform.localScale = new Vector3(0.3f, 0.3f, 1);
                animControl.MoveAnimation();
            }
            else if (movingLeft)
            {
                Vector2 newVelocity = rb.velocity;
                if (dootykZeme)
                {
                    newVelocity.x = -movementSpeed;
                }
                else
                {
                    newVelocity.x = -movementSpeed * 0.75f;
                }
                rb.velocity = newVelocity;
                //transform.localScale = new Vector3(-0.3f, 0.3f, 1);
                animControl.MoveAnimation();
            }
            else
            {
                Vector2 newVelocity = rb.velocity;
                newVelocity.x = 0;
                rb.velocity = newVelocity;
            }

            if (isFalling)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (isLowJumping)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowerJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
    }

    //jump method
    void Jump()
    {
        audio.clip = jump;
        audio.Play();
        if (rb == null) return;
        jumping = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        dootykZeme = false;
        animControl.JumpAnimation();
    }

    //move methods
    public void OnMove(float direction)
    {
        if (!isStunned)
        {
            audio.clip = walk;
            audio.Play();
            audio.loop = true;
            if (direction > 0)
            {
                movingLeft = false;
                movingRight = true;
                facingRight = true;
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                movingLeft = true;
                movingRight = false;
                facingRight = false;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    public void Stop()
    {
        movingLeft = false;
        movingRight = false;
        if (!isStunned)
        {
            animControl.StopAnimation();
        }
    }
    //jump call
    public void OnJump()
    {
        if (!isStunned)
        {
            if (dootykZeme && !jumped && !dJumped)
            {
                Jump();
                jumped = true;
            }
            else if (!dootykZeme && jumped && !dJumped)
            {
                Jump();
                dJumped = true;
            }
            else if (!dootykZeme && !jumped && !dJumped)
            {
                Jump();
                jumped = true;
                dJumped = true;
            }
        }
    }
    //drop function
    public void JumpRest()
    {
        jumped = false;
        dJumped = false;
        dootykZeme = true;
    }
    public void DisableJump()
    {
        dootykZeme = false;
    }
    public void StunAnimation()
    {
        animControl.ChangeAnimation(Animations.down);
    }
}
