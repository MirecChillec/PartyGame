using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public TextMeshProUGUI counDownText;
    public int CountDownTime;
    public SelectionMenuElement[] selctions;
    int index = -1;
    int players = 0;
    public PlayerTypes[] playerTypes;
    public GameManager gameManager;
    private void Awake()
    {
        foreach (SelectionMenuElement x in selctions)
        {
            x.Init(this);
        }
    }
    public SelectionMenuElement AddPlayer()
    {
        index++;
        if(index < 4)
        {
            players+=1;
            return selctions[index]; 
        }
        return null;
    }
    public void CheckPlayers()
    {
        if (players >= 2)
        {
            bool start = true;
            for (int i = 0; i < players; i++)
            {
                if (!selctions[i].ready)
                {
                    start = false;
                }
            }
            if (start)
            {
                StartCoroutine(CountDown());
            }
            else
            {
                StopAllCoroutines();
                counDownText.gameObject.SetActive(false);
            }
        }
        return;
    }
    IEnumerator CountDown()
    {
        counDownText.gameObject.SetActive(true);
        counDownText.SetText(CountDownTime.ToString());
        int x = CountDownTime;
        while (x > 0)
        {
            yield return new WaitForSeconds(1f);
            counDownText.enabled = false;
            yield return new WaitForSeconds(0.25f);
            x -= 1;
            counDownText.SetText(x.ToString());
            counDownText.enabled = true;
        }
        gameManager.StartGame();
    }
}
