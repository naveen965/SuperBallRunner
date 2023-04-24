using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{
    public class SphereCollid : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Follower")
            {
                FindObjectOfType<GeneratePathExample>().isSpawn = true;
            }
        }
    }
}
