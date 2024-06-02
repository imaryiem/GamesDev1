using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private Animator rightDoorAnimator = null;
    [SerializeField] private Animator leftDoorAnimator = null;
    [SerializeField] private goldChest3 chest3 = null;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private AudioSource gateAudioSource = null; // Reference to the AudioSource for gate sound
    [SerializeField] private AudioClip gateOpenSound = null; // Sound clip for gate opening

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
            if (!isDoorOpen && chest3.getKey())
            {
                uiManager.ShowMessage("Press F to open the gate");
            }
            if(!isDoorOpen && !chest3.getKey())
            {
                uiManager.ShowMessage("You need key number 3 to open the gate and escape");
            }
            else
            {
                uiManager.ShowMessage("Gate opened, Time To Escape!");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!isDoorOpen && chest3.getKey())
                {
                    rightDoorAnimator.Play("RightDoorOpen", 0, 0.0f);
                    leftDoorAnimator.Play("LeftDoorOpen", 0, 0.0f);
                    isDoorOpen = true;
                    PlayGateSound(gateOpenSound); // Play gate opening sound
                }
            }
        }
    }

    private void PlayGateSound(AudioClip sound)
    {
        if (gateAudioSource && sound)
        {
            gateAudioSource.PlayOneShot(sound); // Play the specified sound
        }
    }
}
