using UnityEngine;

public class AnimationSoundHandler : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource untuk memutar suara

    // Metode untuk dipanggil dari Animation Event
    public void PlaySound(AudioClip soundClip)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is not assigned!");
            return;
        }

        if (soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
            Debug.Log($"Playing sound: {soundClip.name}");
        }
        else
        {
            Debug.LogWarning("No AudioClip assigned to the Animation Event!");
        }
    }
}
