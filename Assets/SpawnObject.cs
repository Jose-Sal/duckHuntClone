using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnObject : MonoBehaviour
{

    bool spawnButton;
    [SerializeField]public Projectile_Pooling prefabObjectToSpawn;

    [SerializeField] private Transform parentForObjectHolder;

    //[SerializeField]List<GameObject> gameObjectPool = new List<GameObject>();


    //using unity built in object pool system to manage the pooling of the spawned objects
    private IObjectPool<Projectile_Pooling> objectPool;

    //checks if we're trying to add an object to the pool that is already in the pool, if true it will give a warning and not add the object to the pool
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxPoolSize = 50;

    private void Awake()
    {
        objectPool = new ObjectPool<Projectile_Pooling>(
            () => CreatePooledItem(), 
            actionOnGet: OnGetFromPool, 
            actionOnRelease: OnReturnedToPool, 
            actionOnDestroy: OnDestroyPoolObject, 
            collectionCheck: collectionCheck, 
            defaultCapacity: defaultCapacity, 
            maxSize: maxPoolSize);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }


    //for the unity built in object pool system, this is the function that will create a new object to add to the pool when the pool is empty and we try to get an object from the pool
    private Projectile_Pooling CreatePooledItem()
    {
        Projectile_Pooling objInstance = Instantiate(prefabObjectToSpawn, parentForObjectHolder);
        objInstance.ObjectPool = objectPool;
        return objInstance;
    }

    //invoked when creating an item to populate the object pool
    private void OnGetFromPool(Projectile_Pooling pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }
    //invoked when returning an item to the object pool
    private void OnReturnedToPool(Projectile_Pooling pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }
    //invoke when we exceed the max pool size and try to get an object from the pool, it will destroy the object instead of adding it to the pool
    private void OnDestroyPoolObject(Projectile_Pooling pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    

    public void SpawnItem()
    {
        int randX = Random.Range(-5,5);
        int randZ = Random.Range(-5,5);
        //spawn location for the object with random range area
        Vector3 spawnObjectLocation = new Vector3 (randX,10,randZ);
        //Instantiate(prefabObjectToSpawn, spawnObjectLocation, Quaternion.identity,parentForObjectHolder);
        //will switch the one above to get a pooled object instead of instantiating

        //get an object from the pool and set its position to the spawn location, if the pool is empty it will create a new object and add it to the pool
        Projectile_Pooling prefObject = objectPool.Get();
        if (prefObject == null)
        {
            return;
        }
        prefObject.transform.position = spawnObjectLocation;

        prefObject.Deactivate();


    }

    public void GetInput()
    {
        //check if spanwButton is pressed with the space key
        spawnButton = Input.GetKeyDown(KeyCode.Space);
        if (spawnButton)
        {
            SpawnItem();
        }
    }

}
