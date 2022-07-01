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
    public GameObject selection;
    public TextMeshProUGUI characterName;
    public Transform rightArrow;
    public Transform leftArrow;
    public float pressTime;
    private void Start()
    {
        active = false;
        ready = false;
    }

    public void Activate()
    {
        if (GameData.sceneManager.loading) return;
        if (!active)
        {
            active = true;
            readyText.SetText("Press space / A to ready");
            text.enabled = false;
            selection.SetActive(true);
            image.sprite = types[0].picture;
            characterName.text = types[0].name;
        }
    }
    public void ChangeRight()
    {
        if (active && !ready)
        {
            StartCoroutine(Right());
            if(index < types.Length - 1)
            {
                index += 1;
                image.sprite = types[index].picture;
                characterName.text = types[index].name;

            }
            else if(index + 1 > types.Length - 1)
            {
                index = 0;
                image.sprite = types[index].picture;
                characterName.text = types[index].name;
            }
        }
    }
    public void ChangeLeft()
    {
        if (active && !ready)
        {
            StartCoroutine(Left());
            if (index -1 < 0)
            {
                index = types.Length-1;
                image.sprite = types[index].picture;
                characterName.text = types[index].name;
            }
            else
            {
                index -= 1;
                image.sprite = types[index].picture;
                characterName.text = types[index].name;
            }
        }
    }
    public PlayerControl Ready()
    {
        if (ready)
        {
            readyText.SetText("Press space / A to ready");
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
    IEnumerator Right()
    {
        rightArrow.localScale = new Vector3(0.5f, 0.5f, 1);
        yield return new WaitForSeconds(pressTime);
        rightArrow.localScale = new Vector3(1f, 1f, 1);
    }
    IEnumerator Left()
    {
        leftArrow.localScale = new Vector3(0.5f, 0.5f, 1);
        yield return new WaitForSeconds(pressTime);
        leftArrow.localScale = new Vector3(1f, 1f, 1);
    }

}
