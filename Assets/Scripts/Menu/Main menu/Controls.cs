using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : BaseScren
{
    public GameObject keyboard;
    public GameObject controler;
    public void Back()
    {
        Keyboard();
        GameData.menuManager.Hide<Controls>();
        GameData.menuManager.Show<MainMenu>();
    }
    public void Keyboard()
    {
        keyboard.SetActive(true);
        controler.SetActive(false);
    }
    public void Controler()
    {
        keyboard.SetActive(false);
        controler.SetActive(true);
    }
}
