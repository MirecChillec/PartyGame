using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip stun;
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
    bool holding = false;
    bool blink;
    public ObjectControl OC;
    public float playerStunMax = 12f;
    AnimationsControler anim;
    public float throwX;
    public float throwY;
    public float gravScal;
    bool death = false;
    public Animator arrowAnim;
    public LayerMask altarMask;

    private void Awake()
    {
        blink = false;
        thrown = false;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<Movement>();
        OC = GetComponent<ObjectControl>();
        anim = GetComponent<AnimationsControler>();
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
            audio.clip = stun;
            audio.Play();
            OC.stunned = true;
            OC.detector.Release();
            blink = true;
            lastHitId = killerId;
            altarPullSpeed = 0.3f;
            baseStunTime += stunTime;
            Mathf.Clamp(baseStunTime,0f,playerStunMax);
            StartCoroutine(StunTimer());
        }
    }
    IEnumerator StunTimer()
    {
        Stun();
        arrowAnim.Play("Stun");

        //StartCoroutine(StunBlicker());
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
            if (!blink)
            {
                break;
            }
        }
    }
    public void PlayerSacrifice()
    {
        if (death) return;
        death = true;
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
        if (holding)
        {
            HoldingPlayerReset();
        }
        arrowAnim.Play("Idle");
        anim.HandBool(false);
        rb.gravityScale = 1;
        OC.stunned = false;
        blink = false;
        holding = false;
        move.enabled = true;
        this.gameObject.layer = 8;
        isStunned = false;
        anim.ChangeAnimation(Animations.idleHand);
        downCol.enabled = false;
        rb.simulated = true;
        rb.gravityScale = 1;
        col.enabled = true;
        move.isStunned = false;
        thrown = false;
        transform.SetParent(inputParent);
    }
    public void StunRelease()
    {
        if (holding)
        {
            HoldingPlayerReset();
        }
        arrowAnim.Play("Stun");
        anim.HandBool(false);
        holding = false;
        rb.simulated = true;
        rb.gravityScale = 1;
        thrown = false;
        transform.SetParent(inputParent);

    }
    public void PickUp(Transform parent)
    {
        if (!isStunned) return;
        arrowAnim.Play("Idle");
        holding = true;
        rb.simulated = false;
        rb.gravityScale = 0;
        transform.SetParent(parent);
        transform.position = parent.position;
    }
    public void Throw(bool right)
    {
        HoldingPlayerReset();
        blink = false;
        holding = false;
        grounded = false;
        thrown = true;
        this.gameObject.layer = 10;
        rb.simulated = true;
        rb.gravityScale = 1;
        downCol.enabled = true;
        rb.velocity = Vector2.zero;
        transform.SetParent(inputParent);
        rb.gravityScale = gravScal;
        if (right)
        {
            rb.AddForce(new Vector3(throwX, throwY, 0));
        }
        else
        {
            rb.AddForce(new Vector3(-throwX, throwY, 0));
        }
        StartCoroutine(GetUp());
    }
    public void ThrownDown()
    {
        HoldingPlayerReset();
        rb.gravityScale = gravScal;
        blink = false;
        holding = false;
        grounded = false;
        thrown = true;
        this.gameObject.layer = 10;
        rb.simulated = true;
        rb.gravityScale = 1;
        rb.velocity = Vector2.zero;
        col.enabled = true;
        transform.SetParent(inputParent);
        rb.AddForce(Vector3.down * 4000);
        StartCoroutine(GetUp());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (thrown)
        {
            if (collision.gameObject.tag == "Altar")
            {
                PlayerSacrifice();
            }
        }else if (CheckAltar() && isStunned)
        {
            PlayerSacrifice();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CheckAltar() && isStunned)
        {
            //PlayerSacrifice();
        }
    }
    bool CheckAltar()
    {
        if (Physics2D.OverlapCapsule(downCol.bounds.center, downCol.bounds.size, CapsuleDirection2D.Horizontal, 0f,altarMask))
        {
            print("altar");
            return true;
        }
        return false;
    }
    IEnumerator GetUp()
    {
        while (thrown)
        {
            if (grounded)
            {
                Release();
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    void HoldingPlayerReset()
    {
        GameObject holdingPlayer = transform.parent.transform.parent.gameObject;
        AnimationsControler anim = holdingPlayer.GetComponent<AnimationsControler>();
        anim.ChangeAnimation(Animations.idleHand);
        anim.HandBool(false);
        ObjectControl OC = holdingPlayer.GetComponent<ObjectControl>();
        OC.holding = false;
        OC.detector.PlayerReset();
    }
}
