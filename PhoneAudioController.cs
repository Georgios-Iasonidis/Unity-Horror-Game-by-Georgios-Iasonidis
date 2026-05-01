using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneAudioController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip soundEffect;   // Sound to play on trigger
    public GameObject player;       // Reference to the player GameObject
    public GameObject objectToEnable; // The object to enable after the audio finishes

    private bool hasPlayed = false; // Flag to ensure sound is only played once

    private void Start()
    {
        // Check if AudioSource or soundEffect is not assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned.");
        }

        if (soundEffect == null)
        {
            Debug.LogError("SoundEffect is not assigned.");
        }

        if (player == null)
        {
            Debug.LogError("Player is not assigned.");
        }

        if (objectToEnable == null)
        {
            Debug.LogError("Object to enable is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger is with the player object and if the sound hasn't been played yet
        if (other.gameObject == player && !hasPlayed)
        {
            // Play the sound effect
            if (audioSource != null && soundEffect != null)
            {
                audioSource.PlayOneShot(soundEffect);
                Debug.Log("Sound played.");
                hasPlayed = true; // Set the flag to true to prevent the sound from playing again
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player has exited the trigger
        if (other.gameObject == player && hasPlayed)
        {
            // Start coroutine to disable the object after the audio has finished playing
            StartCoroutine(DisableAfterAudio());
        }
    }

    private IEnumerator DisableAfterAudio()
    {
        // Wait until the audio has finished playing
        if (audioSource != null)
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        // Disable the trigger object
        gameObject.SetActive(false);
        Debug.Log("Trigger object disabled.");

        // Enable the specified object
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
            Debug.Log("Object enabled.");
        }
    }
}
