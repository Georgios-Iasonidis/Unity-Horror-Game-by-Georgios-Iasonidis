using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockDoorTrigger : MonoBehaviour
{
    public AudioClip knockSound;  // The sound to play
    private AudioSource audioSource;  // AudioSource component to play the sound

    void Start()
    {
        // Get or add an AudioSource component if one does not exist
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the sound is assigned
        if (knockSound != null)
        {
            audioSource.clip = knockSound;
            audioSource.Play();
            // Start a coroutine to disable the object after the sound finishes
            StartCoroutine(DisableAfterSound());
        }
    }

    private IEnumerator DisableAfterSound()
    {
        // Wait for the sound to finish playing
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // Disable the GameObject
        gameObject.SetActive(false);
    }
}
