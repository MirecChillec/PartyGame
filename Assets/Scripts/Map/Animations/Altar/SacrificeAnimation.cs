using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeAnimation : MonoBehaviour
{
    Animator anim;
    int kills = 0;
    public bool playing { get; private set; }
    void Start()
    {
        playing = false;
        anim = GetComponent<Animator>();
    }
    public void PlaySacrifice()
    {
        kills += 1;
        if (playing) return;
        StartCoroutine(AnimationControl());
    }
    IEnumerator AnimationControl()
    {
        playing = true;
        anim.Play("sacrifice");
        while (kills > 0)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            {
                kills -= 1;
                if(kills > 0)
                {
                    anim.Play("sacrifice");
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
        playing = false;
    }
}
