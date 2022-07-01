using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    BaseScren[] screens;
    private void Awake()
    {
        GameData.menuManager = this;
        screens = GetComponentsInChildren<BaseScren>(true);
    }
    public void Show<A>()
    {
        print(screens.Length);
        foreach (BaseScren screen in screens)
        {
            if (screen.GetType() == typeof(A))
            {
                screen.Show();
            }
        }
    }
    public void Hide<A>()
    {
        foreach (BaseScren screen in screens)
        {
            if (screen.GetType() == typeof(A))
            {
                screen.Hide();
            }
        }
    }
}
