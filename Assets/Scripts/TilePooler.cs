using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;
using System;

public class TilePooler : MonoBehaviour
{
    [SerializeField] private TilePoolObject tilePrefab;

    public IObjectPool<TilePoolObject> objectPool;

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 2;
    [SerializeField] private int maxCapacity = 3;

    private void Awake() {
        objectPool = new ObjectPool<TilePoolObject>(CreateTile, OnGetFromPool, OnReleaseToPool, OnDestroyPoolObjects, collectionCheck, defaultCapacity, maxCapacity);
    }
    private TilePoolObject CreateTile() {
        TilePoolObject tileInstance = Instantiate(tilePrefab);
        tileInstance.TileObjectPool = objectPool;
        return tileInstance;
    }

    private void OnGetFromPool(TilePoolObject pooledObject) {
        pooledObject.gameObject.SetActive(true);
    }
    private void OnReleaseToPool(TilePoolObject pooledObject) {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObjects(TilePoolObject pooledObject) {
        Destroy(pooledObject.gameObject);
    }

    

}
