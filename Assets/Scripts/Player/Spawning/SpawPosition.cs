using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawPosition : MonoBehaviour
{
    [Header("Spawning positions")]
    public List<Transform> positions;
    private List<Transform> freePositions;

    private void Awake()
    {
        if(positions.Count == 0)
        {
            //error if no positions are added
            Debug.LogError("Missing spawning positions");
            return;
        }
        freePositions = positions;
    }
    //asigning random position
    public void OnPlayerJoin(PlayerInput input)
    {
        //checking for free positions
        if(freePositions.Count != 0)
        {
            int x = Random.Range(0, freePositions.Count);
            GameObject player = input.gameObject;
            player.transform.position = freePositions[x].position;
            freePositions.RemoveAt(x);
        }
    }
}
