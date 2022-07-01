using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseScren
{
    public void Play()
    {
        GameData.sceneManager.LoadScene("testing","MainMenu");
    }
    public void Quit()
    {
        print("quit");
        Application.Quit();
    }
    public void Options()
    {
        GameData.menuManager.Hide<MainMenu>();
        GameData.menuManager.Show<Controls>();
    }
    public void Credits()
    {
        GameData.menuManager.Hide<MainMenu>();
        GameData.menuManager.Show<Credits>();
    }
}
