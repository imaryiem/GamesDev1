using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool triggerOpen = false;
    [SerializeField] private bool triggerClose = false;
    [SerializeField] private GameObject keypadUI = null;  // Reference to the Keypad UI
    [SerializeField] private thirdLevelUIManager uiManager = null; // Reference to the UI Manager

    private bool isOpening = false;
    private bool isClosing = false;
    private bool isPlayerNear = false;  // Track if the player is near the door

    private void Start()
    {
        keypadUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            if (triggerOpen)
            {
                uiManager.ShowMessage("Press F to open the door");
                isOpening = true;
            }
            else if (triggerClose)
            {
                //Debug.Log("Door will close automatically");
                isClosing = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.ClearMessage();
            isPlayerNear = false;

            if (triggerOpen)
            {
                isOpening = false;
                keypadUI.SetActive(false);  // Hide the keypad UI if the player moves away
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (triggerClose)
            {
                isClosing = false;
            }
        }
    }

    private void Update()
    {
        if (isOpening && isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            uiManager.ClearMessage();
            keypadUI.SetActive(true);  // Show the keypad UI
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
            Cursor.visible = true;  // Make the cursor visible
        }

        if (isClosing)
        {
            myDoor.Play("doorClose", 0, 0.0f);
            isClosing = false;
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor
            Cursor.visible = false;  // Hide the cursor
        }
    }

    public void OpenDoor()
    {
        myDoor.Play("doorOpen", 0, 0.0f);
        isOpening = false;
        keypadUI.SetActive(false);  // Hide the keypad UI when the door is opened
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor
        Cursor.visible = false;  // Hide the cursor
    }
}