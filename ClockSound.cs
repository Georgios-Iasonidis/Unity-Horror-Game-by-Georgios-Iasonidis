using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    void Start()
    {
        // Find the AudioSource component on the child if not assigned in the Inspector
        if (audioSource == null)
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }

        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not assigned.");
        }
    }
}
