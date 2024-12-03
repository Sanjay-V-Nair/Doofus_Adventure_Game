using UnityEngine;
using UnityEngine.Pool;
using System;

public class TurretBulletPooler : MonoBehaviour
{

    [SerializeField] private BulletPoolObject bulletPrefab;
    public IObjectPool<BulletPoolObject> objectPool;

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 3;
    [SerializeField] private int maxCapacity = 4;


    private void Awake() {
        objectPool = new ObjectPool<BulletPoolObject>(CreateTile, OnGetFromPool, OnReleaseFromPool, OnDestroyObjects, collectionCheck, defaultCapacity, maxCapacity);

    }
    private BulletPoolObject CreateTile() {
        BulletPoolObject bulletInstance = Instantiate(bulletPrefab);
        bulletInstance.BulletObject = objectPool;
        return bulletInstance;
    }

    private void OnGetFromPool(BulletPoolObject pooledObject) {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseFromPool(BulletPoolObject pooledObject) {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyObjects(BulletPoolObject pooledObject) {
        Destroy(pooledObject.gameObject);
    }

}
