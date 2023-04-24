using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = false;
        public bool isSpawn = false;
        public float zSpawn = 0;
        public float tileLength;
        public GameObject waypoint;
        public GameObject waypointsPerant;

        private List<Transform> waypoints = new List<Transform>();

        void Start () 
        {
            isSpawn = true;

            if (waypoints.Count > 0) {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator> ().bezierPath = bezierPath;
            }
        }

        void Update ()
        {
            if (isSpawn == true)
            {
                StartCoroutine(spawnObj());
            }
        }

        private IEnumerator spawnObj()
        {
            SpawnWaypoint();
            yield return new WaitForSeconds(3f);
            DeleteWaypoints();
            isSpawn = false;
        }

        public void SpawnWaypoint()
        {
            //GameObject spawnPosition = GameObject.FindGameObjectWithTag("SpawnPosition");

            GameObject gameObject = Instantiate(waypoint, transform.forward * zSpawn, transform.rotation);

            gameObject.transform.SetParent(waypointsPerant.transform);

            SetWaypoints(gameObject.transform);

            zSpawn += tileLength;
        }

        private void SetWaypoints(Transform waypoint)
        {
            waypoints.Add(waypoint);
        }

        private void DeleteWaypoints()
        {
            foreach (Transform waypoint in waypoints)
            {
                if (waypoint == waypoints[0] && waypoint.CompareTag("way"))
                {
                    waypoints.Remove(waypoint);
                    Destroy(gameObject);
                }
            }
        }

        public List<Transform> GetWaypoints()
        {
            return waypoints;
        }
    }
}