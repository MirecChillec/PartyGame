using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputHandler[] playerHandlers;
    //active players , number of players spawned in map
    public int activePlayers { get; internal set; }
    public Material[] playerMats;
    public GameControl gameMan;
    public float winPause;

    public void CheckPlayers()
    {
        playerHandlers = GetComponentsInChildren<InputHandler>();
    }
    public void SpawnPlayers(List<Transform> positions,GameObject altar)
    {
        activePlayers = 0;
        for(int i = 0; i< playerHandlers.Length; i++)
        {
            activePlayers += 1;
            Transform pos = positions[i];
            playerHandlers[i].SpawnPlayer(pos,playerMats[i],altar); 
        }
    }
    public int GetNumberOfPlayers()
    {
        return playerHandlers.Length;
    }
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
    public void PlayerDeath()
    {
        activePlayers -= 1;
        if (activePlayers <= 1)
        {
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
    IEnumerator WinTimer()
    {
        yield return new WaitForSeconds(winPause);
        Despawn();
        gameMan.ChangeMap();
    }
}
