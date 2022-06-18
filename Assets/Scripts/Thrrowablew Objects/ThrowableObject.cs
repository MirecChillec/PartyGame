using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    Rigidbody2D rb;

    public Movement PM;

    public float objectWeight;
    public int baseThrowForce;
    public int baseGravityScale;
    private bool isThrown = false;

    GameObject owner;

    Vector3 throwArc;

    ObjectSpawnPosition spawnPos;
    ObjectSpawner objectSpawner;
    ScreenBounds screenBounds;


    private void Awake()
    {
        screenBounds = GameData.scrennBounds;
        rb = GetComponent<Rigidbody2D>();
        throwArc = new Vector3(baseThrowForce / objectWeight, baseThrowForce / objectWeight, 0);
    }
    private void Update()
    {
        if (screenBounds != null)
        {
            //destoing outside of screen
            if (screenBounds.AmIOutOfBounds(transform.localPosition))
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Throw(bool right)
    {
        isThrown = true;
        owner = transform.parent.gameObject;
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
        spawnPos.Release();
        rb.simulated = false;
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
    }
}
