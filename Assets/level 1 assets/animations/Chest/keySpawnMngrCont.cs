using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawnManager : MonoBehaviour
{
    public GameObject keyPrefab; // Reference to the key prefab
    public Transform[] spawnPoints; // Array to hold spawn points
    private GameObject spawnedKey;

    void Start()
    {
        // Call a method to spawn keys when the game starts
        SpawnKeys();
    }

    void SpawnKeys()
    {
        // Check if there are spawn points and key prefab assigned
        if (spawnPoints.Length > 0 && keyPrefab != null)
        {
            // Choose a random spawn point index
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Debug.Log("Random index chosen: " + randomIndex);

            // Get the chosen spawn point's position
            Vector3 spawnPosition = spawnPoints[randomIndex].position;

            // Spawn the key at the chosen spawn point
            spawnedKey = Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Key spawned at: " + spawnPosition);
        }
        else
        {
            Debug.LogError("No spawn points or key prefab assigned to the KeySpawnManager!");
        }
    }

    public GameObject GetSpawnedKey()
    {
        return spawnedKey;
    }
}
