using UnityEngine;

public class goldChest2 : MonoBehaviour
{
    [SerializeField] private Animator myChest = null;
    [SerializeField] private bool Chest2rigger = false;
    [SerializeField] private Renderer chest2Key = null;
    [SerializeField] private goldChest1 chest1 = null;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private AudioSource chestAudioSource = null; // Reference to the AudioSource for chest sound
    [SerializeField] private AudioClip chestOpenSound = null; // Sound clip for chest opening
    [SerializeField] private AudioClip chestCloseSound = null; // Sound clip for chest closing

    private bool playerInTrigger = false;
    private bool isChestOpen = false;
    private bool isKey2Collected = false;

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
            else if (!isChestOpen && chest1.getKey())
            {
                uiManager.ShowMessage("Press F to unlock the chest");
            }
            else if (!isChestOpen && chest2Key.enabled == false)
            {
                uiManager.ShowMessage("Press F to open the chest");
            }
            else if (!isChestOpen && !chest1.getKey())
            {
                uiManager.ShowMessage("Search for key number one to open this chest");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!isChestOpen && chest1.getKey())
                {
                    myChest.Play("ChestOpen", 0, 0.0f);
                    isChestOpen = true;
                    PlayChestSound(chestOpenSound); // Play chest opening sound
                }
                else if (!isChestOpen && chest2Key.enabled == false)
                {
                    myChest.Play("ChestOpen", 0, 0.0f);
                    isChestOpen = true;
                    PlayChestSound(chestOpenSound); // Play chest opening sound
                }
                else if (isChestOpen)
                {
                    myChest.Play("ChestClose", 0, 0.0f);
                    isChestOpen = false;
                    PlayChestSound(chestCloseSound); // Play chest closing sound
                }
            }

            if (isChestOpen && !isKey2Collected)
            {
                uiManager.ShowMessage("Press E to collect the key");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    chest2Key.enabled = false;
                    isKey2Collected = true;
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
        return isKey2Collected;
    }
}
