using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    public Follower followScript;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            followScript.startFollow();
        }
    }
}
