using UnityEngine;

class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClipLevelStart;
    public AudioClip audioClipLevelLoop;
    public static AudioSource AudioSource { get; private set; }

    private void Start()
    {
        audioSource.clip = audioClipLevelStart;
        audioSource.Play();
        AudioSource = audioSource;
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClipLevelLoop;
            audioSource.Play();
        }
        return;
    }
}

