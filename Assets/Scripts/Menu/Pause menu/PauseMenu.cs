using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public BaseScren screns;
    bool paused = false;
    public PlayerManager PM;
    public bool Pause()
    {
        if (PM.won) return false;
        bool stoped = false;
        if (paused)
        {
            screns.Hide();
            Time.timeScale = 1;
        }
        else
        {
            stoped = true;
            screns.Show();
            Time.timeScale = 0;
        }
        paused = !paused;
        return stoped;
    }
    public void Quit()
    {
        Time.timeScale = 1;
        GameData.sceneManager.LoadScene("MainMenu", "testing");
    }
    public void Continue()
    {
        Pause();
        PM.UnPause();
    }
}
