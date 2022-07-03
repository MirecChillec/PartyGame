using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsButton : BaseButton
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        GameData.menuManager.Hide<MainMenu>();
        GameData.menuManager.Show<Credits>();
    }
}
