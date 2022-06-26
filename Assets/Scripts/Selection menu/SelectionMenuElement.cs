using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectionMenuElement : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI readyText;

    SelectionManager selectionMan;
    PlayerTypes[] types;
    public bool ready { get; private set; }
    int index = 0;
    public bool active { get; private set; }
    public Image image;
    private void Start()
    {
        active = false;
        ready = false;
    }

    public void Activate()
    {
        if (!active)
        {
            active = true;
            readyText.SetText("Unready");
            text.enabled = false;
            image.enabled = true;
            image.sprite = types[0].picture;
        }
    }
    public void ChangeRight()
    {
        if (active && !ready)
        {
            if(index < types.Length - 1)
            {
                index += 1;
                image.sprite = types[index].picture;
            }
            else if(index + 1 > types.Length - 1)
            {
                index = 0;
                image.sprite = types[index].picture;
            }
        }
    }
    public void ChangeLeft()
    {
        if (active && !ready)
        {
            if (index -1 < 0)
            {
                index = types.Length-1;
                image.sprite = types[index].picture;
            }
            else
            {
                index -= 1;
                image.sprite = types[index].picture;
            }
        }
    }
    public PlayerControl Ready()
    {
        if (ready)
        {
            readyText.SetText("Unready");
            ready = false;
        }
        else
        {
            readyText.SetText("Ready");
            ready = true;
        }
        selectionMan.CheckPlayers();
        return types[index].playerPrefab;
    }
    public void Init(SelectionManager selMan)
    {
        selectionMan = selMan;
        types = selectionMan.playerTypes;
    }
}
