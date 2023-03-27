using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "RefTrasform")
        {
            //Debug.Log("Animator speed: " + FindObjectOfType<Timer>().animator.speed);
            StartCoroutine(speedDown());
            //Debug.Log("Animator speed 2: " + FindObjectOfType<Timer>().animator.speed);
        }
    }

    IEnumerator speedDown()
    {
        FindObjectOfType<Timer>().animator.speed += 0.3f;

        yield return new WaitForSeconds(6);

        if(FindObjectOfType<Timer>().animator.speed >= 0.6f)
        {
            FindObjectOfType<Timer>().animator.speed -= 0.3f;
        }
    }
}
