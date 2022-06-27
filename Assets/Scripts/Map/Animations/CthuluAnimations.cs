using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthuluAnimations : MonoBehaviour
{
    public Animator anim;
    [Range(3,10)]public float min;
    [Range(1,2)]public float max;
    void Start()
    {
        StartCoroutine(BlinkTimer());
    }
    IEnumerator BlinkTimer()
    {
        while (true)
        {
            float wait = Random.Range(min, max);
            yield return new WaitForSeconds(wait);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Watching"))
            {
                Blink();
            }
        }
    }
    void Blink()
    {
        anim.Play("Blink");
    }
}
