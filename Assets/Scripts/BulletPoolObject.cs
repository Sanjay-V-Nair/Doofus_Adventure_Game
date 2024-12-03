using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolObject : MonoBehaviour
{
    private IObjectPool<BulletPoolObject> bulletObject;

    public IObjectPool<BulletPoolObject> BulletObject {
        set { bulletObject = value; }
    }

    public void Deactivate() {
        StartCoroutine(DeactivateRoutine(2));
    }

    IEnumerator DeactivateRoutine(float delay) {
        yield return new WaitForSeconds(delay);
        bulletObject.Release(this);
    }
}
