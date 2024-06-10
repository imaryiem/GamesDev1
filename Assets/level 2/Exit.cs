using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private Animator door = null;
    [SerializeField] private Grave grave = null;
    [SerializeField] private AudioSource gateAudioSource = null; // Reference to the AudioSource for gate sound
    [SerializeField] private AudioClip gateOpenSound = null; // Sound clip for gate opening
    [SerializeField] private UIManager uiManager = null;

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
            uiManager.ShowMessage("");
        }
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            if (grave == null)
            {
                Debug.LogError("Grave reference is not set in the Inspector.");
            }

            if (!isDoorOpen && grave.GetHasKey())
            {
                uiManager.ShowMessage("Press F to open the gate");
            }
            else if (!isDoorOpen && !grave.GetHasKey())
            {
                uiManager.ShowMessage("You need a key to open the gate");
            }
            else if (isDoorOpen)
            {
                uiManager.ShowMessage("Gate opened, exit to escape the zombies");
            }

            if (Input.GetKeyDown(KeyCode.F) && !isDoorOpen && grave.GetHasKey())
            {
                if (door == null)
                {
                    Debug.LogError("Door Animator reference is not set in the Inspector.");
                    return;
                }

                door.Play("exitOpen", 0, 0.0f);
                PlayGateSound(gateOpenSound);
                isDoorOpen = true;
            }
        }
    }

    private void PlayGateSound(AudioClip sound)
    {
        if (gateAudioSource == null)
        {
            Debug.LogError("Gate AudioSource reference is not set in the Inspector.");
            return;
        }

        if (sound == null)
        {
            Debug.LogError("Gate Open Sound reference is not set in the Inspector.");
            return;
        }

        gateAudioSource.PlayOneShot(sound); // Play the specified sound
    }
}
