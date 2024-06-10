using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    [SerializeField] private Animator myGrave = null;
    [SerializeField] private Renderer SecondLevelkeyRenderer = null;
    [SerializeField] private AudioSource doorAudioSource = null; 
    [SerializeField] private AudioClip graveOpenSound = null; 
    [SerializeField] private AudioClip graveCloseSound = null; 
    [SerializeField] private UIManager uiManager = null;

    private bool isOpen = false;
    private bool hasKey = false;
    private bool playerInTrigger = false;

    private void PlayGraveSound(AudioClip sound)
    {
        if (doorAudioSource && sound)
        {
            doorAudioSource.PlayOneShot(sound); // Play the specified sound
        }
    }

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
            uiManager.ShowMessage(""); // Clear the message when the player exits the trigger

            if (isOpen)
            {
                CloseGrave();
            }
        }
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            if(!isOpen)
            {
                uiManager.ShowMessage("Press F to open the grave");
            }
            else if (isOpen && !hasKey)
            {
                uiManager.ShowMessage("Press E to collect the key");
            }
            else
            {
                uiManager.ShowMessage("Find the door to escape");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!isOpen)
                {
                    OpenGrave();
                    isOpen = true;
                }
            }
            if (isOpen && !hasKey && Input.GetKeyDown(KeyCode.E))
                {
                    CollectKey();
                }
        }
    }

    private void OpenGrave()
    {
        if (myGrave == null)
        {
            Debug.LogError("Grave Animator reference is not set in the Inspector.");
            return;
        }
        
        myGrave.Play("GraveOpen", 0, 0.0f);
        isOpen = true;
        SecondLevelkeyRenderer.enabled = true; // Enable the key card renderer
        PlayGraveSound(graveOpenSound);
    }

    private void CloseGrave()
    {
        if (myGrave == null)
        {
            Debug.LogError("Grave Animator reference is not set in the Inspector.");
            return;
        }
        
        myGrave.Play("GraveClose", 0, 0.0f);
        isOpen = false;
        SecondLevelkeyRenderer.enabled = false; // Disable the key card renderer
        PlayGraveSound(graveCloseSound);
    }

    private void CollectKey()
    {
        hasKey = true;
        SecondLevelkeyRenderer.gameObject.SetActive(false); // Disable the key card renderer
    }

    public bool GetHasKey()
    {
        return hasKey;
    }
}
