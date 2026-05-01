using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareATtheHALL : MonoBehaviour
{
    public GameObject JumpscareTrigger; // Το αντικείμενο που ενεργοποιείται όταν εισέρχεται ο παίκτης στο trigger

    private void Start()
    {
        // Έλεγχος αν το αντικείμενο του trigger έχει ανατεθεί
        if (JumpscareTrigger == null)
        {
            Debug.LogError("Jumpscare trigger object is not assigned."); // Καταγραφή σφάλματος αν δεν έχει ανατεθεί το αντικείμενο του trigger
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Έλεγχος αν ο παίκτης εισέρχεται στο trigger
        if (other.CompareTag("Player"))
        {
            if (JumpscareTrigger != null)
            {
                // Ενεργοποίηση του αντικειμένου 
                JumpscareTrigger.SetActive(true);
                Debug.Log("Jumpscare trigger object enabled."); // Καταγραφή της ενεργοποίησης του αντικειμένου 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Έλεγχος αν ο παίκτης εξέρχεται από το trigger
        if (other.CompareTag("Player"))
        {
            // Απενεργοποίηση του αντικειμένου στο οποίο είναι συνδεδεμένος αυτός ο κώδικας
            gameObject.SetActive(false);
            Debug.Log("Trigger object disabled after player exits."); // Καταγραφή της απενεργοποίησης του αντικειμένου
        }
    }
}
