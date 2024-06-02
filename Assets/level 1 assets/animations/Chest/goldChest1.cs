using UnityEngine;

public class goldChest1 : MonoBehaviour
{
    [SerializeField] private Animator myChest = null;
    [SerializeField] private bool ChestTrigger = false;
    [SerializeField] private Renderer chestKey = null;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private AudioSource chestAudioSource = null; // Reference to the AudioSource for chest sound
    [SerializeField] private AudioClip chestOpenSound = null; // Sound clip for chest opening
    [SerializeField] private AudioClip chestCloseSound = null; // Sound clip for chest closing

    private bool playerInTrigger = false;
    private bool isChestOpen = false;
    private bool isKeyCollected = false;

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
        }
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            if (isChestOpen)
            {
                uiManager.ShowMessage("Press F to close the chest");
            }
            else
            {
                uiManager.ShowMessage("Press F to open the chest");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isChestOpen)
                {
                    myChest.Play("ChestClose", 0, 0.0f);
                    isChestOpen = false;
                    uiManager.ShowMessage("Chest closed");
                    PlayChestSound(chestCloseSound); // Play chest closing sound
                }
                else
                {
                    myChest.Play("ChestOpen", 0, 0.0f);
                    isChestOpen = true;
                    uiManager.ShowMessage("Chest opened");
                    PlayChestSound(chestOpenSound); // Play chest opening sound
                }
            }

            if (isChestOpen && !isKeyCollected)
            {
                uiManager.ShowMessage("Press E to collect the key");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Collect the key
                    chestKey.enabled = false;
                    isKeyCollected = true;
                    uiManager.ShowMessage("Key collected!");
                }
            }
        }
    }

    private void PlayChestSound(AudioClip sound)
    {
        if (chestAudioSource && sound)
        {
            chestAudioSource.PlayOneShot(sound); // Play the specified sound
        }
    }

    public bool getKey()
    {
        return isKeyCollected;
    }
}
