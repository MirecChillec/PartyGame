using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    Rigidbody2D rb;

    public float objectWeight;
    public int baseThrowForce;
    public int baseGravityScale;

    Vector3 throwArc;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        throwArc = new Vector3(baseThrowForce / objectWeight, baseThrowForce / objectWeight, 0);
    }

    public void Throw(bool right)
    {
        rb.simulated = true;
        transform.parent = null;
        if (right)
        {
            rb.AddForce(throwArc);
        }else
        {
            rb.AddForce(new Vector3(-throwArc.x,throwArc.y,throwArc.z));
        }
        rb.gravityScale = baseGravityScale * objectWeight;
    }
    public void PickUp(Transform player)
    {
        transform.position = player.position;
        transform.position += Vector3.up * 0.2f;
        transform.parent = player;

        rb.simulated = false;
    }
}
