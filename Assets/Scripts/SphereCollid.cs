using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "3DBall")
        {
            int waypointIndex = FindObjectOfType<PathCreation.Examples.GeneratePathExample>().GetWaypoints().IndexOf(other.transform);

            Debug.Log("waypointIndex: " + waypointIndex);

            StartCoroutine(FindObjectOfType<PathCreation.Examples.GeneratePathExample>().spawnObj());
        }
    }
}
