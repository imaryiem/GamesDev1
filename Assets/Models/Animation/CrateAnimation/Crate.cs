using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private Animator myCrate = null;
    [SerializeField] private bool CrateTrigger = false;
    [SerializeField] private Renderer keyCardRenderer = null;

    private bool isOpen = false;
    private bool hasKey = false;

    public bool GetHasKey()
    {
        return hasKey;
    }

    private void Start()
    {

        Debug.Log(hasKey);
        Debug.Log(CrateTrigger);
        keyCardRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("testing"); Debug.Log(CrateTrigger);
        if (other.CompareTag("Player") && CrateTrigger)
        {
            if (isOpen && !hasKey)
            {
                Debug.Log("Press E to get the key");
            }
            else if (!isOpen)
            {
                Debug.Log("Press F to open the Crate");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && CrateTrigger)
        {
            Debug.Log("Player left the Crate proximity");
            if (isOpen)
            {
                CloseCrate();
            }
        }
    }

    private void Update()
    {
        if (CrateTrigger && Input.GetKeyDown(KeyCode.F) && !isOpen)
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
            keyCardRenderer.enabled = false; // Disable the key card renderer
            Debug.Log("You disabled the keycard!");
        }
    }
}