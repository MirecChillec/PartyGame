using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public ObjectControl controler;
    //objekt na hadzanie
    private ThrowableObject objekt;
    //player colider na urcenie priestoru na detekciu
    public Collider2D playerColider;
    //array ziskan7ch objektov pomocou fyziky
    public Collider2D[] cols;
    //layermaska objektov
    public LayerMask mask;
    //cas na reset na zobranie dalsieho objektu
    public float pickTimer;
    public Character character;
    [SerializeField] private bool canPick;
    public Transform holder;
    PlayerStun stunedPlayer;
    private void Start()
    {
        canPick = false;
        objekt = null;
        stunedPlayer = null;
        StartCoroutine(StartWait());
    }
    private void FixedUpdate()
    {
        //ak moze zobrat objekt
        if (canPick)
        {
            //detekcia objectov
            cols = Physics2D.OverlapBoxAll(playerColider.bounds.center, playerColider.bounds.size, 0, mask);
            if (cols.Length != 0)
            {
                if (controler.state == Throwable.idle)
                {
                    //ziskanie thowable skriptu na volanie funkcie na hadzanie
                    if (cols[0].gameObject.tag == "Throwable")
                    {
                        objekt = cols[0].gameObject.GetComponent<ThrowableObject>();
                    }else if(cols[0].gameObject.tag == "Player")
                    {
                        stunedPlayer = cols[0].gameObject.GetComponent<PlayerStun>();
                    }
                }
            }
        }
    }
    public void Pick()
    {
        if (canPick)
        {
            if (objekt != null)
            {
                objekt.PickUp(holder,character);
                controler.holding = true;
                controler.animControl.ChangeAnimation(Animations.idleNoHand);
                controler.animControl.HandBool(true);
            }else if (stunedPlayer!= null)
            {
                controler.holding = true;
                stunedPlayer.PickUp(holder);
                controler.animControl.ChangeAnimation(Animations.idleNoHand);
                controler.animControl.HandBool(true);
            }
        }
    }
    public void Throw(bool direction)
    {
        if (objekt != null)
        {
            controler.holding = false;
            canPick = false;
            objekt.Throw(direction,this.gameObject);
            //nulovanie objekt premennej inak by hrac vedel chytit hodeny objekt pocas toho ako leti
            objekt = null;
            StartCoroutine(PickTimer());
            controler.animControl.ChangeAnimation(Animations.idleHand);
            controler.animControl.HandBool(false);
        }else if(stunedPlayer != null)
        {
            controler.holding = false;
            canPick = false;
            stunedPlayer.Throw(direction);
            stunedPlayer = null;
            StartCoroutine(PickTimer());
            controler.animControl.ChangeAnimation(Animations.idleHand);
            controler.animControl.HandBool(false);

        }
    }
    IEnumerator PickTimer()
    {
        //pauza po hode mozne vyuzit na nejaky debaf
        //bez corontine bolo mozne chytit hodeny objekt pocas toho ako leti
        canPick = false;
        yield return new WaitForSeconds(pickTimer);
        canPick = true;
    }
    public void ThrowDown()
    {
        if (objekt != null)
        {
            objekt.ThrowDown();
            StartCoroutine(PickTimer());
        }
        else if(stunedPlayer != null)
        {
            stunedPlayer.ThrownDown();
            StartCoroutine(PickTimer());
        }
    }
    IEnumerator StartWait()
    {
        objekt = null;
        yield return new WaitForSeconds(0.1f);
        objekt = null;
        stunedPlayer = null;
        canPick = true;
    }
}
public enum Character
{
    butcher,
    detective,
    nobleman,
    ocultist
}
