using UnityEngine;
using UnityEngine.SceneManagement;

public class TpController : MonoBehaviour
{
    void OnEnable()
    {
        // Καταχωρεί τη μέθοδο OnSceneLoaded για να εκτελείται όταν φορτώνεται μια νέα σκηνή
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Αφαίρεση της μεθόδου sceneLoaded όταν το αντικείμενο απενεργοποιείται
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        // Ορισμός της αρχικής κατάστασης του κέρσορα
        SetCursorState(CursorLockMode.None, true);
        Debug.Log("Start: Cursor should be visible and unlocked.");
    }

    void Update()
    {
        // Πατήστε 'C' για να εναλλάξετε την ορατότητα και την κατάσταση κλειδώματος του κέρσορα
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCursorState();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Απόκτηση του component PlayerCam και ρύθμιση της περιστροφής Υ
            PlayerCam playerCam = other.GetComponentInChildren<PlayerCam>();
            if (playerCam != null)
            {
                playerCam.AdjustYRotation(90f); // 90 μοιρες προς τα δεξια
                Debug.Log("PlayerCam Y rotation adjusted.");
            }
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Φόρτωση της επόμενης σκηνής
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("Loading next scene: Index " + nextSceneIndex);
        }
        else
        {
            Debug.Log("No next scene. Returning to the initial scene.");

            // Ορισμός της ορατότητας και της αποδέσμευσης του κέρσορα πριν από τη φόρτωση του κύριου μενού
            SetCursorState(CursorLockMode.None, true);
            Debug.Log("Cursor should now be visible and unlocked before loading the main menu.");

            // Φόρτωση του κύριου μενού
            SceneManager.LoadScene(0);
        }
    }

    // Μέθοδος για την ρύθμιση της κατάστασης του κέρσορα
    void SetCursorState(CursorLockMode lockMode, bool visible)
    {
        // Ορίζει την κατάσταση κλειδώματος του κέρσορα (π.χ. κλειδωμένος ή ξεκλείδωτος)
        Cursor.lockState = lockMode;

        // Ορίζει την ορατότητα του κέρσορα (π.χ. ορατός ή κρυμμένος)
        Cursor.visible = visible;

        // Καταγράφει την αλλαγή στην κατάσταση του κέρσορα στο κονσόλα για αποσφαλμάτωση
        Debug.Log("Cursor state set: " + (visible ? "Visible" : "Hidden") + ", Lock State: " + lockMode);
    }


        //ΓΙΑ ΕΝΝΑΛΛΑΓΕΣ ΣΤΟ ΠΕΡΙΒΑΛΛΩΝ ΜΕ ΤΟ UI (οταν ο καιρσοσας πρεπει και οταν δεν πρεπει να ειναι ενεργος - εμφανης)!!!! :

    //Εναλλάσσει την κατάσταση του κέρσορα για αποσφαλμάτωση ή ειδικές περιπτώσεις.
    void ToggleCursorState()
    {
        CursorLockMode newLockMode = (Cursor.lockState == CursorLockMode.None) ? CursorLockMode.Locked : CursorLockMode.None;
        bool newVisible = !Cursor.visible;
        SetCursorState(newLockMode, newVisible);
    }

    //Εξασφαλίζει ότι η κατάσταση του κέρσορα είναι κατάλληλη μετά τη φόρτωση μιας νέας σκηνής.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ορισμός της κατάστασης του κέρσορα όταν φορτώνεται μια νέα σκηνή
        SetCursorState(CursorLockMode.None, true);
        Debug.Log("OnSceneLoaded: Cursor should be visible and unlocked in scene " + scene.name);
    }
}
