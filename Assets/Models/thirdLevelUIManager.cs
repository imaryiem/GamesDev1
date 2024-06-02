using UnityEngine;
using TMPro;

public class thirdLevelUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText = null;
    [SerializeField] private Canvas messageCanvas = null;  // Reference to the Canvas

    private void Start()
    {
        HideCanvas();  // Hide the canvas by default
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

    private void ShowCanvas()
    {
        messageCanvas.enabled = true;  // Enable the canvas
    }

    private void HideCanvas()
    {
        messageCanvas.enabled = false;  // Disable the canvas
    }
}
