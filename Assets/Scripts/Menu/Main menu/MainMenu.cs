using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseScren
{
    public void Play()
    {
        GameData.sceneManager.LoadScene("testing","MainMenu");
    }
    public void Settings()
    {
        GameData.menuManager.Hide<MainMenu>();
        GameData.menuManager.Show<Settings>();
    }
}
