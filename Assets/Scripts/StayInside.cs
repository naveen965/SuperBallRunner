using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2f, 2f),
			transform.position.y, transform.position.z);
	}
}
