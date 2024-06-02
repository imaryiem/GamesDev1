using UnityEngine;

public class HouseDoorCont : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private AudioSource doorAudioSource = null; // Reference to the AudioSource for door sound
    [SerializeField] private AudioClip doorOpenSound = null; // Sound clip for door opening
    [SerializeField] private AudioClip doorCloseSound = null; // Sound clip for door closing

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
            if (isDoorOpen)
            {
                uiManager.ShowMessage("Press F to close the door");
            }
            else
            {
                uiManager.ShowMessage("Press F to open the door");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isDoorOpen)
                {
                    myDoor.Play("HouseDoor1Close", 0, 0.0f);
                    isDoorOpen = false;
                    PlayDoorSound(doorCloseSound); // Play door closing sound
                }
                else
                {
                    myDoor.Play("HouseDoor1Open", 0, 0.0f);
                    isDoorOpen = true;
                    PlayDoorSound(doorOpenSound); // Play door opening sound
                }
            }
        }
    }

    private void PlayDoorSound(AudioClip sound)
    {
        if (doorAudioSource && sound)
        {
            doorAudioSource.PlayOneShot(sound); // Play the specified sound
        }
    }
}
