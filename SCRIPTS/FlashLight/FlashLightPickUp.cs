using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPickUp : MonoBehaviour
{
    public GameObject FlashLightOnPlayer;  // Το αντικείμενο του φακού που έχω στον παίκτη
    public GameObject player;  // Υποθέτω ότι ο παίκτης έχει μια κάμερα ως παιδί (child object)

    [SerializeField] private AudioClip pickUpSound = null;  // Ο ήχος που θέλω να παίξει όταν ο παίκτης πάρει τον φακό
    [SerializeField] private AudioSource audioSource = null; // Το AudioSource που χρησιμοποιώ για την αναπαραγωγή του ήχου

    [SerializeField] private GameObject additionalTrigger; // Το πρόσθετο trigger που θέλω να ενεργοποιηθεί

    void Start()
    {
        // Ενεργοποιώ τον φακό στον παίκτη, ανάλογα με την αποθηκευμένη ρύθμιση
        FlashLightOnPlayer.SetActive(KeppFlashOnHand.IsFlashlightEnabled);
    }

    void Update()
    {
        if (player != null)
        {
            // Ενημερώνω τη θέση του φακού ώστε να ταιριάζει με τη θέση της κάμερας του παίκτη
            FlashLightOnPlayer.transform.position = player.GetComponentInChildren<Camera>().transform.position;

            // Ενημερώνω τον προσανατολισμό του φακού ώστε να ταιριάζει με τον προσανατολισμό της κάμερας του παίκτη
            FlashLightOnPlayer.transform.rotation = player.GetComponentInChildren<Camera>().transform.rotation;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Όταν ο παίκτης πατήσει το κουμπί 'E', απενεργοποιώ το αντικείμενο του φακού που μπορεί να πάρει και ενεργοποιώ τον φακό στον παίκτη
            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                FlashLightOnPlayer.SetActive(true);

                // Αποθηκεύω ότι ο φακός είναι ενεργοποιημένος
                KeppFlashOnHand.IsFlashlightEnabled = true;
                
                // Ενεργοποιώ το πρόσθετο trigger
                if (additionalTrigger != null)
                {
                    additionalTrigger.SetActive(true);
                }

                // Παίζω τον ήχο του pick-up
                PlaySound(pickUpSound);
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        // Ελέγχω αν το AudioSource και το AudioClip είναι έτοιμα, και παίζω τον ήχο εάν δεν αναπαράγεται ήδη
        if (audioSource != null && clip != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning("Το AudioSource παίζει ήδη. Περιμένω να τελειώσει.");
            }
        }
        else
        {
            Debug.LogError("Το AudioSource ή το AudioClip δεν είναι ορισμένα.");
        }
    }
}
