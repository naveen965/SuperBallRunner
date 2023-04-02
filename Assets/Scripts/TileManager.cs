using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 2600;
    public List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;

    public List<Animator> animators = new List<Animator>();
    //public List<Transform> waypoints = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            if(i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 1305 > zSpawn - (tilePrefabs.Length * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject gameObject = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(gameObject);
        zSpawn += tileLength;
        //CreateWaypoints(tilePrefabs[tileIndex]);
        SetAnimators(tilePrefabs[tileIndex]);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        animators.RemoveAt(0);
    }

    private void SetAnimators(GameObject pathSegment)
    {
        animators.Add(pathSegment.GetComponent<Animator>());
    }

    public List<Animator> GetAnimators()
    {
        return animators;
    }


    /*// This method use for set waypoints in active tiles.

    private void CreateWaypoints(GameObject roadSegment)
    {
        Transform[] segmentWaypoints = roadSegment.GetComponentsInChildren<Transform>();
        foreach (Transform waypoint in segmentWaypoints)
        {
            if (waypoint != roadSegment.transform && waypoint.CompareTag("waypoint"))
            {
                waypoints.Add(waypoint);
            }
        }
    }

    // This method use for get waypoints in active tiles.

    public List<Transform> GetWaypoints()
    {
        return waypoints;
    }*/
}
