using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
   // [SerializeField] private float Yaxis;
    public GameObject gameControll;
    public AudioSource audio;
    public AudioClip hit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "power")
        {
			
            audio.PlayOneShot(hit);
            gameControll.GetComponent<GameController>().dubblescore();
			
        }
		
		if (other.tag == "health")
        {
			
            audio.PlayOneShot(hit);
            gameControll.GetComponent<GameController>().addhelth();
			
        }
		
		if (other.tag == "sheld")
        {
			
            audio.PlayOneShot(hit);
            gameControll.GetComponent<GameController>().sheldOn();
			
        }

        if (other.tag == "coin")
        {
            audio.PlayOneShot(hit);
            gameControll.GetComponent<GameController>().CoinCollected();
        }

    }
	
}
