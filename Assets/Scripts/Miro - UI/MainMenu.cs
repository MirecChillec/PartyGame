using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    public GameObject main;
    public GameObject controls;

    public void Play()
    {
        SceneManager.LoadScene(sceneName: "testing");
    }

    public void Options()
    {
        options.SetActive(true);
        main.SetActive(false);
    }

    public void Controls()
    {
        controls.SetActive(true);
        main.SetActive(false);
    }

    public void Back()
    {
        options.SetActive(false);
        controls.SetActive(false);
        main.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
