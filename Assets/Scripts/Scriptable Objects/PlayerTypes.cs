using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerObject")]
public class PlayerTypes : ScriptableObject
{
    public string name;
    public PlayerControl playerPrefab;
}
