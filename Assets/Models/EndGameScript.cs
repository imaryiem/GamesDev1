using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    [SerializeField] private bool EndGameTrigger = false;
    public GameObject endGameCanvas; // Reference to the EndGameCanvas GameObject

    private void Start()
    {
        // Disable the endGameCanvas at the start
        endGameCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndGameTrigger = true;
            endGameCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndGameTrigger = false;
            endGameCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (endGameCanvas.activeSelf && Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(0);
        }
    }
}