using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSoundAndEnableCollidersOnTrigger : MonoBehaviour
{
    public AudioSource targetAudioSource;     // Αναφορά στο AudioSource που θα απενεργοποιηθεί
    public GameObject player;                 // Αναφορά στο GameObject του παίκτη
    public GameObject objectWithCollider1;    // GameObject που περιέχει το πρώτο BoxCollider
    public GameObject objectWithCollider2;    // GameObject που περιέχει το δεύτερο BoxCollider

    private BoxCollider collider1;            // Αναφορά στο πρώτο BoxCollider
    private BoxCollider collider2;            // Αναφορά στο δεύτερο BoxCollider

    private void Start()
    {
        // Έλεγχος αν το AudioSource ή ο παίκτης δεν έχουν ανατεθεί
        if (targetAudioSource == null)
        {
            Debug.LogError("TargetAudioSource is not assigned."); // Καταγραφή σφάλματος
        }

        if (player == null)
        {
            Debug.LogError("Player is not assigned."); // Καταγραφή σφάλματος
        }

        // Λήψη των BoxCollider από τα παρεχόμενα GameObjects
        if (objectWithCollider1 != null)
        {
            collider1 = objectWithCollider1.GetComponent<BoxCollider>(); // Λήψη του πρώτου BoxCollider
            if (collider1 == null)
            {
                Debug.LogError("ObjectWithCollider1 does not have a BoxCollider component."); // Καταγραφή σφάλματος
            }
        }
        else
        {
            Debug.LogError("ObjectWithCollider1 is not assigned."); // Καταγραφή σφάλματος
        }

        if (objectWithCollider2 != null)
        {
            collider2 = objectWithCollider2.GetComponent<BoxCollider>(); // Λήψη του δεύτερου BoxCollider
            if (collider2 == null)
            {
                Debug.LogError("ObjectWithCollider2 does not have a BoxCollider component."); // Καταγραφή σφάλματος
            }
        }
        else
        {
            Debug.LogError("ObjectWithCollider2 is not assigned."); // Καταγραφή σφάλματος
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Έλεγχος αν το trigger είναι με το GameObject του παίκτη
        if (other.gameObject == player)
        {
            // Απενεργοποίηση του AudioSource αν έχει ανατεθεί
            if (targetAudioSource != null)
            {
                targetAudioSource.enabled = false; // Απενεργοποίηση του AudioSource
                Debug.Log("Target AudioSource has been disabled."); // Καταγραφή της ενέργειας
            }

            // Ενεργοποίηση του πρώτου BoxCollider αν έχει ανατεθεί
            if (collider1 != null)
            {
                collider1.enabled = true; // Ενεργοποίηση του πρώτου BoxCollider
                Debug.Log("BoxCollider 1 has been enabled."); // Καταγραφή της ενέργειας
            }

            // Ενεργοποίηση του δεύτερου BoxCollider αν έχει ανατεθεί
            if (collider2 != null)
            {
                collider2.enabled = true; // Ενεργοποίηση του δεύτερου BoxCollider
                Debug.Log("BoxCollider 2 has been enabled."); // Καταγραφή της ενέργειας
            }

            // Απενεργοποίηση του GameObject στο οποίο είναι συνδεδεμένος αυτός ο κώδικας
            gameObject.SetActive(false); // Απενεργοποίηση του GameObject
            Debug.Log("GameObject has been disabled."); // Καταγραφή της ενέργειας
        }
    }
}
