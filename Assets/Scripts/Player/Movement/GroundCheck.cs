using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Movement PM;
    public PlayerStun stun;
    public bool grounded { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            PM.JumpRest();
            if (stun.thrown)
            {
                stun.grounded = true;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            PM.JumpRest();
            if (stun.thrown)
            {
                stun.grounded = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            PM.DisableJump();
        }
    }
}