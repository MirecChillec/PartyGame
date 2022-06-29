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
    public Collider2D downCol;
    public bool thrown { get; private set; }
    public bool grounded { get; set; }
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
            StartCoroutine(StunTimer());
        }
    }
    IEnumerator StunTimer()
    {
        Stun();

        StartCoroutine(StunBlicker());
        yield return new WaitForSeconds(baseStunTime);

        if (!thrown)
        {
            Release();
        }
    }
    void Stun()
    {
        rb.gravityScale = 0f; 
        move.isStunned = true;
        move.StunAnimation();
        move.enabled = false;
        rb.velocity = Vector2.zero;
        col.enabled = false;
        downCol.enabled = true;
        this.gameObject.layer = 9;
        isStunned = true;    
    }
    IEnumerator StunBlicker()
    {
        Color color;
        for (int i = 0; i < 5; i++)
        {
            color = sr.color;
            color.a = 0;
            sr.color = color;
            yield return new WaitForSeconds(baseStunTime / 10);
            color = sr.color;
            color.a = 1;
            sr.color = color;
            yield return new WaitForSeconds(baseStunTime / 10);
        }
    }
    public void PlayerSacrifice()
    {
        //finish player death 
        InputHandler handler = transform.parent.gameObject.GetComponent<InputHandler>();
        StopAllCoroutines();
        handler.DestroyPlayer(lastHitId);
    }
    bool CheckAltarPosition()
    {
        return (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(altar.transform.position.x, altar.transform.position.y)) < 0.4) ? true : false;
    }
    //throwing
    public void Release()
    {
        print("release");
        move.enabled = true;
        this.gameObject.layer = 8;
        //throwableObjectScript.enabled = false;
        isStunned = false;
        move.animControl.ChangeAnimation(Animations.idleHand);
        downCol.enabled = false;
        rb.simulated = true;
        rb.gravityScale = 1;
        col.enabled = true;
        move.isStunned = false;
        thrown = false;
        transform.SetParent(inputParent);
    }
    public void PickUp(Transform parent)
    {
        if (!isStunned) return;
        rb.simulated = false;
        rb.gravityScale = 0;
        //downCol.enabled = false;
        transform.SetParent(parent);
        transform.position = parent.position;
    }
    public void Throw(bool right)
    {
        grounded = false;
        thrown = true;
        this.gameObject.layer = 10;
        rb.simulated = true;
        rb.gravityScale = 1;
        downCol.enabled = true;
        rb.velocity = Vector2.zero;
        transform.SetParent(inputParent);
        if (right)
        {
            rb.AddForce(new Vector3(2000, 500, 0));
        }
        else
        {
            rb.AddForce(new Vector3(-2000, -500, 0));
        }
        StartCoroutine(GetUp());
    }
    public void ThrownDown()
    {
        grounded = false;
        thrown = true;
        this.gameObject.layer = 10;
        rb.simulated = true;
        rb.gravityScale = 1;
        rb.velocity = Vector2.zero;
        col.enabled = true;
        transform.SetParent(inputParent);
        rb.AddForce(Vector3.down * 1000);
        StartCoroutine(GetUp());
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
    IEnumerator GetUp()
    {
        while (thrown)
        {
            if (grounded)
            {
                Release();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}