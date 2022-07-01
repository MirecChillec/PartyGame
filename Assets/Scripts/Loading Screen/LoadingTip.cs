using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingTip : MonoBehaviour
{
    public LoadingElement[] elements;
    int index;
    void Start()
    {
        index = Random.Range(0, elements.Length);
        ChangeElement();
    }
    public void ChangeElement()
    {
        LoadingElement element = Instantiate(elements[index]);
        element.parent = this;
        element.transform.SetParent(this.transform);
        element.transform.position = transform.position;
        index++;
        if(index >= elements.Length)
        {
            index = 0;
        }
    }
}
