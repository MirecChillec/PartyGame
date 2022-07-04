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
    public GameObject WinScreen;
    public bool won = false;

    public PauseMenu pauseMenu;
    private void Awake()
    {
        playerStats = new List<PlayerStats>();
    }
    public void CheckPlayers()
    {
        playerHandlers = GetComponentsInChildren<InputHandler>();
    }
    public void SpawnPlayers(List<Transform> positions, GameObject altar)
    {
        ingameUI.KillIconReset();
        activePlayers = 0;
        for (int i = 0; i < playerHandlers.Length; i++)
        {
            activePlayers += 1;
            Transform pos = positions[i];
            playerHandlers[i].SpawnPlayer(pos, playerMats[i], altar);
            playerHandlers[i].SetId(activePlayers);
            if (!start)
            {
                playerStats.Add(new PlayerStats(activePlayers, playerHandlers[i].GetT(), playerHandlers[i].selection.GetCurrentType().DownPose, playerHandlers[i].selection.GetCurrentType().WinPose));
            }
        }
        foreach (PlayerStats stat in playerStats)
        {
            stat.Spawn();
        }
        start = true;
        ingameUI.gameObject.SetActive(true);
        ingameUI.UpdateUI();
    }
    // geting numbers of players, all players even death
    public int GetNumberOfPlayers()
    {
        return playerHandlers.Length;
    }
    //switching controls schemes
    public void SwitchControlToGame()
    {
        foreach (InputHandler handler in playerHandlers)
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
    public void PlayerDeath(int id, int killID)
    {
        activePlayers -= 1;
        KilledPlayer(id, killID);
        gameMan.map.altarMan.Sacrifice();
        ingameUI.UpdateUI();
        if (activePlayers <= 1)
        {
            foreach (PlayerStats stat in playerStats)
            {
                if (stat.id == id)
                {
                    stat.Won();
                }
            }

            ingameUI.gameObject.SetActive(true);
            if (CheckWin())
            {
                StartCoroutine(Win());
                return;
            }
            StartCoroutine(WinTimer());
        }
    }
    public void Despawn()
    {
        foreach (InputHandler x in playerHandlers)
        {
            x.PlayerDespawn();
        }
    }
    //win pause
    IEnumerator WinTimer()
    {
        yield return new WaitForSeconds(1.5f);
        ingameUI.EndGame();
        yield return new WaitForSeconds(winPause);
        Despawn();
        gameMan.ChangeMap();
    }
    //stat manipulation
    public void KilledPlayer(int killerId, int deathID)
    {
        foreach (PlayerStats stat in playerStats)
        {
            if (stat.id == killerId)
            {
                stat.GetKill();
            }
            if (stat.id == deathID)
            {
                stat.Killed();
            }
        }
        ingameUI.ActivateKillIcon(deathID);
        ingameUI.UpdateUI();
    }
    public bool PauseGame()
    {
        return pauseMenu.Pause();
    }
    public void UnPause()
    {
        foreach (InputHandler han in playerHandlers)
        {
            han.stoped = false;
        }
    }
    bool CheckWin()
    {
        foreach (PlayerStats stat in playerStats)
        {
            if (stat.wins >= 3)
            {
                return true;
            }
        }
        return false;
    }
    public IEnumerator Win()
    {
        yield return new WaitForSeconds(1.5f);
        ingameUI.EndGame();
        yield return new WaitForSeconds(2f);
        ingameUI.gameObject.SetActive(false);
        won = true;
        WinScreen.SetActive(true);
        WinScreen.gameObject.GetComponent<WonScreen>().ShowResults(playerStats);
    }
}
