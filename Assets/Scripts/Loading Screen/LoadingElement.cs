using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingElement : MonoBehaviour
{
    public LoadingTip parent { get; set; }
    public float stayTime;
    private void Start()
    {
        StartCoroutine(Stay());
    }
    IEnumerator Stay()
    {
        yield return new WaitForSeconds(stayTime);
        parent.ChangeElement();
        Destroy(this.gameObject);
    }
}
