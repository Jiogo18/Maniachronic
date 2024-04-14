using UnityEngine;

class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClipLevelStart;
    public AudioClip audioClipLevelLoop;
    public static AudioSource AudioSource { get; private set; }
    private static bool canPlay = true;

    private static AudioClip previousAudioClip;
    private static int previousMusicTime;

    public AudioClip audioClipStart;

    private void Start()
    {
        AudioSource = audioSource;
        audioSource.clip = audioClipLevelStart;

        if (previousMusicTime > 0 && previousAudioClip == audioClipLevelStart)
        {
            // Play from the saved time
            audioSource.timeSamples = previousMusicTime;
            previousMusicTime = 0;
            previousAudioClip = null;
        }

        audioSource.Play();
    }

    private void Update()
    {
        if (!canPlay) return;
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClipLevelLoop;
            audioSource.Play();
        }
        return;
    }

    public static void StopByDeath()
    {
        canPlay = false;
        AudioSource.Stop();
    }

    public static void SaveMusicTime()
    {
        if (AudioSource != null)
        {
            previousAudioClip = AudioSource.clip;
            previousMusicTime = AudioSource.timeSamples;
        }
    }
}

