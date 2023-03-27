using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float earnedpoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 90 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "RefTrasform")
        {
            earnedpoints++;
            //booomapi.sendEarnedPoints();
            Debug.Log("Colleced!");
            Destroy(gameObject);
        }

    }
}
