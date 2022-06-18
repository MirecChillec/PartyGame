using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionMenuElement : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI readyText;

    SelectionManager selectionMan;
    PlayerTypes[] types;
    public bool ready { get; private set; }
    int index = 0;
    public bool active { get; private set; }
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
            text.SetText(types[index].name);
        }
    }
    public void ChangeRight()
    {
        if (active && !ready)
        {
            if(index < types.Length - 1)
            {
                index += 1;
                text.SetText(types[index].name);
            }else if(index + 1 > types.Length - 1)
            {
                index = 0;
                text.SetText(types[index].name);
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
                text.SetText(types[index].name);
            }
            else
            {
                index -= 1;
                text.SetText(types[index].name);
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
