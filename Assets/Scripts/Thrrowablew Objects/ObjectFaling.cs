using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFaling : MonoBehaviour
{
    public Collider2D groundCol;
    Rigidbody2D rb;
    Vector3 ofset;
    GameObject parent;
    bool faling = true;
    private void Awake()
    {
        parent = transform.parent.gameObject;
        ofset = parent.transform.position - transform.position; 
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }
    private void FixedUpdate()
    {
        if (faling)
        {
            parent.transform.position = transform.position + ofset;
        }
    }
    public void Fall()
    {
        rb.velocity = Vector3.zero;
        groundCol.enabled = true;
        rb.gravityScale = 1;
        faling = true;
        rb.simulated = true;
    }
    public void Stop()
    {
        faling = false;
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
        groundCol.enabled = false;
        parent.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Stop();
        }
    }
}
