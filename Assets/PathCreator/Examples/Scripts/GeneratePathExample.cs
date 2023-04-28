using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = false;
        public float zSpawn = 0f;
        public float tileLength = 25f;
        public GameObject waypoint;
        public GameObject waypointsPerant;
        public float spawnRange = 3f;

        private List<Transform> waypoints = new List<Transform>();

        void Start () 
        {
            for (int i = 0; i < 6; i++)
            {
                SpawnWaypoint();
            }

            if (waypoints.Count > 0) {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator> ().bezierPath = bezierPath;
            }
        }

        void Update ()
        {
            
        }

        public void spawnObj()
        {
            SpawnWaypoint();

            if (waypoints.Count > 0)
            {
                // Create a new bezier path from the waypoints.
                BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                RoadMeshCreator roadMeshCreator = new RoadMeshCreator();

                GetComponent<PathCreator>().bezierPath = bezierPath;
            }

            DeleteWaypoint(waypoints[0]);
        }

        public void SpawnWaypoint()
        {
            float zPos = 0;
            float xPos = 0;
            float yPos = 0;
            float randomNumber = Random.Range(-spawnRange, spawnRange);

            int randomInt = Random.Range(0, 3);

            if (randomInt == 0)
            {
                zPos = transform.position.z + randomNumber + zSpawn;
                xPos = transform.position.x;
                yPos = transform.position.y;
            }
            else if (randomInt == 1)
            {
                zPos = transform.position.z + zSpawn;
                xPos = transform.position.x + randomNumber;
                yPos = transform.position.y;
            }
            else if (randomInt == 2)
            {
                zPos = transform.position.z + zSpawn;
                xPos = transform.position.x;
                yPos = transform.position.y + randomNumber;
            }

            Vector3 spawnPos = new Vector3 (xPos, yPos, zPos);

            GameObject newWaypoint = Instantiate(waypoint, spawnPos, transform.rotation);
            newWaypoint.transform.SetParent(waypointsPerant.transform);

            SetWaypoints(newWaypoint.transform);

            zSpawn += tileLength;
        }


        private void SetWaypoints(Transform waypoint)
        {
            waypoints.Add(waypoint);
        }

        public void DeleteWaypoint(Transform waypoint)
        {
            // Remove the waypoint from the waypoints list
            waypoints.Remove(waypoint);

            // Destroy the waypoint object
            Destroy(waypoint.gameObject);

            // Rebuild the BezierPath object without the removed waypoint
            if (waypoints.Count > 0)
            {
                BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;
            }
            else
            {
                GetComponent<PathCreator>().bezierPath = null;
            }
        }

        public List<Transform> GetWaypoints()
        {
            return waypoints;
        }
    }
}