using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTheMonster1 : MonoBehaviour
{
    public GameObject monster;                // Το αντικείμενο τέρας για ενεργοποίηση
    public AudioSource jumpscareSound;        // Η πηγή ήχου για τον ήχο τρομακτικής σκηνής
    public GameObject objectToEnableOnEnter;  // Το αντικείμενο που θα ενεργοποιηθεί όταν ο παίκτης εισέλθει στο trigger

    private void OnTriggerEnter(Collider other)
    {
        // Έλεγχος αν το αντικείμενο που εισήλθε στο trigger είναι ο παίκτης
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");

            // Ενεργοποίηση του τέρατος
            if (monster != null)
            {
                monster.SetActive(true);
                Debug.Log("Monster enabled");
            }
            else
            {
                Debug.LogWarning("Monster reference is null");
            }

            // Αναπαραγωγή του ήχου τρομακτικής σκηνής
            if (jumpscareSound != null)
            {
                jumpscareSound.Play();
                Debug.Log("Jumpscare sound played");
            }
            else
            {
                Debug.LogWarning("Jumpscare sound reference is null");
            }

            // Προγραμματισμός απενεργοποίησης του αντικειμένου στο οποίο είναι συνδεδεμένος αυτός ο κώδικας
            if (jumpscareSound != null)
            {
                float soundDuration = jumpscareSound.clip.length;
                Invoke("DisableSelf", soundDuration);
            }

            // Ενεργοποίηση του καθορισμένου αντικειμένου όταν εισέρχεται ο παίκτης
            if (objectToEnableOnEnter != null)
            {
                objectToEnableOnEnter.SetActive(true);
                Debug.Log("Object enabled on enter");
            }
            else
            {
                Debug.LogWarning("Object to enable on enter reference is null");
            }
        }
    }

    private void DisableSelf()
    {
        // Απενεργοποίηση του αντικειμένου στο οποίο είναι συνδεδεμένος αυτός ο κώδικας
        gameObject.SetActive(false);
        Debug.Log("Self disabled");
    }
}
