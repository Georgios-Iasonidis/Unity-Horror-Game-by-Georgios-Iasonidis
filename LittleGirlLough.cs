using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlJumpscare : MonoBehaviour
{
    public AudioSource audioSource;    // Αναφορά στο AudioSource που θα αναπαράγει τον ήχο
    public AudioClip soundEffect;      // Ο ήχος που θα αναπαραχθεί κατά την ενεργοποίηση του trigger
    public GameObject player;          // Αναφορά στο GameObject του παίκτη

    private bool soundTriggered = false; // Σημαία για να εξασφαλιστεί ότι ο ήχος θα αναπαραχθεί μόνο μία φορά

    private void Start()
    {
        // Έλεγχος αν οι αναφορές δεν έχουν ανατεθεί
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned."); // Καταγραφή σφάλματος αν δεν έχει ανατεθεί το AudioSource
        }

        if (soundEffect == null)
        {
            Debug.LogError("SoundEffect is not assigned."); // Καταγραφή σφάλματος αν δεν έχει ανατεθεί ο ήχος
        }

        if (player == null)
        {
            Debug.LogError("Player is not assigned."); // Καταγραφή σφάλματος αν δεν έχει ανατεθεί ο παίκτης
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Έλεγχος αν το trigger είναι με το αντικείμενο του παίκτη και αν ο ήχος δεν έχει αναπαραχθεί ακόμη
        if (other.gameObject == player && !soundTriggered)
        {
            // Αναπαραγωγή του ηχητικού εφέ
            if (audioSource != null && soundEffect != null)
            {
                audioSource.PlayOneShot(soundEffect); // Αναπαραγωγή του ηχητικού εφέ
                Debug.Log("Sound activated."); // Καταγραφή της ενεργοποίησης του ήχου
                soundTriggered = true; // Θέτει τη σημαία σε true για να αποτραπεί η επανάληψη του ήχου

                // Απενεργοποίηση του αντικειμένου μετά την αναπαραγωγή του ήχου
                StartCoroutine(DisableAfterSound());
            }
        }
    }

    private IEnumerator DisableAfterSound()
    {
        // Αναμονή μέχρι να τελειώσει η αναπαραγωγή του ήχου
        if (audioSource != null)
        {
            yield return new WaitWhile(() => audioSource.isPlaying); // Περιμένετε όσο ο ήχος αναπαράγεται
        }

        // Απενεργοποίηση αυτού του GameObject
        gameObject.SetActive(false); // Απενεργοποίηση του GameObject
        Debug.Log("Game object disabled after sound."); // Καταγραφή της απενεργοποίησης του αντικειμένου
    }
}
