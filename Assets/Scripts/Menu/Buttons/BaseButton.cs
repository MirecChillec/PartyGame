using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class BaseButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    public TextMeshProUGUI text;
    float move = 6;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y - move, text.transform.position.z);
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y + move, text.transform.position.z);
    }
}
