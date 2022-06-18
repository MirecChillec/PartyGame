using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject selection;
    public GameObject game;

    public void StartGame()
    {
        selection.SetActive(false);
        game.SetActive(true);
    }
}
