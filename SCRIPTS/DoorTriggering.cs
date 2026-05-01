using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggering : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;  // Αναφορά στον Animator του φωτιστικού
    [SerializeField] private bool openTrigger = false;  // Σημαία για άνοιγμα της πόρτας
    [SerializeField] private bool closeTrigger = false;  // Σημαία για κλείσιμο της πόρτας
    [SerializeField] private AudioClip openSound = null;   // Ήχος για το άνοιγμα
    [SerializeField] private AudioClip closeSound = null;  // Ήχος για το κλείσιμο
    [SerializeField] private AudioSource audioSource = null; // Συστατικό AudioSource

    // Κλήση όταν ο παίκτης μπαίνει στην περιοχή ενεργοποίησης
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(openTrigger)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);  // Παίζει την animation για το άνοιγμα της πόρτας
                PlaySound(openSound);  // Αναπαράγει τον ήχο του ανοίγματος
                gameObject.SetActive(false);  // Απενεργοποιεί το GameObject (πιθανώς την πόρτα)
            }
            else if(closeTrigger)
            {
                myDoor.Play("DoorClose", 0, 0.0f);  // Παίζει την animation για το κλείσιμο της πόρτας
                PlaySound(closeSound); // Αναπαράγει τον ήχο του κλεισίματος
                gameObject.SetActive(false);  // Απενεργοποιεί το GameObject (πιθανώς την πόρτα)
            }
        }
    }

    // Μέθοδος για την αναπαραγωγή ήχου
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);  // Παίζει τον ήχο
        }
    }
}
