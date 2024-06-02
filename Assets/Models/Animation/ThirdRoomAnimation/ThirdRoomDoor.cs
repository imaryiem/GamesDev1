using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdRoomDoor : MonoBehaviour
{
    [SerializeField] private Animator thirdDoor = null;
    [SerializeField] private bool thirdDoorOpenTrigger = false;
    [SerializeField] private bool thirdDoorCloseTrigger = false;

    [SerializeField] private thirdLevelUIManager uiManager = null; // Reference to the UI Manager

    private bool isOpening = false;
    private bool isClosing = false;

    private bool hasKey = false;

    [SerializeField] private KeyCard key = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (thirdDoorOpenTrigger)
            {
                if (hasKey)
                {
                    //uiManager.ShowMessage("door is opening");
                    isOpening = true;
                }
                else
                {
                    uiManager.ShowMessage("door is locked, please get the key");
                }
            }
            else if (thirdDoorCloseTrigger)
            {
                // Debug.Log("door is closing");
                isClosing = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            uiManager.ClearMessage();  // Clear the message when the player leaves

            if (thirdDoorOpenTrigger)
            {
                isOpening = false;
            }
            else if (thirdDoorCloseTrigger)
            {
                isClosing = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        hasKey = key != null && key.GetHasBeenCollected();

        if (isOpening && hasKey)
        {
            thirdDoor.Play("ThirdRoomDoorOpen");
            isOpening = false;
        }

        if (isClosing)
        {
            // Play the door close animation
            thirdDoor.Play("ThirdRoomDoorClose");
            isClosing = false;
            gameObject.SetActive(false);
        }
    }
}