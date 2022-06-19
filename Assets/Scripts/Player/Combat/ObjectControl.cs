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
    void Throw()
    {
        canPickUp = true;
        state = Throwable.idle;
        detector.Throw(movement.facingRight);
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

}
public enum Throwable
{
    idle,
    holding
}

