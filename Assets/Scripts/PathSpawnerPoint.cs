using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawnerPoint : MonoBehaviour
{
    private int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball")
        {
            randomNumber = Random.Range(0, FindObjectOfType<TileManager>().tilePrefabs.Length);
            Debug.Log("Colleced!");
            FindObjectOfType<TileManager>().SpawnTile(randomNumber);

            // Wait for 3 seconds before deleting the tile
            yield return new WaitForSeconds(3f);

            FindObjectOfType<TileManager>().DeleteTile();
        }
    }
}
