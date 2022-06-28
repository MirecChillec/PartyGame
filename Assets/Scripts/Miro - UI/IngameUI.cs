using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    PlayerManager playerManager;
    public List<RectTransform> players;
    public int numberOfPlayers;

    void Start()
    {
        players[0].gameObject.SetActive(false);
        players[1].gameObject.SetActive(false);
        players[2].gameObject.SetActive(false);
        players[3].gameObject.SetActive(false);
        playerManager = GameObject.Find("Players").GetComponent<PlayerManager>();
        numberOfPlayers = playerManager.activePlayers;

        switch (numberOfPlayers)
        {
            case 2:
                players[0].gameObject.SetActive(true);
                players[1].gameObject.SetActive(true);
                players[0].anchoredPosition = new Vector2(480, -75);
                players[1].anchoredPosition = new Vector2(1440, -75);
                Debug.Log("case 2");
                break;
            case 3:
                players[0].gameObject.SetActive(true);
                players[1].gameObject.SetActive(true);
                players[2].gameObject.SetActive(true);
                players[0].anchoredPosition = new Vector2(354, -75);
                players[1].anchoredPosition = new Vector2(994, -75);
                players[2].anchoredPosition = new Vector2(1634, -75);
                break;                      
            case 4:
                players[0].gameObject.SetActive(true);
                players[1].gameObject.SetActive(true);  
                players[2].gameObject.SetActive(true);
                players[3].gameObject.SetActive(true);
                players[0].anchoredPosition = new Vector2(480, -75);
                players[1].anchoredPosition = new Vector2(1440, -75);
                players[2].anchoredPosition = new Vector2(480, -75);
                players[3].anchoredPosition = new Vector2(1440, -75);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        
    }
}
