using UnityEngine;
using TMPro;
using System.Collections;

public class KeypadController : MonoBehaviour
{
    public TMP_Text displayText;  // Reference to the TMP_Text component displaying the input
    public string correctCode = "619";  // Correct code to open the door
    private string inputCode = "";

    public Door door;  // Reference to the Door script

    // Call this method when a number button is pressed
    public void OnNumberButtonPressed(string number)
    {
        inputCode += number;
        displayText.text = inputCode;
    }

    // Call this method when the Enter button is pressed
    public void OnEnterButtonPressed()
    {
        if (inputCode == correctCode)
        {
            door.OpenDoor();  // Call the method to open the door
            displayText.text = "Correct!";
            inputCode = "";  // Reset input code
        }
        else
        {
            StartCoroutine(ShowErrorMessage());
        }
    }

    private IEnumerator ShowErrorMessage()
    {
        displayText.text = "Error!";
        yield return new WaitForSeconds(3);  // Wait for 3 seconds
        inputCode = "";  // Reset input code
        displayText.text = "";  // Clear the display text
    }
}