using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public ObjectControl controler;
    private ThrowableObject objekt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Throwable" && controler.state == Throwable.idle)
        {
            controler.canPickUp = true;
            objekt = collision.GetComponent<ThrowableObject>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Throwable" && controler.state == Throwable.idle)
        {
            controler.canPickUp = false;
            objekt = null;
        }
    }
    public void Pick()
    {
        if (objekt != null)
        {
            objekt.PickUp(this.transform);
        }
    }
    public void Throw(bool direction)
    {
        if (objekt != null)
        {
            objekt.Throw(direction);
        }
    }
    public void ThrowDown()
    {
        if (objekt != null)
        {
            objekt.ThrowDown();
        }
    IEnumerator PickTimer()
    {
        //pauza po hode mozne vyuzit na nejaky debaf
        //bez corontine bolo mozne chytit hodeny objekt pocas toho ako leti
        canPick = false;
        yield return new WaitForSeconds(pickTimer);
        canPick = true;
    }
}
