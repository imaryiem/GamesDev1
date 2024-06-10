using UnityEngine;

public class mazeInstruction : MonoBehaviour
{
    [SerializeField] private Canvas mazeCanvas = null;
    private bool isMazeTriggered = false;

    private void Start()
    {
        HideCanvas(mazeCanvas);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMazeTriggered = true;
            ShowCanvas(mazeCanvas);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMazeTriggered = false;
            HideCanvas(mazeCanvas);
        }
    }

    private void Update()
    {
        if (!isMazeTriggered)
            HideCanvas(mazeCanvas);
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