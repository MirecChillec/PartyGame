using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Movement PM;
    public PlayerStun stun;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PM.JumpRest();
            if (stun.thrown)
            {
                stun.grounded = true;
                stun.Release();
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Ground"))
        {
            PM.JumpRest();
            if (stun.thrown)
            {
                stun.grounded = true;
                stun.Release();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Ground"))
        {
            PM.DisableJump();
        }
    }
}