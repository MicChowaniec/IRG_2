using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the Audio Source component
    public AudioClip[] musicTracks;  // Array of music tracks
    private int currentTrackIndex = 0; // Index of the current track being played

    void Start()
    {
        if (audioSource != null && musicTracks.Length > 0)
        {
            PlayCurrentTrack(); // Play the first track at the start
        }
    }

    // Play the current track based on the index
    private void PlayCurrentTrack()
    {
        if (musicTracks.Length == 0) return; // Exit if no tracks are provided

        audioSource.clip = musicTracks[currentTrackIndex];
        audioSource.Play();

        // Automatically play the next song when the current one ends
        audioSource.loop = false; // Ensure looping is disabled for individual clips
        audioSource.PlayScheduled(AudioSettings.dspTime + audioSource.clip.length);

        // Check for the end of the clip and schedule the next track
        Invoke(nameof(PlayNextTrack), audioSource.clip.length);
    }

    // Play the next track in the array
    private void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length; // Move to the next track and loop back
        PlayCurrentTrack(); // Play the next track
    }

    // Stop the music playback
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            CancelInvoke(nameof(PlayNextTrack)); // Stop automatic switching to next track
        }
    }

    // Pause the music playback
    public void PauseMusic()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
            CancelInvoke(nameof(PlayNextTrack)); // Pause automatic switching
        }
    }

    // Resume the music playback
    public void ResumeMusic()
    {
        if (audioSource != null)
        {
            audioSource.UnPause();
            Invoke(nameof(PlayNextTrack), audioSource.clip.length - audioSource.time); // Schedule the next track after resuming
        }
    }
}
