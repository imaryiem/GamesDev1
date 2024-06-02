using UnityEngine;

public class LoopSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure the audio source is set to loop
        audioSource.loop = true;

        // Play the audio source
        audioSource.Play();
    }
}
