using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f;
    public AnimationCurve curve;
    public Transform[] pathPoints;
    public bool loop = true;

    private float timer;
    private int currentPointIndex;

    private void Update()
    {
        timer += Time.deltaTime * speed;

        if (timer > 1f)
        {
            timer = 0f;

            if (currentPointIndex == pathPoints.Length - 1)
            {
                if (loop)
                {
                    currentPointIndex = 0;
                }
                else
                {
                    enabled = false;
                }
            }
            else
            {
                currentPointIndex++;
            }
        }

        Vector3 startPosition = pathPoints[currentPointIndex].position;
        Vector3 endPosition = pathPoints[(currentPointIndex + 1) % pathPoints.Length].position;

        transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(timer));
    }
}
