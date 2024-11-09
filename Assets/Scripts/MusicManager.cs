using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the Audio Source component

    void Start()
    {
        if (audioSource != null)
        {
            //audioSource.Play(); // Play the music at the start
        }
    }

    // Optionally, you can add methods to control music playback
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void PauseMusic()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }
}
