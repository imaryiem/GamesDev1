using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : MonoBehaviour
{
    [SerializeField] private Animator myBarn = null;

    private bool playerInTrigger = false;
    private bool isBarnOpen = false;

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
            if (isBarnOpen)
            {
                Debug.Log("Press F to close the Barn");
            }
            else
            {
                Debug.Log("Press F to open the Barn");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isBarnOpen)
                {
                    myBarn.Play("BarnClose", 0, 0.0f);
                    isBarnOpen = false;
                }
                else
                {
                    myBarn.Play("BarnOpen", 0, 0.0f);
                    isBarnOpen = true;
                }
            }
        }
    }
}
