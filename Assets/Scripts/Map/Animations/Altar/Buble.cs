using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buble : MonoBehaviour
{
    public Animator anim;
    public bool playing { get; private set; }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void PlayAnimation()
    {
        playing = true;
        anim.Play("buble");
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        while (playing)
        {
            yield return new WaitForSeconds(0.1f);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
            {
                playing = false;
            }
        }
    }
}
