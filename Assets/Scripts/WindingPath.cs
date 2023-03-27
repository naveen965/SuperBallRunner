using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class WindingPath : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;

    private int currentWaypoint = 0;

    public Color linecolor;
    [Range(0, 1)] public float SphereRadius;
    public List<Transform> nodes = new List<Transform>();

    void Update()
    {
        if (currentWaypoint < waypoints.Length)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, step);

            if (transform.position == waypoints[currentWaypoint].position)
            {
                currentWaypoint++;
            }
        }
    }
}*/

public class WindingPath : MonoBehaviour
{
    public List<Transform> waypoints;
    public float pathSpeed = 5.0f;
    public float sideSpeed = 10.0f;
    public float minX = -5.0f;
    public float maxX = 5.0f;

    private int currentWaypoint = 0;

    void Start()
    {
        TileManager tileManager = FindObjectOfType<TileManager>();
        if (tileManager != null)
        {
            waypoints = tileManager.GetWaypoints();
            transform.position = waypoints[0].position;
        }
    }

    void Update()
    {
        // Move along the winding path
        if (currentWaypoint < waypoints.Count)
        {
            float pathStep = pathSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, pathStep);

            if (transform.position == waypoints[currentWaypoint].position)
            {
                currentWaypoint++;
            }
        }

        // Move left or right when the mouse button is held down
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.mousePosition.x / Screen.width * 2 - 1;
            float ballX = Mathf.Lerp(minX, maxX, (mouseX + 1) / 2);
            float sideStep = sideSpeed * Time.deltaTime;
            transform.position = new Vector3(ballX, transform.position.y, transform.position.z);

            // Reset the path if the ball moves too far off course
            if (currentWaypoint > 0 && Vector3.Distance(transform.position, waypoints[currentWaypoint - 1].position) > 2.0f)
            {
                currentWaypoint = 0;
                transform.position = waypoints[0].position;
            }
        }
    }
}

/*public class WindingPath : MonoBehaviour
{
    public Transform[] waypoints;
    public float pathSpeed = 5.0f;
    public float sideSpeed = 10.0f;
    public float minX = -5.0f;
    public float maxX = 5.0f;
    public float mouseSensitivity = 10.0f;

    private int currentWaypoint = 0;

    void Update()
    {
        // Move along the path
        if (currentWaypoint < waypoints.Length)
        {
            float pathStep = pathSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, pathStep);

            if (transform.position == waypoints[currentWaypoint].position)
            {
                currentWaypoint++;
            }
        }

        // Move left or right based on mouse position
        float mouseX = Input.mousePosition.x;
        float screenX = Screen.width;
        float normalizedMouseX = Mathf.Clamp(mouseX / screenX, 0.0f, 1.0f);
        float ballX = Mathf.Lerp(minX, maxX, normalizedMouseX);

        transform.position = new Vector3(ballX, transform.position.y, transform.position.z);

        // Reset the path if the ball moves too far off course
        if (currentWaypoint > 0 && Vector3.Distance(transform.position, waypoints[currentWaypoint - 1].position) > 2.0f)
        {
            currentWaypoint = 0;
            transform.position = waypoints[0].position;
        }

        // Rotate the ball based on mouse movement
        float mouseY = Input.GetAxis("Mouse Y");
        float rotationAmount = mouseY * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAmount);
    }
}*/
