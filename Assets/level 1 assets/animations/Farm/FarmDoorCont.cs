using UnityEngine;

public class FarmDoorCont : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
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
        }
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            if (isDoorOpen)
            {
                Debug.Log("Press F to close the farm door");
            }
            else
            {
                Debug.Log("Press F to open the farm door");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isDoorOpen)
                {
                    myDoor.Play("FarmClose", 0, 0.0f);
                    isDoorOpen = false;
                    PlayDoorSound(doorCloseSound); // Play door closing sound
                }
                else
                {
                    myDoor.Play("FarmOpen", 0, 0.0f);
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
