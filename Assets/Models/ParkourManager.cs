using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourTileRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Disable the key card
            gameObject.SetActive(false);
        }
    }
}
