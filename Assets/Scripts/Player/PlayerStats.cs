using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public int id { get;private set; }
    public int kils { get; private set; }
    public int wins { get; private set; }
    public bool alive { get; private set; }
    public player typ;
    public PlayerStats(int pId, player type)
    {
        this.id = pId;
        this.kils = 0;
        this.wins = 0;
        this.typ = type;
    }
    public void Won()
    {
        wins += 1;
    }
    public void GetKill()
    {
        kils += 1;
    }
    public void Spawn()
    {
        alive = true;
    }
    public void Killed()
    {
        alive = false;
    }
}
public enum player { 
    butcher,
    detective,
    ocultist,
    nobleman
}
