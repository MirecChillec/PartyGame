using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectControl : MonoBehaviour
{
    public AudioClip throwSound;
    public AudioSource audio;
    public Throwable state { get; private set; }
    public bool canPickUp { get; set; }
    public Movement movement;
    public ObjectDetection detector;
    bool downKeybindPressed;
    public AnimationsControler animControl;
    public bool stunned { get; set; }
    public bool holding { get; set; }
    private void Start()
    {
        animControl = GetComponent<AnimationsControler>();
        holding = false;
        state = Throwable.idle;
        canPickUp = true;
        stunned = false;
    }
    void PickUp()
    {
        if (stunned) return;
        if (detector.Pick())
        {
            holding = true;
            canPickUp = false;
            state = Throwable.holding;
        }
    }
    void Throw()
    {
        audio.clip = throwSound;
        audio.Play();
        holding = false;
        canPickUp = true;
        state = Throwable.idle;
        detector.Throw(movement.facingRight);
    }
    public void OnAction()
    {
        if (!movement.isStunned)
        {
            if (state == Throwable.idle && canPickUp)
            {
                PickUp();
            }
            else if (state == Throwable.holding && !canPickUp)
            {
                if (downKeybindPressed)
                {
                    ThrowDown();
                }
                else
                {
                    Throw();
                }
            }
        }
    }
    //only throw straight down / release object
    void ThrowDown()
    {
        holding = false;
        canPickUp = true;
        state = Throwable.idle;
        detector.ThrowDown();
    }
    //bool for checking if key down / S (or equivalent) is pressed
    public void OnDown()
    {
        downKeybindPressed = true;
    }
    public void CancelDown()
    {
    downKeybindPressed = false;
    }
}
public enum Throwable
{
    idle,
    holding
}

