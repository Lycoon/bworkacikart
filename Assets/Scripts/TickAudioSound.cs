using UnityEngine;

public class TickAudioSound : MonoBehaviour
{
    public AudioSource tickAudioSource;

    public void PlayTickSound()
    {
        tickAudioSource.Play();
    }
}
