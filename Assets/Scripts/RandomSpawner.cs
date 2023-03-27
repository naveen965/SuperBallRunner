using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject fallingObjectPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(transform.position.x + 30, transform.position.x - 30), Random.Range(20, 40), 
                Random.Range(transform.position.z + 30, transform.position.z - 30));

            Instantiate(fallingObjectPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }
}
