using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollJumpscare : MonoBehaviour
{
    public GameObject monster;               // Το αντικείμενο του τέρατος που θα ενεργοποιηθεί
    public AudioSource jumpscareSound;       // Η πηγή ήχου για τον ήχο της τρομακτικής σκηνής
    public float animationSpeed = 1.0f;      // Ο πολλαπλασιαστής ταχύτητας για την κίνηση της κίνησης του τέρατος

    [SerializeField] private float monsterDisableEarlyTime = 0.1f;  // Ο χρόνος για την πρόωρη απενεργοποίηση του τέρατος
    [SerializeField] private float moveDuration = 1.0f;              // Διάρκεια της κίνησης του τέρατος
    [SerializeField] private float moveDistance = 2.0f;              // Απόσταση που θα μετακινηθεί το τέρας μπροστά

    private Vector3 initialMonsterPosition;  // Η αρχική θέση του τέρατος
    private Animator monsterAnimator;        // Αναφορά στο συστατικό Animator του τέρατος

    private void Start()
    {
        if (monster != null)
        {
            // Αποθήκευση της αρχικής θέσης του τέρατος
            initialMonsterPosition = monster.transform.position;

            // Λήψη του συστατικού Animator που είναι συνδεδεμένο με το τέρας
            monsterAnimator = monster.GetComponent<Animator>();

            if (monsterAnimator == null)
            {
                Debug.LogError("No Animator component found on the monster."); // Καταγραφή σφάλματος αν δεν βρεθεί το Animator
            }
            else
            {
                // Ορισμός της ταχύτητας κίνησης
                monsterAnimator.speed = animationSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Έλεγχος αν το αντικείμενο που μπήκε στο trigger είναι ο παίκτης
        if (other.CompareTag("Player"))
        {
            // Ενεργοποίηση του τέρατος
            if (monster != null)
            {
                monster.SetActive(true);
                // Έναρξη της coroutine για την κίνηση του τέρατος προς τα εμπρός
                StartCoroutine(MoveMonsterForward());
            }

            // Αναπαραγωγή του ήχου της τρομακτικής σκηνής
            if (jumpscareSound != null)
            {
                jumpscareSound.Play();
            }

            // Υπολογισμός της διάρκειας του ήχου της τρομακτικής σκηνής και προγραμματισμός της απενεργοποίησης του τέρατος και του αντικειμένου
            float soundDuration = jumpscareSound.clip.length;
            Invoke("DisableMonster", soundDuration - monsterDisableEarlyTime);
            Invoke("DisableMonsterAndSelf", soundDuration);
        }
    }

    private IEnumerator MoveMonsterForward()
    {
        float elapsedTime = 0.0f;
        Vector3 targetPosition = initialMonsterPosition + monster.transform.forward * moveDistance;

        while (elapsedTime < moveDuration)
        {
            // Ομαλή ενδιάμεση θέση του τέρατος μεταξύ της αρχικής και της στόχου θέσης
            monster.transform.position = Vector3.Lerp(initialMonsterPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Διασφάλιση ότι το τέρας φτάνει στη τελική στόχο θέση
        monster.transform.position = targetPosition;
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
        // Απενεργοποίηση του GameObject στο οποίο είναι συνδεδεμένος αυτός ο κώδικας
        gameObject.SetActive(false);
    }

    public void SetAnimationSpeed(float newSpeed)
    {
        // Αλλαγή της ταχύτητας κίνησης του τέρατος
        if (monsterAnimator != null)
        {
            monsterAnimator.speed = newSpeed;
            Debug.Log("Monster animation speed set to: " + newSpeed); // Καταγραφή της νέας ταχύτητας κίνησης
        }
    }
}
