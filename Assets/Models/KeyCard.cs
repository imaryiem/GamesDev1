using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    private bool hasBeenCollected = false;

    public bool GetHasBeenCollected()
    {
        return hasBeenCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Check if this is the RealKeyCard
            if (gameObject.CompareTag("RealKeyCard"))
            {
                // Set the boolean variable to true
                hasBeenCollected = true;
            }

            // Disable the key card
            gameObject.SetActive(false);

            Debug.Log(hasBeenCollected);
            Debug.Log("hello world");
        }
    }
}