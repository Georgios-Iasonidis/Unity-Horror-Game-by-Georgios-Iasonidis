using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resolution_Controller : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown; // Dropdown για την επιλογή ανάλυσης οθόνης

    private Resolution[] resolutions; // Πίνακας με όλες τις διαθέσιμες αναλύσεις οθόνης
    private List<Resolution> filteredResolutions; // Λίστα με φιλτραρισμένες αναλύσεις σύμφωνα με τον τρέχοντα ρυθμό ανανέωσης

    private float currentRefreshRate; // Τρέχον ρυθμός ανανέωσης της οθόνης
    private int currentResolutionIndex = 0; // Τρέχον δείκτης ανάλυσης στην λίστα του dropdown

    // Εκτελείται κατά την αρχική φόρτωση του παιχνιδιού
    void Start()
    {
        resolutions = Screen.resolutions; // Λήψη όλων των διαθέσιμων αναλύσεων οθόνης
        filteredResolutions = new List<Resolution>(); // Δημιουργία λίστας για τις φιλτραρισμένες αναλύσεις

        resolutionDropdown.ClearOptions(); // Καθαρισμός των προηγούμενων επιλογών στο dropdown
        currentRefreshRate = Screen.currentResolution.refreshRateRatio.numerator; // Λήψη του τρέχοντος ρυθμού ανανέωσης

        Debug.Log("RefreshRate: " + currentRefreshRate); // Καταγραφή του τρέχοντος ρυθμού ανανέωσης για αποσφαλμάτωση

        // Φιλτράρισμα αναλύσεων σύμφωνα με τον τρέχοντα ρυθμό ανανέωσης
        for (int i = 0; i < resolutions.Length; i++)
        {
            Debug.Log("Resolution: "+ resolutions[i]); // Καταγραφή της τρέχουσας ανάλυσης για αποσφαλμάτωση
            if (resolutions[i].refreshRateRatio.numerator == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]); // Προσθήκη της ανάλυσης στη λίστα φιλτραρισμένων αναλύσεων
            }
        }

        List<string> options = new List<string>(); // Λίστα επιλογών για το dropdown
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            // Δημιουργία κειμένου επιλογής με βάση την ανάλυση και τον ρυθμό ανανέωσης
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRateRatio.numerator + " Hz";
            options.Add(resolutionOption);

            // Ορισμός της τρέχουσας ανάλυσης
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Προσθήκη των επιλογών στο dropdown και ορισμός της τρέχουσας επιλογής
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Μέθοδος για την αλλαγή ανάλυσης βάσει της επιλεγμένης τιμής του dropdown
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex]; // Λήψη της επιλεγμένης ανάλυσης
        Screen.SetResolution(resolution.width, resolution.height, true); // Ορισμός της νέας ανάλυσης οθόνης
    }
}
