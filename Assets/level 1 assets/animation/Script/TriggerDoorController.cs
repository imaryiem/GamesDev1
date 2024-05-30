using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    private bool playerInTrigger = false;
    private bool isDoorOpen = false;

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
            if (isDoorOpen)
            {
                Debug.Log("Press F to close the door");
            }
            else
            {
                Debug.Log("Press F to open the door");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isDoorOpen)
                {
                    myDoor.Play("DoorClose", 0, 0.0f);
                    isDoorOpen = false;
                }
                else
                {
                    myDoor.Play("DoorOpen", 0, 0.0f);
                    isDoorOpen = true;
                }
            }
        }
    }
}
