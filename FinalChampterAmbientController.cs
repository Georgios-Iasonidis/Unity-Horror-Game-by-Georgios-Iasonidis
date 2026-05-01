using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChapterAmbientController : MonoBehaviour
{
    // Reference to the audio source you want to fade out
    public AudioSource audioSource;
    
    // Reference to the object you want to enable
    public GameObject objectToEnable;

    // Duration of the fade-out effect
    public float fadeDuration = 2.0f;

    // This will store whether the fading process has started
    private bool hasFaded = false;

    // Update is called once per frame
    void Update()
    {
        // Optional: You can put any other update logic here
    }

    // This method is called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player (you can use tags or other conditions)
        if (other.CompareTag("Player") && !hasFaded)
        {
            hasFaded = true; // Ensure this only happens once
            StartCoroutine(FadeOutAudio());
        }
    }

    // Coroutine to smoothly decrease the audio volume
    IEnumerator FadeOutAudio()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Ensure the volume is set to 0
        audioSource.volume = 0;
        audioSource.Stop();

        // Enable the other object
        objectToEnable.SetActive(true);

        // Disable the GameObject this script is attached to
        gameObject.SetActive(false);
    }
}
