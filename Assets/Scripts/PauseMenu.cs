using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseCanvas;
    private bool isPaused = false;

    public GameObject pausePanel;
    public GameObject settingsPanel;

    // public GameObject settingsCanvas;

    void Start()
    {
        Time.timeScale = 1f; // Ensure the game starts with normal time scale
        pauseCanvas.SetActive(false); // Hide the pause canvas by default

        settingsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
          //  TogglePause();
        //}
    }


    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            pauseCanvas.SetActive(true); // Show the pause canvas
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            pauseCanvas.SetActive(false); // Hide the pause canvas
        }
    }


    public void resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game
        pauseCanvas.SetActive(false); // Hide the pause canvas
    }

    public void settings()
    {
       // pauseCanvas.SetActive(false);
       // settingsCanvas.SetActive(true);
        
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void backToPauseMenu()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
