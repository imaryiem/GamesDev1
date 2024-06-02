using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText = null;

    public void ShowMessage(string message)
    {
        messageText.text = message;
    }
}
