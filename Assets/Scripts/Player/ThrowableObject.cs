using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    ObjectState myState = ObjectState.idle;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (myState == ObjectState.idle)
            {
                myState = ObjectState.picked;
                transform.position = collision.transform.position;
                transform.position += Vector3.up * 0.2f;
                transform.parent = collision.transform;

                rb.simulated = false;
                //rb.isKinematic = true;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(myState == ObjectState.picked)
            {
                myState = ObjectState.thrown;
                rb.simulated = true;
                transform.parent = null;
                rb.AddForce(throwArc);
                rb.gravityScale = baseGravityScale * objectWeight;
            }
        }
    }
}

public enum ObjectState
{
    idle, picked, thrown
}