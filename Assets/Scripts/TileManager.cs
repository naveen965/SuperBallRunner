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
    private float timeSinceLastSpawn = 0;
    private float halfFirstAnimLength = 0;

    void Start()
    {
        SpawnTile(0);
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
                SpawnTile(Random.Range(0, tilePrefabs.Length));
                timeSinceLastSpawn = 0;
            }

            if (Time.time - firstAnimator.GetCurrentAnimatorStateInfo(0).length >= animLength)
            {
                DeleteTile();
            }
        }

        timeSinceLastSpawn += Time.deltaTime;*/
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject gameObject = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(gameObject);
        zSpawn += tileLength;
        SetAnimators(tilePrefabs[tileIndex]);

        if (activeTiles.Count == 1)
        {
            Animator firstAnimator = activeTiles[0].GetComponent<Animator>();
            if (firstAnimator != null)
            {
                halfFirstAnimLength = firstAnimator.GetCurrentAnimatorStateInfo(0).length / 2;
            }
        }
    }

    public void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        animators.RemoveAt(0);
    }

    private void SetAnimators(GameObject pathSegment)
    {
        animators.Insert(0, pathSegment.GetComponent<Animator>());
    }

    public List<Animator> GetAnimators()
    {
        return animators;
    }
}
