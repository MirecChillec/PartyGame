using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnPosition : MonoBehaviour
{
    [Header("Spawning positions")]
    public List<Transform> positions;
    public List<Transform> freePositions { get; private set; }
    public GameObject altar;
    public AltarManger altarMan;
    int index;
    private void Awake()
    {
        index = 0;
        if(positions.Count == 0)
        {
            //error if no positions are added
            Debug.LogError("Missing spawning positions");
            return;
        }
        freePositions = positions;
    }
    //asigning random position
    public Transform GetRandomPlayerPosition()
    {
        //checking for free positions
        if(freePositions.Count != 0)
        {
            int x = Random.Range(0, freePositions.Count);
            Transform trans = freePositions[x];
            freePositions.RemoveAt(x);
            return trans;
        }
        return null;
    }
}
