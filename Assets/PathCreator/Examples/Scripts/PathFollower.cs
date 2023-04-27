using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;
        public float minX = -5.0f;
        public float maxX = 5.0f;
        public float horizontalSmoothing = 0.1f;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                // Move the ball along the path
                distanceTravelled += speed * Time.deltaTime;
                Vector3 newPosition = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                // Add 0.5 to the y value of the position
                newPosition.y += 0.5f;
                transform.position = newPosition;
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

                // Check if the left mouse button is down
                if (Input.GetMouseButton(0))
                {
                    float mouseX = Input.mousePosition.x / Screen.width * 2 - 1;
                    float targetX = Mathf.Lerp(minX, maxX, (mouseX + 1) / 2);
                    float smoothedX = Mathf.Lerp(transform.position.x, targetX, horizontalSmoothing);
                    /*transform.position = new Vector3(smoothedX, transform.position.y, transform.position.z);
                    // Move the ball left or right based on the mouse position
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);*/
                    newPosition.x += smoothedX;
                    transform.position = newPosition;
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                }
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}