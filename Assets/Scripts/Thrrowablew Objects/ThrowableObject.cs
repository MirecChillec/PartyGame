using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    Rigidbody2D rb;

    public float objectWeight;
    public int baseThrowForce;
    public int baseGravityScale;

    bool isThrown;
    public PlayerStun PM;
    GameObject owner;

    Vector3 throwArc;

    ObjectSpawnPosition spawnPos;
    ObjectSpawner objectSpawner;
    ScreenBounds screenBounds;

    public bool player;

    public Transform spawnerPos;
    public SpriteRenderer sr;
    public Sprite[] sprites;
    public Sprite baseSprite;

    public float stunTime;

    private void Awake()
    {
        screenBounds = GameData.scrennBounds;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //throwArc = new Vector3(baseThrowForce / objectWeight, baseThrowForce / objectWeight, 0);
    }
    private void Update()
    {
        if (!player)
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
    }

    public void Throw(bool right, GameObject parent)
    {
        isThrown = true;
        owner = parent;
        rb.simulated = true;
        transform.parent = spawnerPos;
        throwArc = new Vector3(baseThrowForce / objectWeight, (baseThrowForce / objectWeight/2), 0);
        if (right)
        {
            rb.AddForce(throwArc);
        }else
        {
            rb.AddForce(new Vector3(-throwArc.x,throwArc.y,throwArc.z));
        }
        rb.gravityScale = baseGravityScale * objectWeight;
        sr.sprite = baseSprite;
    }
    public void PickUp(Transform player,Character character)
    {
        transform.position = player.position;
        transform.position += Vector3.up * 0.2f;
        transform.parent = player;
        spawnPos.Release();
        ChangeSprite(character);
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
        transform.parent = spawnerPos;
        rb.AddForce(Vector3.down * 3f);
        rb.gravityScale = baseGravityScale * objectWeight * 3;
        sr.sprite = baseSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("colision");
        if (collision.gameObject.tag == "Player" && isThrown)
        {
            //Debug.Log("object is thrown and touching player");
            PM = collision.GetComponent<PlayerStun>();
            if (owner != PM.gameObject)
            {
                int id = owner.transform.parent.GetComponent<InputHandler>().playerId;
                //Debug.Log("stuning player");
                PM.StunPlayer(id, stunTime);
            }
        }
    }
    public void Release()
    {
        owner = null;
        rb.simulated = false;
        transform.parent = spawnerPos;
        sr.sprite = baseSprite;

    }
    void ChangeSprite(Character typ)
    {
        switch (typ)
        {
            case Character.butcher:
                sr.sprite = sprites[0];
                break;
            case Character.detective:
                sr.sprite = sprites[1];
                break;
            case Character.nobleman:
                sr.sprite = sprites[2];
                break;
            case Character.ocultist:
                sr.sprite = sprites[3];
                break;
        }
    }
}
