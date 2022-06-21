using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public ObjectControl controler;
    //objekt na hadzanie
    private ThrowableObject objekt;
    //player colider na urcenie priestoru na detekciu
    public Collider2D playerColider;
    //array ziskan7ch objektov pomocou fyziky
    public Collider2D[] cols;
    //layermaska objektov
    public LayerMask mask;
    //cas na reset na zobranie dalsieho objektu
    public float pickTimer;
    [SerializeField] private bool canPick;
    private void Start()
    {
        canPick = true;
        objekt = null;
    }
    private void FixedUpdate()
    {
        //ak moze zobrat objekt
        if (canPick)
        {
            //detekcia objectov
            cols = Physics2D.OverlapBoxAll(playerColider.bounds.center, playerColider.bounds.size, 0, mask);
            if (cols.Length != 0)
            {
                if (controler.state == Throwable.idle)
                {
                    //ziskanie thowable skriptu na volanie funkcie na hadzanie
                    objekt = cols[0].gameObject.GetComponent<ThrowableObject>();
                }
            }
        }
    }
    public void Pick()
    {
        if (canPick)
        {
            if (objekt != null)
            {
                objekt.PickUp(this.transform);
            }
        }
    }
    public void Throw(bool direction)
    {
        if (objekt != null)
        {
            canPick = false;
            objekt.Throw(direction);
            //nulovanie objekt premennej inak by hrac vedel chytit hodeny objekt pocas toho ako leti
            objekt = null;
            StartCoroutine(PickTimer());
        }
    }
    IEnumerator PickTimer()
    {
        //pauza po hode mozne vyuzit na nejaky debaf
        //bez corontine bolo mozne chytit hodeny objekt pocas toho ako leti
        canPick = false;
        yield return new WaitForSeconds(pickTimer);
        canPick = true;
    }
    public void ThrowDown()
    {
        if (objekt != null)
        {
            objekt.ThrowDown();
        }
    }

}
