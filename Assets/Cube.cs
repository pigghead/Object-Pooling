using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IPooledObject
{
    private float up = 1.0f;
    private float side = 0.1f;

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-side, side);
        float yForce = Random.Range(up / 2f, up);
        float zForce = Random.Range(-side, side);

        Vector3 spawnForce = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = spawnForce;
    }
}
