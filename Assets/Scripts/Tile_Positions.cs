using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Positions : MonoBehaviour
{
    public static Transform[] tilePositions;

    private void Awake() {
        tilePositions = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            tilePositions[i] = transform.GetChild(i);
        }
    }
}
