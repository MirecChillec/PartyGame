using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public PlayerManager playerManager;
    public List<RectTransform> players;
    public List<GameObject> kills;
    public List<GameObject> wins;
    public List<GameObject> lines;
    public int numberOfPlayers;
    public GameObject endGame;
    public GameObject endGameText;

    void Awake()
    {
        players[0].gameObject.SetActive(false);
        players[1].gameObject.SetActive(false);
        players[2].gameObject.SetActive(false);
        players[3].gameObject.SetActive(false);
        numberOfPlayers = playerManager.playerHandlers.Length;

        switch (numberOfPlayers)
        {
            case 2:
                players[0].gameObject.SetActive(true);
                players[1].gameObject.SetActive(true);
                //players[0].anchoredPosition = new Vector2(480, -75);
                //players[1].anchoredPosition = new Vector2(1440, -75);
                Debug.Log("case 2");
                break;
            case 3:
                players[0].gameObject.SetActive(true);
                players[1].gameObject.SetActive(true);
                players[2].gameObject.SetActive(true);
                //players[0].anchoredPosition = new Vector2(354, -75);
                //players[1].anchoredPosition = new Vector2(994, -75);
                //players[2].anchoredPosition = new Vector2(1634, -75);
                break;                      
            case 4:
                players[0].gameObject.SetActive(true);
                players[1].gameObject.SetActive(true);  
                players[2].gameObject.SetActive(true);
                players[3].gameObject.SetActive(true);
               // players[0].anchoredPosition = new Vector2(480, -75);
                //players[1].anchoredPosition = new Vector2(1440, -75);
                //players[2].anchoredPosition = new Vector2(480, -75);
                //players[3].anchoredPosition = new Vector2(1440, -75);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            wins[i].GetComponent<TextMeshProUGUI>().text = playerManager.playerStats[i].wins.ToString();
            kills[i].GetComponent<TextMeshProUGUI>().text = playerManager.playerStats[i].kils.ToString();
        }
    }

    public void Cross(int id)
    {

    }

    public void EndGame()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (playerManager.playerStats[i].alive)
            {

            }
        }
    }
}
