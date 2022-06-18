using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputHandler[] playerHandlers;
    public Material[] playerMats;
    public void CheckPlayers()
    {
        playerHandlers = GetComponentsInChildren<InputHandler>();
    }
    public void SpawnPlayers(List<Transform> positions)
    {
        for(int i = 0; i< playerHandlers.Length; i++)
        {
            Transform pos = positions[i];
            playerHandlers[i].SpawnPlayer(pos,playerMats[i]); 
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

}
