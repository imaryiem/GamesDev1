using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parkourInstructions : MonoBehaviour
{
    [SerializeField] private Canvas parkourCanvas = null;
    private bool isParkourTriggered = false;

    private void Start()
    {
        HideCanvas(parkourCanvas);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isParkourTriggered = true;
            ShowCanvas(parkourCanvas);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isParkourTriggered = false;
            HideCanvas(parkourCanvas);
        }
    }

    private void Update()
    {
        if (!isParkourTriggered)
            HideCanvas(parkourCanvas);
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
