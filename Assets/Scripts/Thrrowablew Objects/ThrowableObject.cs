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
    public void ThrowDown()
    {
        isThrown = true;
        owner = transform.parent.gameObject;
        rb.simulated = true;
        transform.parent = null;
        rb.AddForce(Vector3.down * 3f);
        rb.gravityScale = baseGravityScale * objectWeight * 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isThrown)
        {
            PM = collision.GetComponent<Movement>();
            if(owner != PM.gameObject)
            {
                PM.StunPlayer();
            }
        }
    public void Init(ObjectSpawnPosition spawnPosition,ScreenBounds bounds,ObjectSpawner spawner)
    {
        spawnPos = spawnPosition;
        screenBounds = bounds;
        objectSpawner = spawner;
    }
    private void OnDestroy()
    {
        objectSpawner.DestroiedObject();
    }
}
