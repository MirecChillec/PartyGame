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


    ThrowableObject throwableObjectScript;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        altar = GameObject.Find("Altar");

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
        }


    }
    //jump method
    void Jump()
    {
        if (rb == null) return;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        dootykZeme = false;
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
                sr.flipX = true;
            }
            else
            {
                movingLeft = true;
                movingRight = false;
                facingRight = false;
                sr.flipX = false;
            }
        }
    }
    public void Stop()
    {
        movingLeft = false;
        movingRight = false;
    }
    //jump call
    public void OnJump()
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
    public void StunPlayer()
    {
        stunCounter++;
        altarPullSpeed = 0.3f;
        StartCoroutine(StunTimer(baseStunTime + (stunCounter / 2)));
    }
    public void PlayerSacrifice()
    {
        //finish player death 
        this.gameObject.SetActive(false);


    }
    IEnumerator StunTimer(float timeForStun)
    {
        isStunned = true;
        //throwableObjectScript.enabled = true;
        rb.gravityScale = 0f;

        StartCoroutine(StunBlicker(timeForStun));
        yield return new WaitForSeconds(timeForStun);

        rb.gravityScale = 1f;
        //throwableObjectScript.enabled = false;
        isStunned = false;
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


}
