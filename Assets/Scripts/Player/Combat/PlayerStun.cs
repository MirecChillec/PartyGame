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
    public Rigidbody2D rb;
    Movement move;
    public GameObject altar;
    public Collider2D col;
    bool thrown;
    public Transform inputParent { get; set; }
    private void Awake()
    {
        thrown = false;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<Movement>();
    }
    private void FixedUpdate()
    {
        //puling to altar
       /* if (isStunned)
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
        }*/
    }
    //stun timers
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
        this.gameObject.layer = 9;
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
        if (!thrown)
        {
            this.gameObject.layer = 8;
            Release();
        }
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
    //throwing
    public void Release()
    {
        rb.simulated = true;
        rb.gravityScale = 1;
        col.enabled = true;
        transform.SetParent(inputParent);
        move.enabled = true;
    }
    public void PickUp(Transform parent)
    {
        rb.simulated = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        move.enabled = false;
        col.enabled = false;
        transform.SetParent(parent);
        transform.position = parent.position;
    }
    public void Throw(bool right)
    {
        thrown = true;
        this.gameObject.layer = 10;
        rb.simulated = true;
        rb.gravityScale = 1;
        col.enabled = true;
        transform.SetParent(inputParent);
        if (right)
        {
            rb.AddForce(new Vector3(500, 500, 0));
        }
        else
        {
            rb.AddForce(new Vector3(-500, -500, 0));
        }
    }
    public void ThrownDown()
    {
        thrown = true;
        this.gameObject.layer = 10;
        rb.simulated = true;
        rb.gravityScale = 1;
        col.enabled = true;
        transform.SetParent(inputParent);
        rb.AddForce(Vector3.down * 10);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (thrown)
        {
            if(collision.gameObject.tag == "Altar")
            {
                PlayerSacrifice();
            }
        }
    }
}
