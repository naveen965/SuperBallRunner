using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make a sphere, stick this on it, put it in front of the camera, watch it go

public class PathMaker : MonoBehaviour
{
    IEnumerator<Vector3> Maker;

    // produces an endless stream of coordinates
    IEnumerator<Vector3> Generator()
    {
        while (true)
        {
            Vector3 pos = Random.onUnitSphere;
            pos *= Random.Range(1.0f, 5.0f);
            yield return pos;
        }
    }

    void Start()
    {
        Maker = Generator();

        Maker.MoveNext();
        Destination = Maker.Current;
    }

    Vector3 Destination;

    void Update()
    {
        // see how close we are
        float distance = Vector3.Distance(Destination, transform.position);

        // when we get close to destination, get a new destination
        if (distance < 0.25f)
        {
            Maker.MoveNext();
            Destination = Maker.Current;
        }

        // I'm using Lerp to make it more squishy smooth... you can
        // produce your next destination however you like...
        transform.position = Vector3.Lerp(transform.position, Destination, Time.deltaTime);
    }
}