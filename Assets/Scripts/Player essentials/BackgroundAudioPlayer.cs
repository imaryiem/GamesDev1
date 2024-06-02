using UnityEngine;

public class BackgroundAudioPlayer : MonoBehaviour
{
    public int audioToPlayIndex = 0;
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        switch (audioToPlayIndex)
        {
            case 1:
                audioSource.loop = true;
                audioSource.clip = audioClips[0];
                audioSource.volume = 0.1f;
                audioSource.Play();
                break;
            case 2:
                audioSource.loop = true;
                audioSource.clip = audioClips[1];
                audioSource.volume = 0.1f;
                audioSource.Play();
                break;
            default:
                break;
        }
    }
}
