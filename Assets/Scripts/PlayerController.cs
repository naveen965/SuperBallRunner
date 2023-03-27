using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 10f; // the speed of the ball
    public float horizontalSpeed = 10f; // the horizontal speed of the ball
    public float jumpForce = 10f; // the force of the ball jump
    public float gravityScale = 1f; // the gravity scale of the ball
    public float minX = -5f; // the minimum x-position of the ball
    public float maxX = 5f; // the maximum x-position of the ball
    public float horizontalSmoothing = 0.1f; // the smoothing factor for the horizontal movement

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        // Move the ball horizontally based on input
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.mousePosition.x / Screen.width * 2 - 1;
            float targetX = Mathf.Lerp(minX, maxX, (mouseX + 1) / 2);
            float smoothedX = Mathf.Lerp(transform.position.x, targetX, horizontalSmoothing);
            transform.position = new Vector3(smoothedX, transform.position.y, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        // Move the ball forward
        rb.MovePosition(transform.position + transform.forward * forwardSpeed * Time.fixedDeltaTime);

        // Apply gravity
        rb.AddForce(Vector3.down * gravityScale * rb.mass * Physics.gravity.magnitude);

        // Jump if the ball is grounded and space key is pressed
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Check if the ball is grounded
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
