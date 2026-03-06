using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    [SerializeField] private ParticleSystem explosionParticle;

    //called when it collides with a bullet
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ExplodeObject();
        }
    }

    public void ExplodeObject()
    {
        // Instantiate explosion particle effect
        if (explosionParticle != null)
        {
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }

        //add explosion force

        Collider[] objectsWithinRadius = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in objectsWithinRadius)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            Destroy ds = collider.GetComponent<Destroy>();
            if (rb == null)
            {
                continue;
            }
            else
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                collider.GetComponent<Destroy>()?.DestroycurrentObject();
            }
        }
        //will change destroy to disable in order t change the object with the destroyed version of the object
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        //draw explosion radius in editor to view the distance of the explosion
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
