using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false; // Κατάσταση παύσης παιχνιδιού
    public GameObject PauseMenuCanvas; // Ο πίνακας του μενού παύσης
    public GameObject optionsMenuPanel; // Πίνακας για το μενού ρυθμίσεων
    public GameObject displayMenuPanel; // Πίνακας για το μενού εμφάνισης
    public GameObject audioMenuPanel; // Πίνακας για το μενού ήχου
    public GameObject controlsMenuPanel; // Πίνακας για το μενού ελέγχων

    public Button audioBackButton;  // Αναφορά στο κουμπί επιστροφής στο μενού ρυθμίσεων ήχου
    public Button controlsBackButton;  // Αναφορά στο κουμπί επιστροφής στο μενού ελέγχων
    public Button displayBackButton;  // Αναφορά στο κουμπί επιστροφής στο μενού εμφάνισης

    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Ξεκλείδωμα του κέρσορα
        Cursor.visible = true; // Ορατότητα του κέρσορα

        // Ανάθεση της μεθόδου BackToOptionsMenu στα κουμπιά επιστροφής
        audioBackButton.onClick.AddListener(BackToOptionsMenu);
        controlsBackButton.onClick.AddListener(BackToOptionsMenu);
        displayBackButton.onClick.AddListener(BackToOptionsMenu);
    }

    void Update()
    {
        // Πατήστε 'Escape' για εναλλαγή του μενού παύσης
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Play(); // Επανεκκίνηση του παιχνιδιού
            }
            else
            {
                Stop(); // Παύση του παιχνιδιού
            }
        }
    }

    void Stop()
    {
        if (!Paused)
        {
            PauseMenuCanvas.SetActive(true); // Ενεργοποίηση του μενού παύσης
            Time.timeScale = 0f; // Σταμάτημα του χρόνου του παιχνιδιού
            Paused = true; // Ενημέρωση της κατάστασης παύσης
            Cursor.lockState = CursorLockMode.None; // Ξεκλείδωμα του κέρσορα
            Cursor.visible = true; // Ορατότητα του κέρσορα
        }
    }

    public void Play()
    {
        if (Paused)
        {
            PauseMenuCanvas.SetActive(false); // Απενεργοποίηση του μενού παύσης
            Time.timeScale = 1f; // Επαναφορά του χρόνου του παιχνιδιού
            Paused = false; // Ενημέρωση της κατάστασης παύσης
            Cursor.lockState = CursorLockMode.Locked; // Κλείδωμα του κέρσορα
            Cursor.visible = false; // Απόκρυψη του κέρσορα
        }
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;  // Επαναφορά του χρόνου του παιχνιδιού
        Cursor.lockState = CursorLockMode.None;  // Ξεκλείδωμα του κέρσορα
        Cursor.visible = true;  // Ορατότητα του κέρσορα
        SceneManager.LoadScene(0);  // Φόρτωση της πρώτης σκηνής (κύριο μενού)
    }

    public void ShowOptionsMenu()
    {
        Debug.Log("Showing Options Menu");
        optionsMenuPanel.SetActive(true); // Ενεργοποίηση του μενού ρυθμίσεων
        displayMenuPanel.SetActive(false); // Απενεργοποίηση του μενού εμφάνισης
        audioMenuPanel.SetActive(false); // Απενεργοποίηση του μενού ήχου
        controlsMenuPanel.SetActive(false); // Απενεργοποίηση του μενού ελέγχων
    }

    public void HideOptionsMenu()
    {
        Debug.Log("Hiding Options Menu");
        optionsMenuPanel.SetActive(false); // Απενεργοποίηση του μενού ρυθμίσεων
    }

    public void ShowDisplayMenu()
    {
        Debug.Log("Showing Display Menu");
        optionsMenuPanel.SetActive(false); // Απενεργοποίηση του μενού ρυθμίσεων
        displayMenuPanel.SetActive(true); // Ενεργοποίηση του μενού εμφάνισης
    }

    public void ShowAudioMenu()
    {
        Debug.Log("Showing Audio Menu");
        optionsMenuPanel.SetActive(false); // Απενεργοποίηση του μενού ρυθμίσεων
        audioMenuPanel.SetActive(true); // Ενεργοποίηση του μενού ήχου
    }

    public void ShowControlsMenu()
    {
        Debug.Log("Showing Controls Menu");
        optionsMenuPanel.SetActive(false); // Απενεργοποίηση του μενού ρυθμίσεων
        controlsMenuPanel.SetActive(true); // Ενεργοποίηση του μενού ελέγχων
    }

    public void BackToOptionsMenu()
    {
        Debug.Log("Back to Options Menu");
        displayMenuPanel.SetActive(false); // Απενεργοποίηση του μενού εμφάνισης
        audioMenuPanel.SetActive(false); // Απενεργοποίηση του μενού ήχου
        controlsMenuPanel.SetActive(false); // Απενεργοποίηση του μενού ελέγχων
        optionsMenuPanel.SetActive(true); // Ενεργοποίηση του μενού ρυθμίσεων
    }
}
