using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class TileSpawner : MonoBehaviour
{
    public float initialLife = 5;
    private float tileLife;

    public TilePoolObject tilePoolObject;
    [Inject] private TilePooler tilePooler;

    private Transform tileTransformCache;
    private Vector3 previousTileCache;

    private void Start() {
        tilePoolObject = tilePooler.objectPool.Get();
        tilePoolObject.transform.position = Tile_Positions.tilePositions[0].position;
        tileLife = initialLife;
        tilePoolObject.Deactivate();
    }


    private void Update() {
        if (tileLife <= 2.5) {
            tileLife = initialLife;
            tileTransformCache = NearestObj(tilePoolObject);
            TileSpawn(tileTransformCache);
        }
        tileLife -= Time.deltaTime;
        tileLife = Mathf.Clamp(tileLife, 0, Mathf.Infinity);
        if (tileLife <= 0) tileLife = initialLife;
        
    }


    public Transform NearestObj(TilePoolObject tile) {
        List<Transform> tiles = new List<Transform>();
        float shortestDist = Mathf.Infinity;

        for (int i = 0; i < Tile_Positions.tilePositions.Length; i++) {
            float dist = Vector3.Distance(tilePoolObject.transform.position, Tile_Positions.tilePositions[i].position);

            if (dist > 0 && (previousTileCache != Tile_Positions.tilePositions[i].transform.position)) {
                if (dist < shortestDist) {
                    // Found a closer tile, update shortestDist and clear the list
                    shortestDist = dist;
                    tiles.Clear();
                    tiles.Add(Tile_Positions.tilePositions[i]);
                }
                else if (Mathf.Approximately(dist, shortestDist)) {
                    // If the distance is equal to the shortest distance, add it to the list
                    tiles.Add(Tile_Positions.tilePositions[i]);
                }
            }
        }
        previousTileCache = tilePoolObject.transform.position;
        return tiles[Random.Range(0, tiles.Count)];
    }

    public void TileSpawn(Transform positionToGo) {

        tilePoolObject = tilePooler.objectPool.Get();

        if (Tile_Positions.tilePositions == null || Tile_Positions.tilePositions.Length == 0) {
            Debug.LogError("Tile positions are not initialized or empty!");
            return;
        }

        tilePoolObject.transform.SetPositionAndRotation(positionToGo.position, positionToGo.rotation);

        if (tilePoolObject != null) { 
            tilePoolObject.Deactivate();
        }

    }

}
