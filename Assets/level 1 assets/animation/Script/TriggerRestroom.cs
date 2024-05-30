using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestRoom : MonoBehaviour
{
    [SerializeField] private Animator myRest = null;

    private bool playerInTrigger = false;
    private bool isRestOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            if (isRestOpen)
            {
                Debug.Log("Press F to close the Restroom");
            }
            else
            {
                Debug.Log("Press F to open the Restroom");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isRestOpen)
                {
                    myRest.Play("RestClose", 0, 0.0f);
                    isRestOpen = false;
                }
                else
                {
                    myRest.Play("RestOpen", 0, 0.0f);
                    isRestOpen = true;
                }
            }
        }
    }
}
