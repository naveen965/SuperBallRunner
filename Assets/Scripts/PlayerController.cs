using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float pathSpeed = 5.0f;
    public float sideSpeed = 10.0f;
    public float minX = -5.0f;
    public float maxX = 5.0f;
    public float mouseSensitivity = 10.0f;
    public float horizontalSmoothing = 0.1f;

    private List<Animator> animators;
    private string animatorName = "Rd03";

    void Start()
    {
        TileManager tileManager = FindObjectOfType<TileManager>();
        if (tileManager != null)
        {
            animators = tileManager.GetAnimators();
            //Debug.Log("Animators count: " + animators.Count);
            //transform.position = waypoints[0].position;
        }
    }

    void Update()
    {
        // Get a reference to the animation clip
        Animator rd01AnimationClip = animators[0].GetComponent<Animator>();

        // Get the name of the animation clip
        string rd01AnimationName = rd01AnimationClip.name;

        //animatorName = animators[0].name;
        //Debug.Log("animator name: " + rd01AnimationName);

        if (animatorName == "Rd03")
        {
            animators[0].enabled = true;
            animators[0].Play(animatorName, 0, 0f);
            animators[0].speed = 0.2f;
        }

        Animator firstAnimator = FindObjectOfType<TileManager>().activeTiles[0].GetComponent<Animator>();
        if (firstAnimator != null)
        {
            float animLength = firstAnimator.GetCurrentAnimatorStateInfo(0).length;
            if (Time.time - firstAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animLength)
            {
                //animators[0].enabled = false;
            }
        }

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.mousePosition.x / Screen.width * 2 - 1;
            float targetX = Mathf.Lerp(minX, maxX, (mouseX + 1) / 2);
            float smoothedX = Mathf.Lerp(transform.position.x, targetX, horizontalSmoothing);
            transform.position = new Vector3(smoothedX, transform.position.y, transform.position.z);
        }
    }
}
