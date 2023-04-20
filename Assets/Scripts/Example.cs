using UnityEngine;

public class Example : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public int pointCount;

    public BezierSpline spline;

    private Vector3[] path;

    private void Start()
    {
        //path = spline.GeneratePath(startPoint, endPoint, pointCount);
    }

    private void OnDrawGizmos()
    {
        if (path != null)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < path.Length - 1; i++)
            {
                Gizmos.DrawLine(path[i], path[i + 1]);
            }
        }
    }
}
