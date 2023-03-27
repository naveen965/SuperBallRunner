using UnityEngine;

public class BallRunner : MonoBehaviour
{
    // Set game variables
    public float pathSpeed = 25f;
    public float pathWidth = 100f;
    public Color pathColor = Color.green;

    //public GameObject path;
    private bool isRunning = true;

    void Start()
    {
        // Create path object
        //path = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //path.transform.localScale = new Vector3(Screen.width, pathWidth, 1f);
        //path.transform.position = new Vector3(Screen.width / 2f, 0f, 0f);
        //path.GetComponent<Renderer>().material.color = pathColor;
    }

    void Update()
    {
        if (isRunning)
        {
            /*// Move path backward
            path.transform.Translate(0f, -pathSpeed * Time.deltaTime, 0f);

            // Check if path is out of screen
            if (path.transform.position.z < -Screen.width / 2f)
            {
                // Respawn path at the end of the screen
                path.transform.position = new Vector3(Screen.width / 2f, 0f, 0f);
            }*/

            transform.Translate(Vector3.back * pathSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if ball collides with path
        /*if (collision.gameObject == path)
        {
            // Game over
            isRunning = false;
        }*/
    }
}
