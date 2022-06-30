using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFaling : MonoBehaviour
{
    public Collider2D groundCol;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Release()
    {
        groundCol.enabled = true;
        rb.gravityScale = 1;
    }
    public void Stop()
    {
        rb.gravityScale = 0;
        groundCol.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            rb.gravityScale = 0;
            groundCol.enabled = false;
        }
    }
}
