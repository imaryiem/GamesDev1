using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Animator myChest = null;
    [SerializeField] private KeySpawnManager keySpawnManager; // Reference to the KeySpawnManager
    private GameObject spawnedKey; // Reference to the key near this chest

    private bool playerInTrigger = false;
    private bool isChestOpen = false;
    private bool isKeyCollected = false;

    private void Start()
    {
        if (keySpawnManager == null)
        {
            keySpawnManager = FindObjectOfType<KeySpawnManager>();
            if (keySpawnManager == null)
            {
                Debug.LogError("KeySpawnManager not found in the scene. Please assign it in the Inspector.");
                return;
            }
        }

        StartCoroutine(AssignSpawnedKey());
    }

    private IEnumerator AssignSpawnedKey()
    {
        yield return new WaitForSeconds(0.1f); // Small delay to ensure key is spawned
        spawnedKey = keySpawnManager.GetSpawnedKey();
        if (spawnedKey == null)
        {
            Debug.LogError("No key spawned by KeySpawnManager.");
        }
    }

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
            if (isChestOpen)
            {
                Debug.Log("Press F to close the chest");
            }
            else
            {
                Debug.Log("Press F to open the chest");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isChestOpen)
                {
                    myChest.Play("ChestClose", 0, 0.0f);
                    isChestOpen = false;
                }
                else
                {
                    myChest.Play("ChestOpen", 0, 0.0f);
                    isChestOpen = true;
                }
            }

            if (isChestOpen && !isKeyCollected && spawnedKey != null)
            {
                Debug.Log("Press E to collect the key");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Collect the key
                    isKeyCollected = true;
                    spawnedKey.SetActive(false); // or Destroy(spawnedKey);
                    Debug.Log("Key collected!");
                }
            }
        }
    }
}
