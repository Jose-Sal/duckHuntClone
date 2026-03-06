using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    //reference to the object pool that this bullet belongs to, so it can return itself to the pool when it's done
    private IObjectPool<Bullet> objectPool;
    //public property to give the bullet a reference to its object pool
    public IObjectPool<Bullet> ObjectPool
    {
        //get => objectPool;
        set => objectPool = value;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    //function to apply velocity to the bullet and start the despawn timer
    public void Fire(Vector3 velocity)
    {
        rb.velocity = velocity;
        StartCoroutine(DespawnBullet(5f));
    }

    IEnumerator DespawnBullet(float delay)
    {
        yield return new WaitForSeconds(delay);
        //reset the moving RigidBody
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //release the bullet back to the pool
        objectPool.Release(this);
    }   
}
