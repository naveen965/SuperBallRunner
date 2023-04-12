using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindingPath : MonoBehaviour
{
    public List<Transform> waypoints;
    public GameObject ParentWaypoints;
    public GameObject Waypoint;
    public float pathSpeed = 5.0f;
    public float sideSpeed = 10.0f;
    public float minX = -5.0f;
    public float maxX = 5.0f;
    public float mouseSensitivity = 10.0f;
    public float horizontalSmoothing = 0.1f;

    private int currentWaypoint = 0;
    private float targetX;

    void Start()
    {
        TileManager tileManager = FindObjectOfType<TileManager>();

        if (tileManager != null)
        {
            SetWindingPath(tileManager);

            Debug.Log("way points: " + waypoints.Count);

            if (waypoints.Count > 0)
                transform.position = waypoints[0].position;
        }
    }

    void Update()
    {
        /*TileManager tileManager = FindObjectOfType<TileManager>();

        Debug.Log("current waypoint in update: " + currentWaypoint);
        Debug.Log("waypoints count:" + waypoints.Count);


        if (tileManager != null)
        {
            SetWindingPath(tileManager);
        }*/

        // Move along the winding path
        if (currentWaypoint < waypoints.Count)
        {
            float pathStep = pathSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, pathStep);

            if (transform.position == waypoints[currentWaypoint].position)
            {
                currentWaypoint++;
                Debug.Log("current waypoint in if: " + currentWaypoint);
            }
        }

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.mousePosition.x / Screen.width * 2 - 1;
            float targetX = Mathf.Lerp(minX, maxX, (mouseX + 1) / 2);
            float smoothedX = Mathf.Lerp(transform.position.x, targetX, horizontalSmoothing);
            transform.position = new Vector3(smoothedX, transform.position.y, transform.position.z);
        }
    }

    public void SetWindingPath(TileManager pathManager)
    {
        foreach (Transform waypoint in pathManager.GetWaypoints())
        {
            GameObject gameObject = Instantiate(Waypoint, waypoint.position, transform.rotation);

            gameObject.transform.SetParent(ParentWaypoints.transform);

            waypoints.Add(gameObject.transform);
        }
    }

    public void DeleteWindingPath(GameObject roadSegment)
    {
        TileManager tileManager = FindObjectOfType<TileManager>();

        foreach (Transform waypoint in tileManager.GetWaypoints())
        {
            waypoints.Remove(gameObject.transform);
        }

        Transform[] segmentWaypoints = roadSegment.GetComponentsInChildren<Transform>();
        foreach (Transform waypoint in segmentWaypoints)
        {
            if (waypoint != roadSegment.transform && waypoint.CompareTag("waypoint"))
            {
                waypoints.Remove(waypoint);
            }
        }
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindingPath : MonoBehaviour
{
    public List<Transform> waypoints;
    public float pathSpeed = 5.0f;
    public float sideSpeed = 10.0f;
    public float minX = -5.0f;
    public float maxX = 5.0f;
    public float mouseSensitivity = 10.0f;
    public float horizontalSmoothing = 0.1f;

    private int currentWaypoint = 0;
    private float targetX;

    void Start()
    {
        TileManager tileManager = FindObjectOfType<TileManager>();
        if (tileManager != null)
        {
            waypoints = tileManager.GetWaypoints();
            Debug.Log("way points: " + waypoints.Count);
            if (waypoints.Count > 0)
                transform.position = waypoints[0].position;
        }
    }

    void Update()
    {
        *//*Debug.Log("current waypoint in update: " + currentWaypoint);
        Debug.Log("waypoints count:" + waypoints.Count);*//*
        TileManager tileManager = FindObjectOfType<TileManager>();

        if (tileManager != null)
        {
            waypoints = FindObjectOfType<TileManager>().GetWaypoints();
        }

        // Move along the winding path
        if (currentWaypoint < waypoints.Count)
        {
            float pathStep = pathSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, pathStep);

            if (transform.position == waypoints[currentWaypoint].position)
            {
                currentWaypoint++;
                //Debug.Log("current waypoint in if: " + currentWaypoint);
            }
        }

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.mousePosition.x / Screen.width * 2 - 1;
            float targetX = Mathf.Lerp(minX, maxX, (mouseX + 1) / 2);
            float smoothedX = Mathf.Lerp(transform.position.x, targetX, horizontalSmoothing);
            transform.position = new Vector3(smoothedX, transform.position.y, transform.position.z);
        }
    }
}
*/