using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnDoorCont : MonoBehaviour
{
   [SerializeField] private Animator myBarn = null;
   [SerializeField] private UIManager uiManager = null; 

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
            uiManager.ShowMessage("");
        }
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            if (isBarnOpen)
            {
                uiManager.ShowMessage("Press F to close the Barn");
            }
            else
            {
                uiManager.ShowMessage("Press F to open the Barn");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isBarnOpen)
                {
                    myBarn.Play("BarnDoorClose", 0, 0.0f);
                    isBarnOpen = false;
                }
                else
                {
                    myBarn.Play("BarnDoorOpen", 0, 0.0f);
                    isBarnOpen = true;
                }
            }
        }
    }
}
