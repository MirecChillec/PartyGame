using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WonScreen : MonoBehaviour
{
    public Image[] losers;
    public Image winner;
    public Sprite[] sprites;
    public Sprite[] winsprites;
    public TextMeshProUGUI text;
    public void ShowResults(List<PlayerStats> statrs)
    {
        PlayerStats win = Biggest(statrs);
        print(win);
        WinnerSprite(win);
        text.text = "Player" + win.id + " won";
        statrs.Remove(win);
        for(int i = 0;i < statrs.Count; i++)
        {
            losers[i].sprite = GetSprite(statrs[i]);
            losers[i].gameObject.SetActive(true);
        }
        print("end");

    }
    PlayerStats Biggest(List<PlayerStats> stats)
    {
        int wins = 0;
        PlayerStats x = null;
        for (int i =0; i< stats.Count;i++)
        {
            if(stats[i].wins > wins)
            {
                x = stats[i];
            }
        }
        return x;
    }
    public void WinnerSprite(PlayerStats stat)
    {
        print(stat.typ);
        switch (stat.typ)
        {
            case player.butcher:
                winner.sprite = winsprites[0];
                break;
            case player.detective:
                winner.sprite = winsprites[1];
                break;
            case player.nobleman:
                winner.sprite = winsprites[2];
                break;
            case player.ocultist:
                winner.sprite = winsprites[3];
                break;
        }
    }
    public Sprite GetSprite(PlayerStats stat)
    {
        switch (stat.typ)
        {
            case player.butcher:
                return  sprites[0];
                break;
            case player.detective:
                return sprites[1];
                break;
            case player.nobleman:
                return sprites[2];
                break;
            case player.ocultist:
                return sprites[3];
                break;
        }
        return null;
    }
    public void Back()
    {
        GameData.sceneManager.LoadScene("MainMenu", "testing");
    }
}
