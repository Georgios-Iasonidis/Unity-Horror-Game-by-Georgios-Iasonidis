using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip soundEffect;   // Sound to play on trigger
    public GameObject player;       // Reference to the player GameObject

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
}
