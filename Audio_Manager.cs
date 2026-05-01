using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_Manager : MonoBehaviour
{
    public Slider VolumeSlider; // Ρυθμιστικό έντασης ήχου

    void Start()
    {
        // Έλεγχος αν υπάρχει αποθηκευμένη τιμή έντασης ήχου
        if (PlayerPrefs.HasKey("Volume"))
            LoadVolume(); // Φόρτωση αποθηκευμένης τιμής έντασης
        else
        {
            PlayerPrefs.SetFloat("Volume", 1); // Ορισμός προεπιλεγμένης τιμής έντασης
            LoadVolume(); // Φόρτωση προεπιλεγμένης τιμής έντασης
        }
    }

    public void SetVolume()
    {
        AudioListener.volume = VolumeSlider.value; // Ενημέρωση της έντασης του ήχου
        SaveVolume(); // Αποθήκευση της νέας τιμής έντασης
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", VolumeSlider.value); // Αποθήκευση της τιμής έντασης
    }

    public void LoadVolume()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("Volume"); // Φόρτωση της αποθηκευμένης τιμής έντασης
    }
}
