using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f; // the speed of the camera rotation
    public Vector3 offset; // the camera offset from the player

    private Vector3 desiredPosition; // the desired position of the camera
    private Vector3 smoothedPosition; // the smoothed position of the camera
    private Quaternion lookRotation; // the desired rotation of the camera

    void LateUpdate()
    {
        desiredPosition = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Rotate the camera to follow the player's movement
        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0f;
        lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, smoothSpeed);
    }
}

