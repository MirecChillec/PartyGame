using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnPosition : MonoBehaviour
{
    [Header("Spawning positions")]
    public List<Transform> positions;
    public List<Transform> freePositions { get; private set; }
    //public Material[] playerMaterials;
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
        print(positions.Count + " positions");
        freePositions = positions;
        print(freePositions.Count + " free");
    }
    //asigning random position
    public Transform GetRandomPlayerPosition()
    {
        print(freePositions.Count + " free");
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
