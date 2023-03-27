
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Rigidbody2D rb;

    public Text scoreText;
    private float scoreAmount;
    public float pointIncreasedPerSecond;
    public Animator animator;
    public Camera camera;

    bool firstSP = false;
    bool secSP = false;
    bool thirdSP = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreAmount = 0f;
    }

    // Update is called once per frame
    void Update( )
    {
        scoreText.text = "" + (int)scoreAmount;
        scoreAmount += pointIncreasedPerSecond * Time.deltaTime;

        if (firstSP == false)
        {
            if (scoreAmount > 1000)
            {
                //animator.speed = 1.3f;
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 70.0f, 0.1f);
                firstSP = true;
            }
        }

        if (secSP == false)
        {
            if (scoreAmount > 1500)
            {
                //animator.speed = 1.5f;
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 80.0f, 0.1f);
                secSP = true;
            }
        } 
            
            
        if (thirdSP == false)
        {
            if (scoreAmount > 2000)
            {
                //animator.speed = 1.8f;
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 90.0f, 0.1f);
                thirdSP = true;
            }
        }    
    }

    public void UpdateScore()
    {
        scoreAmount = 0f;
        scoreText.text = "0";
    }

    public void ScoreReset()
    {
        scoreAmount = 0f;
        animator.speed = 0.3f;
        camera.fieldOfView = 60.0f;
		firstSP = false;
        secSP = false;
        thirdSP = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "CollectingStar")
        {
            scoreAmount += 10f;
            //Debug.Log("Added score: " + scoreAmount);
        }
    }
}
