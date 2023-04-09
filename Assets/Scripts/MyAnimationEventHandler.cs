using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAnimationEventHandler : MonoBehaviour
{
    private int randomNumber;
    /*private Dictionary<string, float> triggerPoints = new Dictionary<string, float>();

    public AnimationClip animationClip;
    public string targetMethodName; // Name of the method that the AnimationEvent calls

    void Start()
    {
        // Get the AnimationEvent array from the animation clip
        AnimationEvent[] events = animationClip.events;

        // Loop through the events and find the ones that call the target method
        foreach (AnimationEvent evt in events)
        {
            if (evt.functionName == targetMethodName)
            {
                // This event calls the target method, so retrieve the trigger point name and position
                string triggerName = evt.stringParameter;
                float triggerTime = evt.time;
                Vector3 samplePosition = new Vector3();
                animationClip.SampleAnimation(gameObject, triggerTime);
                float triggerPosition = samplePosition.z;


                // Add the trigger point to the dictionary
                triggerPoints.Add(triggerName, triggerPosition);
                Debug.Log("Found trigger point: " + triggerName + " at position " + triggerPosition);
            }
        }
    }*/

    public void MyTriggerFunction(string triggerName)
    {
        randomNumber = Random.Range(0, FindObjectOfType<TileManager>().tilePrefabs.Length);

        Debug.Log("Trigger point reached: " + triggerName);
        // Do something in response to the trigger point being reached
        //FindObjectOfType<TileManager>().SpawnTile(randomNumber);
    }

    public void MyTriggerFunction2(string triggerName)
    {
        Debug.Log("Trigger point two reached: " + triggerName);
        // Do something in response to the trigger point being reached
        FindObjectOfType<TileManager>().DeleteTile();
    }
}
