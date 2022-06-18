using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Movement PM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PM.JumpRest();
        }
        if (collision.gameObject.CompareTag("Altar"))
        {
            if (PM.isStunned)
            {
                //player sacrificed
                PM.PlayerSacrifice();
            }
            else
            {
                PM.JumpRest();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PM.JumpRest();
        }
        if (collision.gameObject.CompareTag("Altar"))
        {
            if (PM.isStunned)
            {
                //player sacrificed
                PM.PlayerSacrifice();
            }
            else
            {
                PM.JumpRest();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PM.DisableJump();
        }
        if (collision.gameObject.CompareTag("Altar"))
        {
            PM.DisableJump();
        }
    }
}