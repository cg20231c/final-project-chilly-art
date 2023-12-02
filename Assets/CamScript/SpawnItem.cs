using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject spawnItem;

    public KeyCode spawnKey = KeyCode.Q;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            itemSpawn();
        }
    }

    void itemSpawn(){
        GameObject item = Instantiate(spawnItem, transform.position, transform.rotation);
        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
