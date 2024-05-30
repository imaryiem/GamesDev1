using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] private Animator myFarm = null;

    private bool playerInTrigger = false;
    private bool isFarmOpen = false;

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
            if (isFarmOpen)
            {
                Debug.Log("Press F to close the Farm");
            }
            else
            {
                Debug.Log("Press F to open the Farm");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isFarmOpen)
                {
                    myFarm.Play("FarmClose", 0, 0.0f);
                    isFarmOpen = false;
                }
                else
                {
                    myFarm.Play("FarmOpen", 0, 0.0f);
                    isFarmOpen = true;
                }
            }
        }
    }
}
