using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsControler : MonoBehaviour
{
    public Animator anim;
    Animations current;
    public Movement move;
    public ObjectControl OC;
    private void Awake()
    {
        move = GetComponent<Movement>();
        OC = GetComponent<ObjectControl>();
    }
    private void Start()
    {
        current = Animations.idleHand;
    }
    private void Update()
    {
        if ((current == Animations.landingNoHand || current == Animations.landingHand) && (anim.GetCurrentAnimatorStateInfo(0).IsName("idleNoHand") || anim.GetCurrentAnimatorStateInfo(0).IsName("idleHand")))
        {
            anim.SetBool("hand", false);
        }
    }

    public void ChangeAnimation(Animations animation)
    {
        if (anim == null)
        {
            Debug.LogError("Missing animator reference " + this.gameObject.name);
            return;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(animation.ToString()))
        {
            return;
        }
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName(Animations.jumpHand.ToString()) || anim.GetCurrentAnimatorStateInfo(0).IsName(Animations.jumpNoHand.ToString())) && (animation == Animations.idleHand || animation == Animations.idleNoHand))
        {
            return;
        }
        anim.Play(animation.ToString());
    }
    public void HandBool(bool value)
    {
        anim.SetBool("hand", value);
    }
    public void MoveAnimation()
    {
        if (move.jumping) return;
        if (move.dootykZeme)
        {
            if (OC.holding)
            {
                ChangeAnimation(Animations.walkNoHand);
            }
            else
            {
                ChangeAnimation(Animations.walkHand);
            }
        }
    }
    public void StopAnimation()
    {
        if (move.jumping) return;
        if (OC.holding)
        {
            ChangeAnimation(Animations.idleNoHand);
        }
        else
        {
            ChangeAnimation(Animations.idleHand);
        }
    }
    public void JumpAnimation()
    {
        if (move.isStunned) return;
        if (OC.holding)
        {
            ChangeAnimation(Animations.jumpNoHand);
        }
        else
        {
            ChangeAnimation(Animations.jumpHand);
        }
    }
    public void TopJumpAnimation()
    {
        if (OC.holding)
        {
            ChangeAnimation(Animations.fallingNoHand);
        }
        else
        {
            ChangeAnimation(Animations.fallingHand);
        }
    }
    public void LandingAnimation()
    {
        if (OC.holding)
        {
            ChangeAnimation(Animations.landingNoHand);
        }
        else
        {
            ChangeAnimation(Animations.landingHand);
        }
    }
}
public enum Animations
{
    idleHand,
    idleNoHand,
    walkHand,
    walkNoHand,
    jumpHand,
    jumpNoHand,
    fallingHand,
    fallingNoHand,
    landingHand,
    landingNoHand,
    down
}
