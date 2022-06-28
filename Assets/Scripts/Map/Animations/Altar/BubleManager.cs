using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubleManager : MonoBehaviour
{
    Buble[] bubles;
    public bool playing { get; set; }
    void Start()
    {
        bubles = GetComponentsInChildren<Buble>(true);
        playing = true;
        StartCoroutine(PlayingBubles());
    }
    IEnumerator PlayingBubles()
    {
        while (true)
        {
            if (playing)
            {
                if(CheckPlaying() <= 0)
                {
                    yield return new WaitForSeconds(0.15f);
                    bubles[Random.Range(0, bubles.Length)].PlayAnimation();
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    int CheckPlaying()
    {
        int x = 0;
        foreach(Buble bub in bubles)
        {
            if (bub.playing)
            {
                x += 1;
            }
        }
        return x;
    }
}
