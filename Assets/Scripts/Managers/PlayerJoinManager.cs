using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{
    public SelectionManager selectionMenu;
    public PlayerManager playerManager;
    public void PlayerJoin(PlayerInput input)
    {
        GameObject player = input.gameObject;
        player.transform.SetParent(this.transform);
        InputHandler handler = player.GetComponent<InputHandler>();
        SelectionMenuElement element = selectionMenu.AddPlayer();
        handler.Init(element);
        playerManager.CheckPlayers();
    }
}
