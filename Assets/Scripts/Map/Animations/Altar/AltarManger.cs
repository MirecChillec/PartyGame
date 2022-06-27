using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarManger : MonoBehaviour
{
    public BubleManager buleMan;
    public SacrificeAnimation sacrifice;
    bool sacrificing = false;

    private void Update()
    {
        if (!sacrificing)
        {
            buleMan.playing = true;
        }
    }
    public void Sacrifice()
    {
        sacrificing = true;
        buleMan.playing = false;
        sacrifice.PlaySacrifice();
    }
}
