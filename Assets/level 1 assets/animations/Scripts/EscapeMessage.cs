using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMessage : MonoBehaviour
{

    [SerializeField] private UIManager uiManager = null;
     private bool playerInTrigger = false;

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



    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger)
        {
            uiManager.ShowMessage("jump In The Boat To Escape"); 
        }
    }
}
