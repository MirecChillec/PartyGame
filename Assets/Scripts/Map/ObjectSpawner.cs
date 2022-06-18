using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Objects variables")]
    //objects spawned at start
    public int startingObjects;
    //total objectys in game spawned
    public int numOfObjects;
    //objects
    public ThrowableObject[] objekt;
    [Header("Spawning variables")]
    public float timeInterval;
    private float _timeInterval;
    public int maxObjects;

    //positions for spawning
    ObjectSpawnPosition[] positions;

    [Header("Reference objects")]
    public GameObject parentOfPositions;
    public ScreenBounds bounds;

    bool canSpawn = false;

    private void Start()
    {
        bounds = GameData.scrennBounds;
        _timeInterval = timeInterval;
        positions = parentOfPositions.GetComponentsInChildren<ObjectSpawnPosition>(true);
        foreach (ObjectSpawnPosition x in positions)
        {
            x.Init(bounds, this);
        }
        for (int i = 0; i < startingObjects; i++)
        {
            int x = Random.Range(0, positions.Length);
            if (!SpawnObject(x))
            {
                i -= 1;
            }
        }
        StartCoroutine(Spawning());
    }
    bool SpawnObject(int positionIndex)
    {
        if (!positions[positionIndex].free)
        {
            return false;
        }
        numOfObjects += 1;
        positions[positionIndex].Spawn(objekt[0]);
        return true;
    }
    public void DestroiedObject()
    {
        numOfObjects -= 1;
        canSpawn = true;
    }
    IEnumerator Spawning()
    {
        while (true) { 
            if(numOfObjects < maxObjects)
            {
                yield return new WaitForSeconds(timeInterval);
                int x = Random.Range(0, positions.Length);
                if (!SpawnObject(x))
                {
                    timeInterval = 0f;
                }
                else
                {
                    timeInterval = _timeInterval;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}