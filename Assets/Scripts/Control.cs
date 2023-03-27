using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public GameObject mobile;
    public GameObject fullscreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ctrl(string text)
    {
        string x = (text);
        
        if (x == "true")
        {
            mobile.SetActive(true);
        }

        if (x == "false")
        {
            mobile.SetActive(false);
        }

    }

    public void screenF(string text)
    {
        string x = (text);
        if (x == "true")
        {
            fullscreen.SetActive(true);
            Time.timeScale = 0;
        }

        if (x == "false")
        {
            fullscreen.SetActive(false);
            Time.timeScale = 1;
        }

    }
}
