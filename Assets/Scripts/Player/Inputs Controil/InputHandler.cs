using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInput inputMap;
    SelectionMenuElement selection;
    PlayerControl playerPrefab;
    public PlayerControl InGamePlayer;
    PlayerManager playerMan;
    bool alive;
    public bool stoped = false;
    public int playerId { get; internal set; } 
    private void Awake()
    {
        inputMap = GetComponent<PlayerInput>();
        playerMan = transform.parent.GetComponent<PlayerManager>();
    }

    public void Init(SelectionMenuElement selControl)
    {
        selection = selControl;
        selection.Activate();
    }
    public void SetId(int id)
    {
        this.playerId = id;
    }
    //Selecting UI Controls
    public void SelectingChange(InputAction.CallbackContext ctx)
    {
        if (stoped) return;
        if (ctx.started)
        {
            if (!selection.active)
            {
                selection.Activate();
                return;
            }
            if (ctx.ReadValue<float>() > 0)
            {
                selection.ChangeRight();
            }
            else
            {
                selection.ChangeLeft();
            }
        }
    }
    public void CorfirmSlecrtion(InputAction.CallbackContext ctx)
    {
        if (stoped) return;
        if (ctx.started)
        {
            playerPrefab = selection.Ready();
        }
    }

    //Spawning players
    public void SpawnPlayer(Transform position,Material mat,GameObject altar)
    {
        alive = true;
        InGamePlayer = Instantiate(playerPrefab);
        InGamePlayer.stun.altar = altar;
        InGamePlayer.stun.inputParent = this.transform;
        InGamePlayer.transform.SetParent(this.transform);
        InGamePlayer.transform.position = position.position;
        InGamePlayer.gameObject.GetComponent<SpriteRenderer>().material = mat;
    }
    //Despawning players
    public void DestroyPlayer(int id)
    {
        if (InGamePlayer == null) return;
        Destroy(InGamePlayer.gameObject);
        alive = false;
        playerMan.PlayerDeath(id,this.playerId);
    }
    public void PlayerDespawn()
    {
        if (alive)
        {
            Destroy(InGamePlayer.gameObject);
            playerMan.WinGame(this.playerId);
        }
    }
    //switching control maps
    public void SwitchControlsToGame()
    {
        inputMap.SwitchCurrentActionMap("Game");
    }
    public void SwitchControlsToMenu()
    {
        inputMap.SwitchCurrentActionMap("UI");
    }


    //Game controls
    public void Move(InputAction.CallbackContext ctx)
    {
        if (stoped) return;

        if (CheckInGamePlayer() && ctx.performed)
        {
            InGamePlayer.move.OnMove(ctx.ReadValue<float>());
        }
        else if (CheckInGamePlayer() && ctx.canceled)
        {
            InGamePlayer.move.Stop();
        }
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (stoped) return;

        if (CheckInGamePlayer() && ctx.started)
        {
            InGamePlayer.move.OnJump();
        }
    }
    public void Drop(InputAction.CallbackContext ctx)
    {
        if (stoped) return;
        if (CheckInGamePlayer() && ctx.performed)
        {
            InGamePlayer.OC.OnDown();
        }else if(CheckInGamePlayer() && ctx.canceled)
        {
            InGamePlayer.OC.CancelDown();
        }
    }
    public void Action(InputAction.CallbackContext ctx)
    {
        if (stoped) return;
        if (CheckInGamePlayer() && ctx.started)
        {
            InGamePlayer.OC.OnAction();
        }
    }
    //checking if p[layer is alive
    bool CheckInGamePlayer()
    {
        if (InGamePlayer == null) return false;
        return true;
    }
    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
           stoped = playerMan.PauseGame();
        }
    }
    public player GetT()
    {
        switch (selection.selection.name) {
            case "Butcher":
                return player.butcher;
            case "Detective":
                return player.detective;
            case "Nobleman":
                return player.nobleman;
            case "Ocultist":
                return player.ocultist;
        }
        return player.butcher;
    }
}
