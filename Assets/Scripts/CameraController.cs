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
    private float fixedXRotation = 16f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        desiredPosition = target.position + offset;
        smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;

        // Rotate the camera to follow the player's movement
        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0f;
        lookRotation = Quaternion.LookRotation(lookDirection);
        lookRotation *= Quaternion.Euler(fixedXRotation, 0f, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, smoothSpeed * Time.deltaTime);
    }
}
