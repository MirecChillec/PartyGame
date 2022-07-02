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
    bool holding = false;
    private void Start()
    {
        canPick = false;
        objekt = null;
        stunedPlayer = null;
        StartCoroutine(StartWait());
    }
    private bool CheckObject()
    {
        //ak moze zobrat objekt
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
                    return true;
                }
                else if (cols[0].gameObject.tag == "Player")
                {
                    stunedPlayer = cols[0].gameObject.GetComponent<PlayerStun>();
                    return true;
                }
            }
        }
        return false;
    }
    public bool Pick()
    {
        if (canPick)
        {
            if (CheckObject())
            {
                if (objekt != null)
                {
                    holding = true;
                    objekt.PickUp(holder, character);
                    controler.holding = true;
                    controler.animControl.ChangeAnimation(Animations.idleNoHand);
                    controler.animControl.HandBool(true);
                    return true;
                }
                else if (stunedPlayer != null)
                {
                    holding = true;
                    controler.holding = true;
                    stunedPlayer.PickUp(holder);
                    controler.animControl.ChangeAnimation(Animations.idleNoHand);
                    controler.animControl.HandBool(true);
                    return true;
                }
            }
        }
        return false;
    }
    public void Throw(bool direction)
    {
        if (objekt != null)
        {
            holding = false;
            controler.holding = false;
            canPick = true;
            objekt.Throw(direction, this.gameObject);
            //nulovanie objekt premennej inak by hrac vedel chytit hodeny objekt pocas toho ako leti
            objekt = null;
            controler.animControl.ChangeAnimation(Animations.idleHand);
            controler.animControl.HandBool(false);
        }
        else if (stunedPlayer != null)
        {
            holding = false;
            controler.holding = false;
            canPick = true;
            stunedPlayer.Throw(direction);
            stunedPlayer = null;
            controler.animControl.ChangeAnimation(Animations.idleHand);
            controler.animControl.HandBool(false);

        }
    }
    public void ThrowDown()
    {
        if (objekt != null)
        {
            holding = false;
            objekt.ThrowDown(this.gameObject);
            
        }
        else if (stunedPlayer != null)
        {
            holding = false;
            stunedPlayer.ThrownDown();
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
    public void PlayerReset()
    {
        stunedPlayer = null;
    }
    public void Release()
    {
        controler.holding = false;
        if(objekt != null)
        {
            holding = false;
            objekt.Release();
            objekt = null;
        }else if(stunedPlayer != null)
        {
            holding = false;
            stunedPlayer.StunRelease();
            stunedPlayer = null;
        }
    }
}
public enum Character
{
    butcher,
    detective,
    nobleman,
    ocultist
}
