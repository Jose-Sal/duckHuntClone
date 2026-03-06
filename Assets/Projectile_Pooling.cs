using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile_Pooling : MonoBehaviour
{
    private float despawnTimer = 5f;

    //reference to the object pool that this projectile belongs to, so it can return itself to the pool when it's done
    private IObjectPool<Projectile_Pooling> objectPool;
    //public property to give the projectile a reference to its object pool
    public IObjectPool<Projectile_Pooling> ObjectPool
    {
        //get => objectPool;
        set => objectPool = value;
    }


    public void Deactivate()
    {
        StartCoroutine(DespawnObject(despawnTimer));
    }

    IEnumerator DespawnObject(float delay)
    {
        yield return new WaitForSeconds(delay);

        //reset the moving RigidBody
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //will switche destory with release to pool of despawned object
        //Destroy(gameObject);

        //release the object back to the pool
        objectPool.Release(this);
    }
}
