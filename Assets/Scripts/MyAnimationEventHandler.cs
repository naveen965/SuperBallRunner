using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAnimationEventHandler : MonoBehaviour
{
    public void MyTriggerFunction(string triggerName)
    {
        Debug.Log("Trigger point reached: " + triggerName);
        // Do something in response to the trigger point being reached
        FindObjectOfType<TileManager>().SpawnTile(Random.Range(0, FindObjectOfType<TileManager>().tilePrefabs.Length));
    }

    public void MyTriggerFunction2(string triggerName)
    {
        Debug.Log("Trigger point two reached: " + triggerName);
        // Do something in response to the trigger point being reached
        FindObjectOfType<TileManager>().DeleteTile();
    }
}
