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
    public TextMeshProUGUI score;
    public void ShowResults(List<PlayerStats> stats)
    {
        string scoretext = "Score\n";
        foreach(PlayerStats x in stats)
        {
            scoretext += "P" + x.id + "\n    wins " + x.wins + " kills " + x.kils + "\n";
        }
        score.text = scoretext;
        PlayerStats win = Biggest(stats);
        winner.sprite = win.winpose;
        text.text = "Player " + win.id + " won";
        stats.Remove(win);
        for(int i = 0;i < stats.Count; i++)
        {
            losers[i].sprite = stats[i].downpose;
            losers[i].gameObject.SetActive(true);
        }

    }
    PlayerStats Biggest(List<PlayerStats> stats)
    {
        int wins = 0;
        PlayerStats x = null;
        for (int i =0; i< stats.Count;i++)
        {
            Debug.Log("P" + stats[i].id + "\n wins " + stats[i].wins + " kills " + stats[i].kils + "\n");
            if (stats[i].wins > wins)
            {
                wins = stats[i].wins;
                x = stats[i];
            }
        }
        Debug.Log(x.id);
        return x;
    }
    public void Back()
    {
        GameData.sceneManager.LoadScene("MainMenu", "testing");
    }
}
