using System.Collections;
using UnityEngine;

public class CabinetDoorBehavior : MonoBehaviour
{
    [SerializeField] private Animator myCabinetDoor = null;
    [SerializeField] private Transform playerCamera = null; // Η κάμερα του παίκτη
    [SerializeField] private float viewAngle = 30f; // Γωνία θέασης του παίκτη
    [SerializeField] private AudioClip openSound = null;   // Sound for opening
    [SerializeField] private AudioClip closeSound = null;  // Sound for closing
    [SerializeField] private AudioSource audioSource = null; // AudioSource component

    private bool isDoorOpen = false;
    private Coroutine autoCloseCoroutine;
    private bool canInteract = false; // Σημαίνει αν ο παίκτης μπορεί να αλληλεπιδράσει με την πόρτα

    private Collider doorCollider;

    private void Start()
    {
        // Αποκτήστε το Collider του αντικειμένου
        doorCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract && IsPlayerLookingAtCollider() && !IsAnimationPlaying())
        {
            if (isDoorOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
    }

    private bool IsPlayerLookingAtCollider()
    {
        if (doorCollider == null) return false;

        // Υπολογίστε τη θέση του Collider (κέντρο του Collider)
        Vector3 colliderCenter = doorCollider.bounds.center;

        // Υπολογίστε τη κατεύθυνση από την κάμερα προς το κέντρο του Collider
        Vector3 directionToCollider = colliderCenter - playerCamera.position;

        // Υπολογίστε την γωνία μεταξύ της κατεύθυνσης θέασης της κάμερας και της κατεύθυνσης προς το Collider
        float angle = Vector3.Angle(playerCamera.forward, directionToCollider);

        return angle <= viewAngle;
    }

    private void OpenDoor()
    {
        myCabinetDoor.Play("DrawerOpen", 0, 0.0f); // Παίζει το animation για το άνοιγμα της πόρτας
        PlaySound(openSound);  // Play opening sound
        isDoorOpen = true;

        // Αν υπάρχει ήδη ενεργή coroutine για το κλείσιμο, τη σταματάμε
        if (autoCloseCoroutine != null)
        {
            StopCoroutine(autoCloseCoroutine);
        }

        // Ξεκινάμε νέα coroutine για το κλείσιμο της πόρτας μετά από 7 δευτερόλεπτα
        autoCloseCoroutine = StartCoroutine(CloseDoorAfterDelay(10.0f));
    }

    private IEnumerator CloseDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseDoor();
    }

    private void CloseDoor()
    {
        myCabinetDoor.Play("DrawerClose", 0, 0.0f); // Παίζει το animation για το κλείσιμο της πόρτας
        PlaySound(closeSound); // Play closing sound
        isDoorOpen = false;
        autoCloseCoroutine = null;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private bool IsAnimationPlaying()
    {
        // Check if any animation is currently playing
        return myCabinetDoor.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CompareTag("Cabinet_door_01"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
