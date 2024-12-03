using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TilePoolObject : MonoBehaviour {
    
    private IObjectPool<TilePoolObject> tileObjectPool;

    public IObjectPool<TilePoolObject> TileObjectPool {
        set { tileObjectPool = value; }
    }

    public void Deactivate() {
        StartCoroutine(DeactivateRountine(5));
    }

    IEnumerator DeactivateRountine(float delay) { 
        yield return new WaitForSeconds(delay);
        tileObjectPool.Release(this);
    }

}
