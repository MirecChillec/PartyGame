using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject selection;
    public GameObject game;
    private void Start()
    {
        StartCoroutine(Waiting());
    }
    IEnumerator Waiting()
    {
        while (GameData.sceneManager.loading)
        {
            yield return new WaitForSeconds(0.2f);
        }
        selection.SetActive(true);
    }
    public void StartGame()
    {
        selection.SetActive(false);
        game.SetActive(true);
    }
}
