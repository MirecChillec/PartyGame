using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public int id { get;private set; }
    public int kils { get; private set; }
    public int wins { get; private set; }
    public PlayerStats(int pId)
    {
        this.id = pId;
        this.kils = 0;
        this.wins = 0;
    }
    public void Won()
    {
        wins += 1;
    }
    public void GetKill()
    {
        kils += 1;
    }
}
