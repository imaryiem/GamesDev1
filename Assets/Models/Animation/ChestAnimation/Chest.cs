using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] private Animator doorAnimator = null;

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
                Debug.Log("Press F to CLOSE");
            }
            else
            {
                Debug.Log("Press F to OPEN");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isDoorOpen)
                {
                    doorAnimator.Play("closeChest", 0, 0.0f);
                    isDoorOpen = false;
                }
                else
                {
                    doorAnimator.Play("openChest", 0, 0.0f);
                    isDoorOpen = true;
                }
            }
        }
    }
}