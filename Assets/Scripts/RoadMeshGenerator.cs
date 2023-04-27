using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{
    public class RoadMeshGenerator : MonoBehaviour
    {
        public Material roadMaterial;

        // Start is called before the first frame update
        void Start()
        {
            BezierPath bezierPath = GetComponent<PathCreator>().bezierPath;

            Mesh roadMesh = new Mesh();

            // Populate the vertices array with the positions of the Bezier path
            Vector3[] vertices = new Vector3[bezierPath.NumPoints];
            for (int i = 0; i < bezierPath.NumPoints; i++)
            {
                vertices[i] = bezierPath.GetPoint(i);
            }

            // Populate the triangles array with indices to form the road mesh
            int numSegments = vertices.Length - 1;
            int[] triangles = new int[numSegments * 2 * 3];
            int triangleIndex = 0;
            for (int i = 0; i < numSegments; i++)
            {
                triangles[triangleIndex++] = i;
                triangles[triangleIndex++] = i + 1;
                triangles[triangleIndex++] = i + bezierPath.NumPoints;

                triangles[triangleIndex++] = i + 1;
                triangles[triangleIndex++] = i + bezierPath.NumPoints + 1;
                triangles[triangleIndex++] = i + bezierPath.NumPoints;
            }

            roadMesh.vertices = vertices;
            roadMesh.triangles = triangles;

            GetComponent<MeshFilter>().mesh = roadMesh;

            GetComponent<MeshRenderer>().material = roadMaterial;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}