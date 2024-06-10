using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondRoomInstruction : MonoBehaviour
{
    [SerializeField] private Canvas secondRoomInstructionCanvas = null;
    private bool isSecondRoomTriggered = false;

    private void Start()
    {
        HideCanvas(secondRoomInstructionCanvas);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSecondRoomTriggered = true;
            ShowCanvas(secondRoomInstructionCanvas);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSecondRoomTriggered = false;
            HideCanvas(secondRoomInstructionCanvas);
        }
    }

    private void Update()
    {
        if (!isSecondRoomTriggered)
            HideCanvas(secondRoomInstructionCanvas);
    }

    private void ShowCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
    }

    private void HideCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
    }
}
