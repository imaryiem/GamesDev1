using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool triggerOpen = false;
    [SerializeField] private bool triggerClose = false;

    private bool isOpening = false;
    private bool isClosing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerOpen)
            {
                Debug.Log("Press F to open the door");
                isOpening = true;
            }
            else if (triggerClose)
            {
                Debug.Log("Door will close automatically");
                isClosing = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerOpen)
            {
                isOpening = false;
            }
            else if (triggerClose)
            {
                isClosing = false;
            }
        }
    }

    private void Update()
    {
        if (isOpening && Input.GetKeyDown(KeyCode.F))
        {
            myDoor.Play("doorOpen", 0, 0.0f);
            isOpening = false;
        }

        if (isClosing)
        {
            myDoor.Play("doorClose", 0, 0.0f);
            isClosing = false;
            gameObject.SetActive(false);
        }
    }
}
