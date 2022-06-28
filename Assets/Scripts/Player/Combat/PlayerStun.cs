using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    public bool isStunned = false;
    SpriteRenderer sr;
    float baseStunTime = 0;
    int lastHitId;
    float altarPullSpeed;
    Rigidbody2D rb;
    Movement move;
    public GameObject altar;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<Movement>();
    }
    private void FixedUpdate()
    {
        if (isStunned)
        {
            Vector3 altarDirection = altar.transform.position - transform.position;
            altarDirection = altarDirection.normalized;
            rb.velocity = altarDirection * altarPullSpeed;
            altarPullSpeed += 0.06f;
            CheckAltarPosition();
            if (CheckAltarPosition())
            {
                PlayerSacrifice();
            }
        }
    }
    public void StunPlayer(int killerId, float stunTime)
    {
        if (!isStunned)
        {
            lastHitId = killerId;
            altarPullSpeed = 0.3f;
            baseStunTime += stunTime;
            StartCoroutine(StunTimer(baseStunTime));
        }
    }
    IEnumerator StunTimer(float timeForStun)
    {
        isStunned = true;
        move.StunAnimation();
        //throwableObjectScript.enabled = true;
        rb.gravityScale = 0f;

        StartCoroutine(StunBlicker(timeForStun));
        yield return new WaitForSeconds(timeForStun);

        rb.gravityScale = 1f;
        //throwableObjectScript.enabled = false;
        isStunned = false;
        move.animControl.ChangeAnimation(Animations.idleHand);
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
    public void PlayerSacrifice()
    {
        //finish player death 
        InputHandler handler = transform.parent.gameObject.GetComponent<InputHandler>();
        handler.DestroyPlayer(lastHitId);
    }
    bool CheckAltarPosition()
    {
        return (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(altar.transform.position.x, altar.transform.position.y)) < 0.4) ? true : false;
    }

}
