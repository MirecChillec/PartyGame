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
}
