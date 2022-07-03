using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controler : BaseButton
{
    public GameObject keyboard;
    public GameObject controler;
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        keyboard.SetActive(false);
        controler.SetActive(true);
    }

}
