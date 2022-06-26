using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunnedPickup : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public bool isStunned;
    public int stunCounter = 0;
    public float baseStunTime = 1f;

    ThrowableObject throwableObjectScript;
    Movement playerMovementScript;
    ObjectControl objectControlScript;

    Transform playerTransform;
    GameObject otherPlayer;
    Transform pickedPlayer;
    Collider2D colliderPlayer;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        sr = GetComponentInParent<SpriteRenderer>();

        throwableObjectScript = this.gameObject.GetComponentInParent<ThrowableObject>();
        playerMovementScript = this.gameObject.GetComponentInParent<Movement>();
        objectControlScript = this.gameObject.GetComponentInParent<ObjectControl>();
    }

    void Update()
    {
        if (!isStunned)
        {
            transform.parent.parent = null;
            if (playerTransform != null)
            {
                playerTransform.GetComponent<ObjectControl>().SetIdle();
                playerTransform = null;
            }
            
        }
    }

    public void ThrowPlayer( )
    {
        //playerTransform.parent.parent = null;
        colliderPlayer.gameObject.GetComponent<PlayerStunnedPickup>().Thrown();
        Debug.Log("throw player");
        //playerTransform.GetComponent<Movement>();
        //playerTransform.GetComponentInChildren<PlayerStunnedPickup>().Thrown();
        //otherPlayer.GetComponent<PlayerStunnedPickup>().Thrown();
        //Thrown();
    }

    public void Thrown()
    {
        //rb.AddForce(new Vector2(100,100), ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * 100, ForceMode2D.Impulse);

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isStunned)
        {
            if (collision.gameObject.tag == "Player" /*&& collision.gameObject.GetComponentInParent<PlayerStun>().isStunned*/)
            {
                colliderPlayer = collision;
                //pickedPlayer = collision.gameObject.transform;
                playerTransform = collision.gameObject.transform;
                otherPlayer = collision.gameObject;
                //Debug.Log("stun and pickup");

                transform.parent.position = playerTransform.position;
                transform.parent.position += Vector3.up * 4f;
                transform.parent.parent = playerTransform;
                rb.simulated = false;
                /*pickedPlayer.parent.position = transform.position;
                pickedPlayer.parent.position += Vector3.up * 4f;
                pickedPlayer.parent.parent = transform.parent;
                collision.gameObject.GetComponentInParent<Rigidbody2D>().simulated = false;*/

                //objectControlScript.PickPlayer();

                playerTransform.GetComponent<ObjectControl>().PickPlayer();
                //playerTransform.GetComponentInChildren<PlayerStunnedPickup>().Thrown();
                Thrown();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //collision.gameObject.GetComponent<ObjectControl>().state = Throwable.idle;
    }
}
