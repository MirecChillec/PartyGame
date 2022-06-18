using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public PlayerSpawnPosition[] maps;
    public PlayerManager players;
    PlayerSpawnPosition map;
    void Start()
    {
        players.SwitchControlToGame();
        ChangeMap();
    }
    public void ChangeMap()
    {
        map = Instantiate(maps[0]);
        map.transform.position = new Vector3(0, 0, 0);
        map.gameObject.transform.SetParent(this.transform);
        print(map.freePositions.Count + " control free");
        int max = players.GetNumberOfPlayers();
        List<Transform> positions = new List<Transform>();
        for (int i = 0; i < max; i++)
        {
            positions.Add(map.GetRandomPlayerPosition());
        }
        print(positions.Count);
        players.SpawnPlayers(positions);
    }
}
