using UnityEngine;
using TMPro;

public class thirdLevelUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText = null;
    [SerializeField] private Canvas messageCanvas = null;  // Reference to the Canvas
    [SerializeField] private Canvas victoryMessageCanvas = null;
    [SerializeField] private ParticleSystem victoryParticles = null; // Reference to the particle system

    private void Start()
    {
        HideCanvas();  // Hide the canvas by default
        HideVictoryMessage();  // Hide the victory canvas by default
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        ShowCanvas();
    }

    public void ClearMessage()
    {
        messageText.text = "";
        HideCanvas();
    }

    public void ShowVictoryMessage()
    {
        victoryMessageCanvas.enabled = true;
        if (victoryParticles != null)
            victoryParticles.Play(); // Play the particle system
    }

    public void HideVictoryMessage()
    {
        victoryMessageCanvas.enabled = false;
        if (victoryParticles != null)
            victoryParticles.Stop(); // Stop the particle system
    }

    private void ShowCanvas()
    {
        messageCanvas.enabled = true;  // Enable the canvas
    }

    private void HideCanvas()
    {
        messageCanvas.enabled = false;  // Disable the canvas
    }
}