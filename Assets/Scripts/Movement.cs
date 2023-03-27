using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   // [SerializeField] private float Yaxis;
    public GameObject gameControll;

    public AudioSource audio;
    public AudioClip hit;
    public GameController Controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !Controller.GameoveON)
        {
            audio.PlayOneShot(hit);
            gameControll.GetComponent<GameController>().GameOver();
        }
    }
}
