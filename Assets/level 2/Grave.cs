using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    [SerializeField] private Animator myGrave = null;
    [SerializeField] private bool GraveTrigger = false;
    [SerializeField] private Renderer SecondLevelkeyRenderer = null;
    [SerializeField] private AudioSource doorAudioSource = null; 
    [SerializeField] private AudioClip graveOpenSound = null; 
    [SerializeField] private AudioClip graveCloseSound = null; 

    private bool isOpen = false;
    private bool hasKey = false;

    public bool GetHasKey()
    {
        return hasKey;
    }

    private void PlayGraveSound(AudioClip sound)
    {
        if (doorAudioSource && sound)
        {
            doorAudioSource.PlayOneShot(sound); // Play the specified sound
        }
    }
    private void Start()
    {
        Debug.Log(hasKey);
        SecondLevelkeyRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GraveTrigger)
        {
            if (isOpen && !hasKey)
            {
                Debug.Log("Press E to get the key");
            }
            else if (!isOpen)
            {
                Debug.Log("Press F to open the Grave");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && GraveTrigger)
        {
            Debug.Log("Player left the Grave proximity");
            if (isOpen)
            {
                GraveClose();
            }
        }
    }

    private void Update()
    {
        if (GraveTrigger && Input.GetKeyDown(KeyCode.F) && !isOpen)
        {
            GraveOpen();
        }

        if (isOpen && Input.GetKeyDown(KeyCode.E) && !hasKey)
        {
            DisableKeyCard();
        }
    }

    private void GraveOpen()
    {
        myGrave.Play("GraveOpen", 0, 0.0f);
        isOpen = true;
        SecondLevelkeyRenderer.enabled = true; // Enable the key card renderer
        PlayGraveSound(graveOpenSound);
    }

    private void GraveClose()
    {
        myGrave.Play("GraveClose", 0, 0.0f);
        isOpen = false;
        SecondLevelkeyRenderer.enabled = false; // Disable the key card renderer
        PlayGraveSound(graveCloseSound);
    }

    private void DisableKeyCard()
    {
        if (isOpen && !hasKey)
        {
            hasKey = true;
            SecondLevelkeyRenderer.gameObject.SetActive(false);// Disable the key card renderer
            Debug.Log("You disabled the keycard!");
        }
    }
}