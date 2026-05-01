using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject startButton; // Κουμπί για την έναρξη του παιχνιδιού
    public GameObject optionsButton; // Κουμπί για τις ρυθμίσεις
    public GameObject quitButton; // Κουμπί για έξοδο από το παιχνίδι
    public GameObject title; // Τίτλος της αρχικής οθόνης
    public GameObject optionsMenuPanel; // Πάνελ με ρυθμίσεις
    public GameObject displayMenuPanel; // Πάνελ με ρυθμίσεις οθόνης
    public GameObject audioMenuPanel; // Πάνελ με ρυθμίσεις ήχου
    public GameObject controlsMenuPanel; // Πάνελ με ρυθμίσεις ελέγχου

    public Button audioBackButton;  // Αναφορά στο κουμπί επιστροφής από το μενού ήχου
    public Button controlsBackButton;  // Αναφορά στο κουμπί επιστροφής από το μενού ελέγχου
    public Button displayBackButton;  // Αναφορά στο κουμπί επιστροφής από το μενού οθόνης

    void Start()
    {
        // Ανάθεση της μεθόδου BackToOptionsMenu στα κουμπιά επιστροφής
        audioBackButton.onClick.AddListener(BackToOptionsMenu);
        controlsBackButton.onClick.AddListener(BackToOptionsMenu);
        displayBackButton.onClick.AddListener(BackToOptionsMenu);
    }

    public void Play()
    {
        // Φόρτωμα της επόμενης σκηνής
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            // Τερματισμός της εκτέλεσης στο Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Έξοδος από την εφαρμογή
            Application.Quit();
        #endif
        Debug.Log("Exit game"); // Εμφάνιση μηνύματος εξόδου
    }

    public void ShowOptionsMenu()
    {
        Debug.Log("Showing Options Menu"); // Εμφάνιση μηνύματος για εμφάνιση μενού ρυθμίσεων
        startButton.SetActive(false); // Απόκρυψη του κουμπιού έναρξης
        optionsButton.SetActive(false); // Απόκρυψη του κουμπιού ρυθμίσεων
        quitButton.SetActive(false); // Απόκρυψη του κουμπιού εξόδου
        title.SetActive(false); // Απόκρυψη του τίτλου
        optionsMenuPanel.SetActive(true); // Εμφάνιση του πίνακα ρυθμίσεων
        displayMenuPanel.SetActive(false); // Απόκρυψη του πίνακα ρυθμίσεων οθόνης
        audioMenuPanel.SetActive(false); // Απόκρυψη του πίνακα ρυθμίσεων ήχου
        controlsMenuPanel.SetActive(false); // Απόκρυψη του πίνακα ρυθμίσεων ελέγχου
    }

    public void HideOptionsMenu()
    {
        Debug.Log("Hiding Options Menu"); // Εμφάνιση μηνύματος για απόκρυψη μενού ρυθμίσεων
        startButton.SetActive(true); // Εμφάνιση του κουμπιού έναρξης
        optionsButton.SetActive(true); // Εμφάνιση του κουμπιού ρυθμίσεων
        quitButton.SetActive(true); // Εμφάνιση του κουμπιού εξόδου
        title.SetActive(true); // Εμφάνιση του τίτλου
        optionsMenuPanel.SetActive(false); // Απόκρυψη του πίνακα ρυθμίσεων
    }

    public void ShowDisplayMenu()
    {
        Debug.Log("Showing Display Menu"); // Εμφάνιση μηνύματος για εμφάνιση μενού οθόνης
        optionsMenuPanel.SetActive(false); // Απόκρυψη του πίνακα ρυθμίσεων
        displayMenuPanel.SetActive(true); // Εμφάνιση του πίνακα ρυθμίσεων οθόνης
    }

    public void ShowAudioMenu()
    {
        Debug.Log("Showing Audio Menu"); // Εμφάνιση μηνύματος για εμφάνιση μενού ήχου
        optionsMenuPanel.SetActive(false); // Απόκρυψη του πίνακα ρυθμίσεων
        audioMenuPanel.SetActive(true); // Εμφάνιση του πίνακα ρυθμίσεων ήχου
    }

    public void ShowControlsMenu()
    {
        Debug.Log("Showing Controls Menu"); // Εμφάνιση μηνύματος για εμφάνιση μενού ελέγχου
        optionsMenuPanel.SetActive(false); // Απόκρυψη του πίνακα ρυθμίσεων
        controlsMenuPanel.SetActive(true); // Εμφάνιση του πίνακα ρυθμίσεων ελέγχου
    }

    public void BackToOptionsMenu()
    {
        Debug.Log("Back to Options Menu"); // Εμφάνιση μηνύματος για επιστροφή στο μενού ρυθμίσεων
        displayMenuPanel.SetActive(false); // Απόκρυψη του πίνακα ρυθμίσεων οθόνης
        audioMenuPanel.SetActive(false); // Απόκρυψη του πίνακα ρυθμίσεων ήχου
        controlsMenuPanel.SetActive(false); // Απόκρυψη του πίνακα ρυθμίσεων ελέγχου
        optionsMenuPanel.SetActive(true); // Εμφάνιση του πίνακα ρυθμίσεων
    }
}
