using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CircleCollider2D triggerCollider;

    public bool isStunned;
    public int stunCounter = 0;
    public float baseStunTime = 1f;

    ThrowableObject throwableObjectScript;
    Movement playerMovementScript;
    PlayerStunnedPickup playerStunnedPickupScript;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        triggerCollider = GetComponentInChildren<CircleCollider2D>();

        throwableObjectScript = this.gameObject.GetComponent<ThrowableObject>();
        playerMovementScript = this.gameObject.GetComponent<Movement>();
        playerStunnedPickupScript = this.gameObject.GetComponentInChildren<PlayerStunnedPickup>();
    }

    public void StunPlayer()
    {
        stunCounter++;
        StartCoroutine(StunTimer(baseStunTime + (stunCounter / 2)));
    }

    IEnumerator StunTimer(float timeForStun)
    {
        isStunned = true;
        playerMovementScript.isStunned = true;
        playerStunnedPickupScript.isStunned = true;
        playerMovementScript.enabled = false;
        //throwableObjectScript.enabled = true;
        triggerCollider.enabled = true;
        rb.gravityScale = 0f;
        
        StartCoroutine(StunBlicker(timeForStun));
        yield return new WaitForSeconds(timeForStun);

        rb.simulated = true;
        rb.gravityScale = 1f;
        throwableObjectScript.enabled = false;
        playerMovementScript.enabled = true;
        isStunned = false;
        playerMovementScript.isStunned = false;
        playerStunnedPickupScript.isStunned = false;
        triggerCollider.enabled = false;
    }
    IEnumerator StunBlicker(float timeForStun)
    {
        Color color;
        for (int i = 0; i < 5; i++)
        {
            color = sr.color;
            color.a = 0;
            sr.color = color;
            yield return new WaitForSeconds(timeForStun / 10);
            color = sr.color;
            color.a = 1;
            sr.color = color;
            yield return new WaitForSeconds(timeForStun / 10);
        }

    }
}
