using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.EventSystems;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int defualtPoolCapacity = 20;
    [SerializeField] private int maxPoolCapacity = 50;
    [SerializeField] private float shootingSpeed = 30f;

    [SerializeField]LayerMask uiLayerMask;
    private ObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            createFunc: () => CreateBullet(),
            actionOnGet: (bullet) => bullet.gameObject.SetActive(true),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
            collectionCheck: true,
            defaultCapacity: defualtPoolCapacity,
            maxSize: maxPoolCapacity
            );
    }

    public Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bulletInstance.ObjectPool = bulletPool;
        return bulletInstance;
    }


    // Update is called once per frame
    void Update()
    {
        //get mouseclick input
        if (Input.GetMouseButtonDown(0))
        {
            //shoot a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //check if the ray hits any UI elements
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //if the ray hits the UI, do not shoot
                Debug.Log("Ray hit the UI, not shooting.");
            }
            else
            {
                Shoot(ray);
            }

        }

        void Shoot(Ray ray)
        {
            //get the direction from the camera to the mouse position in world space
            Vector3 direction = ray.direction;;
            Bullet bulletprefab;

            HandleBulletTransformandPositioning(ray, direction, out bulletprefab);
            //apply the velocity to the bullet's rigidbody
            var bulletInfo = bulletprefab.GetComponent<Bullet>();
            if (bulletInfo != null)
            {
                bulletInfo.Fire(direction.normalized * shootingSpeed);
            }
            else
            {
                Debug.LogError("Bullet prefab does not have a Bullet component attached.");
            }
        }
    }

    private void HandleBulletTransformandPositioning(Ray ray, Vector3 direction, out Bullet bulletprefab)
    {
        
        bulletprefab = bulletPool.Get();
        bulletprefab.transform.position = Camera.main.transform.position + direction.normalized;
        bulletprefab.transform.rotation = Quaternion.LookRotation(direction);
    }
}
