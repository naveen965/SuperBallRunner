using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject Path;
    public float zSpawn = 0;
    public float tileLength;
    public List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;
    public List<Animator> animators = new List<Animator>();
    public List<Transform> waypoints = new List<Transform>();

    private bool isFirst = true;
    private float timeSinceLastSpawn = 0;
    private float halfFirstAnimLength = 0;
    private Vector3 spawnPosition;

    void Start()
    {
        //spawnPosition = transform.forward * zSpawn;

        for(int i = 0; i < 3; i++)
        {
            if(i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
        }

        //isFirst = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*Animator firstAnimator = activeTiles[0].GetComponent<Animator>();
        if (firstAnimator != null)
        {
            float animLength = firstAnimator.GetCurrentAnimatorStateInfo(0).length;

            if (timeSinceLastSpawn >= halfFirstAnimLength)
            {
                isFirst = false;
            }
        }

        timeSinceLastSpawn += Time.deltaTime;*/
    }

    public void SpawnTile(int tileIndex)
    {
        /*if (isFirst == false)
        {
            GameObject spawnPos = GameObject.FindWithTag("SpawnPosition");

            Debug.Log("Spawn Position " + spawnPos.name);

            spawnPosition = spawnPos.transform.position;
        }*/

        GameObject gameObject = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);

        gameObject.transform.SetParent(Path.transform);

        activeTiles.Add(gameObject);
        zSpawn += tileLength;
        //SetAnimators(tilePrefabs[tileIndex]);

        if (activeTiles.Count == 3)
        {
            DeleteWaypoints(activeTiles[0]);
            SetWaypoints(activeTiles[2]);
        }

        /*if (activeTiles.Count == 1)
        {
            Animator firstAnimator = activeTiles[0].GetComponent<Animator>();
            if (firstAnimator != null)
            {
                halfFirstAnimLength = firstAnimator.GetCurrentAnimatorStateInfo(0).length / 2;
            }
        }*/
    }

    public void DeleteTile()
    {
        Destroy(activeTiles[0]);
        DeleteWaypoints(activeTiles[0]);
        activeTiles.RemoveAt(0);
        //animators.RemoveAt(0);
    }

    private void SetAnimators(GameObject pathSegment)
    {
        animators.Insert(0, pathSegment.GetComponent<Animator>());
    }

    private void SetWaypoints(GameObject roadSegment)
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

    private void DeleteWaypoints(GameObject roadSegment)
    {
        Transform[] segmentWaypoints = roadSegment.GetComponentsInChildren<Transform>();
        foreach (Transform waypoint in segmentWaypoints)
        {
            if (waypoint != roadSegment.transform && waypoint.CompareTag("waypoint"))
            {
                waypoints.Remove(waypoint);
            }
        }
    }

    public List<Transform> GetWaypoints()
    {
        return waypoints;
    }

    public List<Animator> GetAnimators()
    {
        return animators;
    }
}
