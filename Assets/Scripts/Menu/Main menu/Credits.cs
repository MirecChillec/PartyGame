using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : BaseScren
{
    public void Back()
    {
        GameData.menuManager.Hide<Credits>();
        GameData.menuManager.Show<MainMenu>();
    }
}
