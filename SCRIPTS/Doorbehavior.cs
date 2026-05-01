using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null; // Αναφορά στον Animator της πόρτας

    private bool isDoorOpen = false; // Καταγραφή αν η πόρτα είναι ανοιχτή ή κλειστή
    private Coroutine autoCloseCoroutine; // Αναφορά στην coroutine για αυτόματο κλείσιμο της πόρτας
    private bool canInteract = false; // Σημαίνει αν ο παίκτης μπορεί να αλληλεπιδράσει με την πόρτα
    [SerializeField] private AudioClip openSound = null;   // Ήχος για το άνοιγμα της πόρτας
    [SerializeField] private AudioClip closeSound = null;  // Ήχος για το κλείσιμο της πόρτας
    [SerializeField] private AudioSource audioSource = null; // Συστατικό AudioSource

    private void Update()
    {
        // Ελέγχουμε αν ο παίκτης πατάει το πλήκτρο E και αν μπορεί να αλληλεπιδράσει με την πόρτα
        if (Input.GetKeyDown(KeyCode.E) && canInteract && !IsAnimationPlaying())
        {
            if (isDoorOpen)
            {
                CloseDoor(); // Κλείνει την πόρτα αν είναι ανοιχτή
            }
            else
            {
                OpenDoor(); // Ανοίγει την πόρτα αν είναι κλειστή
            }
        }
    }

    private void OpenDoor()
    {
        myDoor.Play("DoorOpen_toilet", 0, 0.0f); // Παίζει την animation για το άνοιγμα της πόρτας
        PlaySound(openSound);  // Αναπαράγει τον ήχο του ανοίγματος
        isDoorOpen = true; // Θέτει την πόρτα ως ανοιχτή

        // Αν υπάρχει ήδη ενεργή coroutine για το κλείσιμο, τη σταματάμε
        if (autoCloseCoroutine != null)
        {
            StopCoroutine(autoCloseCoroutine);
        }

        // Ξεκινάμε νέα coroutine για το κλείσιμο της πόρτας μετά από 7 δευτερόλεπτα
        autoCloseCoroutine = StartCoroutine(CloseDoorAfterDelay(7.0f));
    }

    private IEnumerator CloseDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Περιμένουμε για τον καθορισμένο χρόνο
        CloseDoor(); // Κλείνουμε την πόρτα μετά την αναμονή
    }

    private void CloseDoor()
    {
        myDoor.Play("DoorClose_toilet", 0, 0.0f); // Παίζει την animation για το κλείσιμο της πόρτας
        PlaySound(closeSound); // Αναπαράγει τον ήχο του κλεισίματος
        isDoorOpen = false; // Θέτει την πόρτα ως κλειστή
        autoCloseCoroutine = null; // Καθαρίζει την αναφορά στην coroutine
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip); // Αναπαράγει τον ήχο
        }
    }

    private bool IsAnimationPlaying()
    {
        // Ελέγχει αν κάποια animation είναι σε εξέλιξη
        return myDoor.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true; // Επιτρέπει στον παίκτη να αλληλεπιδράσει με την πόρτα
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false; // Απαγορεύει στον παίκτη να αλληλεπιδράσει με την πόρτα
        }
    }
}
