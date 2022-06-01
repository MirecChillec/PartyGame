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

    [SerializeField]private bool dootykZeme = false;
    public GameObject GroundCheck;

    private bool movingRight = false;
    private bool movingLeft = false;
    private bool isFalling = false;
    private bool isLowJumping = false;

    private bool facingRight = true;

    public Collider2D playerCollider;

    public ScreenBounds screenBounds;

    private bool jumped = false;
    private bool dJumped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    public void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        dootykZeme = false;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (context.ReadValue<float>() > 0)
            {
                movingLeft = true;
                movingRight = true;
            }
            else
            {
                movingLeft = true;
                movingRight = false;
            }
        }
        else if (context.canceled)
        {
            movingLeft = false;
            movingRight = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
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
        }
    }
    public void OnDrop(InputAction.CallbackContext context)
    {

    }
    public void OnAction(InputAction.CallbackContext context)
    {

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

}
