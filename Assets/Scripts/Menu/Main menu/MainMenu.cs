using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseScren
{
    public GameObject control, credits, quit;
    public void Play()
    {
        GameData.sceneManager.LoadScene("testing","MainMenu");
    }
    public void Quit()
    {
        quit.transform.position = new Vector3(quit.transform.position.x, quit.transform.position.y-3, quit.transform.position.z);
        Application.Quit();
    }
    public void Options()
    {
        control.transform.position = new Vector3(control.transform.position.x, control.transform.position.y - 3, control.transform.position.z);
        GameData.menuManager.Hide<MainMenu>();
        GameData.menuManager.Show<Controls>();
    }
    public void Credits()
    {
        credits.transform.position = new Vector3(credits.transform.position.x, credits.transform.position.y - 3, credits.transform.position.z);
        GameData.menuManager.Hide<MainMenu>();
        GameData.menuManager.Show<Credits>();
    }
    public override void Show()
    {
        base.Show();
    }
}
