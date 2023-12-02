using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrenadeScript : MonoBehaviour
{

    public float delay = 3f;
    public float radius = 5f;

    public float force = 700f;
    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;


    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        // SHOW EFFECT
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.Destroy();
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
        
        foreach (Collider nearbyObject in collidersToMove)
        {
            // ADD FORCE
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        // GET NEARBY OBJECTS
            //ADD FORCE
            // DAMAGE
        // REMOVE GRENADE
        Destroy(gameObject);
        // Debug.Log("BOOM!");
    }
}
