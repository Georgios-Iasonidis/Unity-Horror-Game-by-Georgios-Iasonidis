using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotePickUP : MonoBehaviour
{
    [SerializeField] private Image _notImage; // Εικόνα για την σημείωση
    [SerializeField] private TMP_Text _notText; // Κείμενο για την σημείωση
    [SerializeField] private AudioClip pickupSound; // Ήχος για την παραλαβή της σημείωσης
    [SerializeField] private AudioSource audioSource = null; // Συστατικό AudioSource

    void OnTriggerEnter(Collider other)
    {
        // Όταν ο παίκτης εισέρχεται στην περιοχή του collider
        if (other.CompareTag("Player"))
        {
            _notImage.enabled = true; // Ενεργοποίηση της εικόνας της σημείωσης
            _notText.enabled = true; // Ενεργοποίηση του κειμένου της σημείωσης
            PlaySound(pickupSound); // Αναπαραγωγή του ήχου παραλαβής
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Όταν ο παίκτης εξέρχεται από την περιοχή του collider
        if (other.CompareTag("Player"))
        {
            _notImage.enabled = false; // Απενεργοποίηση της εικόνας της σημείωσης
            _notText.enabled = false; // Απενεργοποίηση του κειμένου της σημείωσης
        }
    }

    private void PlaySound(AudioClip clip)
    {
        // Μέθοδος για την αναπαραγωγή ήχου
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip); // Αναπαραγωγή του ήχου
        }
    }
}
