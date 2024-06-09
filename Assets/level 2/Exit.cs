using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private Animator myExit = null;
    [SerializeField] private bool triggerOpen = false;
    [SerializeField] private bool triggerClose = false;

    private bool isOpening = false;
    private bool isClosing = false;

    [SerializeField] private Grave Grave = null;
    //[SerializeField] private thirdLevelUIManager uiManager = null; // Reference to the ThirdLevelUIManager

    private bool hasKey = false;

    private void Start()
    {
        hasKey = Grave.GetHasKey();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerOpen)
            {
                if (hasKey)
                {
                    // uiManager.ShowMessage("Press F to open the second door"); // Use ThirdLevelUIManager to display message
                    isOpening = true;
                }
                else
                {
                    // uiManager.ShowMessage("You do not have the key"); // Use ThirdLevelUIManager to display message
                }
            }
            else if (triggerClose)
            {
                // Debug.Log("Door will close automatically");
                isClosing = true;
            }
            SceneManager.LoadScene(3); 
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // uiManager.ClearMessage();  // Clear the message when the player leaves

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
        hasKey = Grave.GetHasKey(); // Retrieve the hasKey value each frame

        if (isOpening && Input.GetKeyDown(KeyCode.F) && hasKey)
        {
            myExit.Play("exitOpen", 0, 0.0f);
            isOpening = false;
        }

        if (isClosing)
        {
            myExit.Play("exitClose", 0, 0.0f);
            isClosing = false;
            gameObject.SetActive(false);
        }
    }
}