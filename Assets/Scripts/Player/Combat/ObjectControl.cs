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
    private void Start()
    {
        state = Throwable.idle;
        canPickUp = false;
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
    public void OnAction(InputAction.CallbackContext ctx) {
        if (ctx.started)
        {
            if(state == Throwable.idle && canPickUp)
            {
                PickUp();
            }else if(state == Throwable.holding && !canPickUp)
            {
                Throw();
            }
        }
    }
}
public enum Throwable { 
    idle,
    holding
}

