using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSphere : MonoBehaviour
{
    public GameObject spherePrefab;
    public float spawnSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            GameObject spawnedSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
            Rigidbody sphereRB = spawnedSphere.GetComponent<Rigidbody>();
            sphereRB.velocity = transform.forward * spawnSpeed;
        }
    }
}
