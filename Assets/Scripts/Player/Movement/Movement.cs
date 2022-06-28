using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpPower = 3f;
    public float movementSpeed = 2f;

    public float fallMultiplier = 2.5f;
    public float lowerJumpMultiplier = 2f;

    [SerializeField] private bool dootykZeme = false;
    public GameObject GroundCheck;

    private bool movingRight = false;
    private bool movingLeft = false;
    private bool isFalling = false;
    private bool isLowJumping = false;

    public bool facingRight { get; private set; }

    public Collider2D playerCollider;

    public ScreenBounds screenBounds;

    private bool jumped = false;
    private bool dJumped = false;
    public GroundCheck gChecker;
    public ContactFilter2D filter;
    private SpriteRenderer sr;

    public bool isStunned;
    public int stunCounter = 0;
    public float baseStunTime = 1f;

    public GameObject altar;
    public float altarPullSpeed;

    int lastHitId;
    public AnimationsControler animControl;
    ObjectControl OC;

    bool jumping = false;

    ThrowableObject throwableObjectScript;

    void Awake()
    {
        OC = GetComponent<ObjectControl>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        facingRight = true;

        throwableObjectScript = this.gameObject.GetComponent<ThrowableObject>();
        //throwableObjectScript.enabled = false;  //disables the throwableObject script at start as player isn't stunned at spawn
    }


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
        screenBounds = GameData.scrennBounds;
    }

    void Update()
    {
        if (jumping && dootykZeme && !isStunned)
        {
            LandingAnimation();
        }

        if (dootykZeme && jumping)
        {
            jumping = false;
        }
        if(jumping && rb.velocity.y <= 0.1 && !isStunned)
        {
            TopJumpAnimation();
        }
        //screen wrap script called here
        Vector3 tempPosition = transform.localPosition;
        if (screenBounds.AmIOutOfBounds(tempPosition))
        {
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(tempPosition);
            transform.position = newPosition;
        }
        else
        {
            transform.position = tempPosition;
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
                MoveAnimation();
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
                MoveAnimation();
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
        else
        {
            Vector3 altarDirection = altar.transform.position - transform.position;
            altarDirection = altarDirection.normalized;
            rb.velocity = altarDirection * altarPullSpeed;
            altarPullSpeed += 0.06f;
            CheckAltarPosition();
            if (CheckAltarPosition())
            {
                PlayerSacrifice();
            }
        }


    }
    bool CheckAltarPosition()
    {
        return (Vector2.Distance(new Vector2(transform.position.x,transform.position.y),new Vector2(altar.transform.position.x,altar.transform.position.y)) < 0.4) ? true : false;
    }

    //jump method
    void Jump()
    {
        if (rb == null) return;
        jumping = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        dootykZeme = false;
        JumpAnimation();
    }

    //move methods
    public void OnMove(float direction)
    {
        if (!isStunned)
        {
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
                this.transform.rotation = Quaternion.Euler(0,0,0);
            }
        }
    }
    public void Stop()
    {
        movingLeft = false;
        movingRight = false;
        if (!isStunned)
        {
            StopAnimation();
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
    public void OnDrop()
    {
        StartCoroutine(DropTimer());
    }
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
    IEnumerator DropTimer()
    {
        playerCollider.enabled = false;
        yield return new WaitForSeconds(0.135f);
        playerCollider.enabled = true;
    }
    public void StunPlayer(int killerId,float stunTime)
    {
        if (!isStunned)
        {
            lastHitId = killerId;
            stunCounter++;
            altarPullSpeed = 0.3f;
            baseStunTime += stunTime;
            StartCoroutine(StunTimer(baseStunTime));
        }
    }
    public void PlayerSacrifice()
    {
        //finish player death 
        InputHandler handler = transform.parent.gameObject.GetComponent<InputHandler>();
        handler.DestroyPlayer(lastHitId);
    }
    IEnumerator StunTimer(float timeForStun)
    {
        isStunned = true;
        StunAnimation();
        //throwableObjectScript.enabled = true;
        rb.gravityScale = 0f;

        StartCoroutine(StunBlicker(timeForStun));
        yield return new WaitForSeconds(timeForStun);

        rb.gravityScale = 1f;
        //throwableObjectScript.enabled = false;
        isStunned = false;
        animControl.ChangeAnimation(Animations.idleHand);
    }
    IEnumerator StunBlicker(float timeForStun)
    {
        Color color;
        for (int i = 0; i < 5; i++)
        {
            color = sr.color;
            color.a = 0;
            sr.color = color;
            yield return new WaitForSeconds(timeForStun / 10);
            color = sr.color;
            color.a = 1;
            sr.color = color;
            yield return new WaitForSeconds(timeForStun / 10);
        }

    }
    void MoveAnimation()
    {
        if (jumping) return;
        if (dootykZeme)
        {
            if (OC.holding)
            {
                animControl.ChangeAnimation(Animations.walkNoHand);
            }
            else
            {
                animControl.ChangeAnimation(Animations.walkHand);
            }
        }
    }
    void StopAnimation()
    {
        if (jumping) return;
        if (OC.holding)
        {
            animControl.ChangeAnimation(Animations.idleNoHand);
        }
        else
        {
            animControl.ChangeAnimation(Animations.idleHand);
        }
    }
    void JumpAnimation()
    {
        if (isStunned) return;
        if (OC.holding)
        {
            animControl.ChangeAnimation(Animations.jumpNoHand);
        }
        else
        {
            animControl.ChangeAnimation(Animations.jumpHand);
        }
    }
    void TopJumpAnimation()
    {
        if (OC.holding)
        {
            animControl.ChangeAnimation(Animations.fallingNoHand);
        }
        else
        {
            animControl.ChangeAnimation(Animations.fallingHand);
        }
    }
    void LandingAnimation()
    {
        if (OC.holding)
        {
            animControl.ChangeAnimation(Animations.landingNoHand);
        }
        else
        {
            animControl.ChangeAnimation(Animations.landingHand);
        }
    }
    void StunAnimation()
    {
        animControl.ChangeAnimation(Animations.down);
    }
}
