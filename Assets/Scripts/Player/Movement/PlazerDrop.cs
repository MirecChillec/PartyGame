using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlazerDrop : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    Vector3 ofset;
    public GroundCheck groundCheck;
    bool droping;
    public float dropTime;
    public Movement move;
    private void Awake()
    {
        ofset = player.position - transform.position;
    }
    private void FixedUpdate()
    {
        player.position = new Vector3(player.position.x, transform.position.y + ofset.y, player.position.z);
        if (groundCheck.grounded && !move.jumped && move.dJumped && !droping)
        {
            Vector3 vel = rb.velocity;
            vel.y = 0;
            rb.velocity = vel;
        }
    }
    public void Drop()
    {
        if (!droping && groundCheck.grounded)
        {
            droping = true;
            StartCoroutine(DropTimer());
        }
    }
    IEnumerator DropTimer()
    {
        this.gameObject.layer = 15;
        yield return new WaitForSeconds(dropTime);
        this.gameObject.layer = 14;
        droping = false;

    }
}
