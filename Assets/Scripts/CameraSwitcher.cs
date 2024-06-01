using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstCamera;
    public Camera secondCamera;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCameras();
        }
    }

    private void SwitchCameras()
    {
        if (firstCamera.enabled)
        {
            firstCamera.enabled = false;
            secondCamera.enabled = true;
        }
        else if (secondCamera.enabled)
        {
            secondCamera.enabled = false;
            firstCamera.enabled = true;
        }
    }


    private void Start()
    {
        secondCamera.enabled = false; // Disable the second camera on start
    }
}