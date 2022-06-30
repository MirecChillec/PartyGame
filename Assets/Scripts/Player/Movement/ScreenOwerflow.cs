using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOwerflow : MonoBehaviour
{
    public ScreenBounds screenBounds;
    private void Start()
    {
        screenBounds = GameData.scrennBounds;
    }

    private void Update()
    {
        Vector3 tempPosition = transform.position;
        if (screenBounds.AmIOutOfBounds(tempPosition))
        {
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(tempPosition);
            transform.position = newPosition;
        }
        else
        {
            transform.position = tempPosition;
        }

    }
}
