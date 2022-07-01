using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public InputHandler[] playerHandlers;
    //active players , number of players spawned in map
    public int activePlayers { get; internal set; }
    public Material[] playerMats;
    public GameControl gameMan;
    public float winPause;
    //player stas list
    public List<PlayerStats> playerStats;
    //used for adding to stats list only once

    public IngameUI ingameUI;
    //mirov script

    bool start = false;

    public PauseMenu pauseMenu;
    private void Awake()
    {
        playerStats = new List<PlayerStats>();
    }
    public void CheckPlayers()
    {
        playerHandlers = GetComponentsInChildren<InputHandler>();
    }
    public void SpawnPlayers(List<Transform> positions,GameObject altar)
    {
        activePlayers = 0;
        for (int i = 0; i < playerHandlers.Length; i++)
        {
            activePlayers += 1;
            Transform pos = positions[i];
            playerHandlers[i].SpawnPlayer(pos, playerMats[i], altar);
            playerHandlers[i].SetId(activePlayers);
            if (!start)
            {
                playerStats.Add(new PlayerStats(activePlayers));
            }
        }
        foreach (PlayerStats stat in playerStats)
        {
            stat.Spawn();
            Debug.Log(stat.id + " " + stat.kils + " " + stat.wins + " " + stat.alive);
        }
        start = true;
    }
    // geting numbers of players, all players even death
    public int GetNumberOfPlayers()
    {
        return playerHandlers.Length;
    }
    //switching controls schemes
    public void SwitchControlToGame()
    {
        foreach(InputHandler handler in playerHandlers)
        {
            handler.SwitchControlsToGame();
        }
    }
    public void SwitchControlToMenu()
    {
        foreach (InputHandler handler in playerHandlers)
        {
            handler.SwitchControlsToMenu();
        }
    }
    //killing , despawning players
    public void PlayerDeath(int id,int killID)
    {
        activePlayers -= 1;
        KilledPlayer(id,killID);
        gameMan.map.altarMan.Sacrifice();
        if (activePlayers <= 1)
        {
            print("caling");
            ingameUI.gameObject.SetActive(true);
            ingameUI.EndGame();
            StartCoroutine(WinTimer());
        }
    }
    public void Despawn()
    {
        foreach(InputHandler x in playerHandlers)
        {
            x.PlayerDespawn();
        }
    }
    //win pause
    IEnumerator WinTimer()
    {
        yield return new WaitForSeconds(winPause);
        Despawn();
        gameMan.ChangeMap();
    }
    //stat manipulation
    public void WinGame(int id)
    {
        foreach(PlayerStats stat in playerStats)
        {
            if(stat.id == id)
            {
                stat.Won();
            }
            //Debug.Log(stat.id + " "+ stat.kils +" "+ stat.wins);
        }
    }
    public void KilledPlayer(int killerId,int deathID)
    {
        foreach (PlayerStats stat in playerStats)
        {
            if (stat.id == killerId)
            {
                stat.GetKill();
            }
            if(stat.id == deathID)
            {
                stat.Killed();
            }
            Debug.Log(stat.id + " " + stat.kils + " " + stat.wins+" "+stat.alive);
        }

        //ingameUI.Cross(deathID);
    }
    public bool PauseGame()
    {
        return pauseMenu.Pause();
    }
    public void UnPause()
    {
        foreach(InputHandler han in playerHandlers)
        {
            han.stoped = false;
        }
    }
}
