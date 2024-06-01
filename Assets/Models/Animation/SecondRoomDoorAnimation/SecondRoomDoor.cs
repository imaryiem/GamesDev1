using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRoomDoor : MonoBehaviour
{
    [SerializeField] private Animator mySecondDoor = null;
    [SerializeField] private bool triggerOpen = false;
    [SerializeField] private bool triggerClose = false;

    private bool isOpening = false;
    private bool isClosing = false;

    [SerializeField] private Crate crate = null;

    private bool hasKey = false;

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
                    Debug.Log("Press F to open the second door");
                    isOpening = true;
                }
                else
                {
                    Debug.Log("You do not have the key");
                }
            }
            else if (triggerClose)
            {
                Debug.Log("Door will close automatically");
                isClosing = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
        }

        if (isClosing)
        {
            mySecondDoor.Play("SecondRoomDoorClose", 0, 0.0f);
            isClosing = false;
            gameObject.SetActive(false);
            Debug.Log(hasKey);
        }
    }
}