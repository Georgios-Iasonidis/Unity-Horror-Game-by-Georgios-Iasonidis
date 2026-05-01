using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareController : MonoBehaviour
{
    public GameObject monster;                    // Αντικείμενο του τέρατος που θα ενεργοποιηθεί
    public AudioSource jumpscareSound;            // Πηγή ήχου για το jumpscare
    public GameObject objectToEnableOnEnter;      // Αντικείμενο που θα ενεργοποιηθεί όταν ο παίκτης ενεργοποιήσει το jumpscare

    [SerializeField] private float monsterDisableEarlyTime = 0.1f; // Χρόνος για την πρόωρη απενεργοποίηση του τέρατος

    private void OnTriggerEnter(Collider other)
    {
        // Έλεγχος αν το αντικείμενο που εισήλθε στην περιοχή του trigger είναι ο παίκτης
        if (other.CompareTag("Player"))
        {
            // Ενεργοποίηση του τέρατος
            if (monster != null)
            {
                monster.SetActive(true);
            }

            // Αναπαραγωγή του ήχου του jumpscare
            if (jumpscareSound != null)
            {
                jumpscareSound.Play();
            }

            // Ενεργοποίηση του αντικειμένου
            if (objectToEnableOnEnter != null)
            {
                objectToEnableOnEnter.SetActive(true);
            }

            // Υπολογισμός της διάρκειας του ήχου του jumpscare και προγραμματισμός της απενεργοποίησης
            float soundDuration = jumpscareSound.clip.length;
            Invoke("DisableMonster", soundDuration - monsterDisableEarlyTime); // Πρόωρη απενεργοποίηση του τέρατος
            Invoke("DisableMonsterAndSelf", soundDuration); // Απενεργοποίηση του τέρατος και του GameObject
        }
    }

    private void DisableMonster()
    {
        // Απενεργοποίηση του τέρατος
        if (monster != null)
        {
            monster.SetActive(false);
        }
    }

    private void DisableMonsterAndSelf()
    {
        // Απενεργοποίηση του GameObject που περιέχει το σενάριο
        gameObject.SetActive(false);
    }
}
