using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpPower = 3f;
    public float movementSpeed = 2f;
    public int jumpCounter;
    private int jumpCounterMax = 1;

    public float fallMultiplier = 2.5f;
    public float lowerJumpMultiplier = 2f;

    public bool dootykZeme = false;
    public GameObject GroundCheck;

    private bool movingRight = false;
    private bool movingLeft = false;
    private bool isFalling = false;
    private bool isLowJumping = false;

    private bool facingRight = true;

    public Collider2D playerCollider;

    public ScreenBounds screenBounds;

    Vector2 movement;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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

        //reactive jumps for: w,up arrow,space
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            if (dootykZeme == true)
            {
                Jump();
            }
            else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            {
                if (jumpCounter < jumpCounterMax)
                {
                    Jump();
                    jumpCounter++;
                }
            }
        }

        //checking for vertical velocity and multiplying it
        if (rb.velocity.y < 13)
        {
            isFalling = true;
            isLowJumping = false;
        }
        else if (rb.velocity.y > 13 && !Input.GetKey(KeyCode.W))
        {
            isFalling = false;
            isLowJumping = true;
        }

        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            movingRight = true;
            movingLeft = false;
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
        {
            movingRight = false;
            movingLeft = true;
        }
        else
        {
            movingRight = false;
            movingLeft = false;
        }
    }


    private void FixedUpdate()
    {
        if (movingRight == true)
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
        else if (movingLeft == true)
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
    }

}
