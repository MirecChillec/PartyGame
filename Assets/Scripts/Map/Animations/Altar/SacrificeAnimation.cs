using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeAnimation : MonoBehaviour
{
    Animator anim;
    public AudioSource audio;
    public AltarManger altar;
    int kills = 0;
    public bool playing { get; private set; }
    void Start()
    {
        kills = 0;
        playing = false;
        anim = GetComponent<Animator>();
    }
    public void PlaySacrifice()
    {
        audio.Play();
        anim.Play("sacrifice");

        StartCoroutine(AnimationControl());
    }
    IEnumerator AnimationControl()
    {
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
        }
        altar.sacrificing = false;
        altar.buleMan.playing = true;
    }
}
