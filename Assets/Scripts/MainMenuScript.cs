using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }


    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadFirstLevelScene()
    {
        SceneManager.LoadScene("FirstLevel");
    }
}
