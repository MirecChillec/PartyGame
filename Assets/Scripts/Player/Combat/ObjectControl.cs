using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectControl : MonoBehaviour
{
    public Throwable state { get; private set; }
    public bool canPickUp { get; set; }
    public Movement movement;
    public ObjectDetection detector;
    bool downKeybindPressed;
    PlayerStunnedPickup playerStunnedPickupScript;
    private void Awake()
    {
        playerStunnedPickupScript = GetComponentInChildren<PlayerStunnedPickup>();

    }
    private void Start()
    {
        state = Throwable.idle;
        canPickUp = true;
    }
    void PickUp()
    {
        canPickUp = false;
        state = Throwable.holding;
        detector.Pick();
    }
    //called from PlayerStunnedPickup
    public void PickPlayer()
    {
        canPickUp = false;
        state = Throwable.holdingPlayer;
        //*******************
    }
    void Throw()
    {
        canPickUp = true;
        state = Throwable.idle;
        detector.Throw(movement.facingRight);
    }
    void ThrowPlayer( )
    {
        canPickUp = true;
        state = Throwable.idle;
        playerStunnedPickupScript.ThrowPlayer();
        //*******************
    }
    public void OnAction()
    {
        if (state == Throwable.idle && canPickUp)
        {
            PickUp();
        }
        else if (state == Throwable.holding && !canPickUp)
        {
            Throw();
        }else if(state == Throwable.holdingPlayer && !canPickUp)
        {
            ThrowPlayer();
        }
    }
    //only throw straight down / release object
    void ThrowDown()
    {
        canPickUp = true;
        state = Throwable.idle;
        detector.ThrowDown();
    }
    //bool for checking if key down / S (or equivalent) is pressed
    public void OnDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            downKeybindPressed = true;
        }
        else if (ctx.canceled)
        {
            downKeybindPressed = false;
        }
    }

    public void SetIdle()
    {
        state = Throwable.idle;
    }

}


public enum Throwable
{
    idle,
    holding,
    holdingPlayer
}

