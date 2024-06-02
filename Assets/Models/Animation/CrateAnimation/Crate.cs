using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private Animator myCrate = null;
    [SerializeField] private bool CrateTrigger = false;
    [SerializeField] private Renderer keyCardRenderer = null;
    [SerializeField] private thirdLevelUIManager uiManager = null; // Reference to the UI Manager

    private bool test = false;
    private bool isOpen = false;
    private bool hasKey = false;

    public bool GetHasKey()
    {
        return hasKey;
    }

    private void Start()
    {
        keyCardRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CrateTrigger)
        {
            test = true;
            if (isOpen && !hasKey)
            {
                uiManager.ShowMessage("Press E to get the key");
            }
            else if (!isOpen)
            {
                uiManager.ShowMessage("Press F to open the Crate");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && CrateTrigger)
        {
            test = false;
            uiManager.ClearMessage();  // Clear the message when the player leaves
            if (isOpen)
            {
                CloseCrate();
            }
        }
    }

    private void Update()
    {
        if (test && CrateTrigger && Input.GetKeyDown(KeyCode.F) && !isOpen)
        {
            OpenCrate();
        }

        if (isOpen && Input.GetKeyDown(KeyCode.E) && !hasKey)
        {
            DisableKeyCard();
        }
    }

    private void OpenCrate()
    {
        myCrate.Play("openCrate", 0, 0.0f);
        isOpen = true;
        keyCardRenderer.enabled = true; // Enable the key card renderer
        uiManager.ShowMessage("Press E to get the key");
    }

    private void CloseCrate()
    {
        myCrate.Play("closeCrate", 0, 0.0f);
        isOpen = false;
        keyCardRenderer.enabled = false; // Disable the key card renderer
    }

    private void DisableKeyCard()
    {
        if (isOpen && !hasKey)
        {
            hasKey = true;
            keyCardRenderer.gameObject.SetActive(false); // Disable the key card renderer
            uiManager.ClearMessage();  // Clear the message once the key is taken
        }
    }
}
