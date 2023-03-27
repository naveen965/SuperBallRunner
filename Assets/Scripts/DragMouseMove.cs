using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMouseMove : MonoBehaviour {

    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float moveSpeed = 400f;
    public Camera cam;
    public GameObject Reftranform;
	//public GameObject Trail;
    //public GameController Controller;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        }
        else {
            rb.velocity = Vector2.zero;
        }

        Reftranform.transform.localPosition = new Vector3(transform.position.x, 1, 0);
		//Trail.transform.localPosition = new Vector3(-0.3f, transform.position.x, -0.4f);
    }
}
