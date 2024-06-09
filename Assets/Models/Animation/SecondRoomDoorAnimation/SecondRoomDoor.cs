using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRoomDoor : MonoBehaviour
{
    [SerializeField] private Animator mySecondDoor = null;
    [SerializeField] private bool triggerOpen = false;
    [SerializeField] private bool triggerClose = false;
    [SerializeField] private AudioSource doorAudioSource = null; 
    [SerializeField] private AudioClip doorOpenSound = null; 
    [SerializeField] private AudioClip doorCloseSound = null; 

    private bool isOpening = false;
    private bool isClosing = false;

    [SerializeField] private Crate crate = null;
    [SerializeField] private thirdLevelUIManager uiManager = null; // Reference to the ThirdLevelUIManager

    private bool hasKey = false;


    private void PlayDoorSound(AudioClip sound)
    {
        if (doorAudioSource && sound)
        {
            doorAudioSource.PlayOneShot(sound); // Play the specified sound
        }
    }

    private void Start()
    {
        hasKey = crate.GetHasKey();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerOpen)
            {
                if (hasKey)
                {
                    uiManager.ShowMessage("Press F to open the second door"); // Use ThirdLevelUIManager to display message
                    isOpening = true;
                }
                else
                {
                    uiManager.ShowMessage("You do not have the key"); // Use ThirdLevelUIManager to display message
                }
            }
            else if (triggerClose)
            {
                // Debug.Log("Door will close automatically");
                isClosing = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.ClearMessage();  // Clear the message when the player leaves

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
        hasKey = crate.GetHasKey(); // Retrieve the hasKey value each frame

        if (isOpening && Input.GetKeyDown(KeyCode.F) && hasKey)
        {
            mySecondDoor.Play("SecondRoomDoorOpen", 0, 0.0f);
            isOpening = false;
            PlayDoorSound(doorOpenSound);
        }

        if (isClosing)
        {
            mySecondDoor.Play("SecondRoomDoorClose", 0, 0.0f);
            isClosing = false;
            gameObject.SetActive(false);
            PlayDoorSound(doorCloseSound);
        }
    }
}
