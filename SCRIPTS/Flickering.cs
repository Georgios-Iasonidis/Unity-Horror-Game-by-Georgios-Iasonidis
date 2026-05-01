using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering : MonoBehaviour
{
    public new Light light;  // Αναφορά στο φως που θα αναβοσβήνει
    [Tooltip("Ελάχιστη τυχαία ένταση φωτός")]
    public float minIntensity = 0f;  // Ελάχιστη ένταση φωτός
    [Tooltip("Μέγιστη τυχαία ένταση φωτός")]
    public float maxIntensity = 0.5f;  // Μέγιστη ένταση φωτός
    [Tooltip("Πόσο να εξομαλυνθεί η τυχαιότητα; Χαμηλότερες τιμές = σπινθήρες, υψηλότερες = φανάρι")]
    [Range(1, 50)]
    public int smoothing = 5;  // Επίπεδο εξομάλυνσης της τυχαιότητας

    Queue<float> smoothQueue;  // Ουρά για την αποθήκευση των τιμών έντασης
    float lastSum = 0;  // Τελευταίο άθροισμα των τιμών έντασης

    // Μέθοδος για την επαναφορά της κατάστασης
    public void Reset() {
        smoothQueue.Clear();  // Εκκαθάριση της ουράς
        lastSum = 0;  // Επαναφορά του τελευταίου αθροίσματος
    }

    // Κλήση κατά την εκκίνηση
    void Start() {
         smoothQueue = new Queue<float>(smoothing);  // Δημιουργία νέας ουράς με το μέγεθος της εξομάλυνσης
         // Εξωτερικό ή εσωτερικό φως;
         if (light == null) {
            light = GetComponent<Light>();  // Αν δεν έχει οριστεί εξωτερικά, ανακτά το φως από το ίδιο το GameObject
         }
    }

    // Κλήση σε κάθε frame
    void Update() {
        if (light == null)
            return;  // Αν δεν υπάρχει φως, έξοδος από τη μέθοδο

        // Αφαίρεση ενός στοιχείου αν είναι πάρα πολλά
        while (smoothQueue.Count >= smoothing) {
            lastSum -= smoothQueue.Dequeue();  // Αφαίρεση του πρώτου στοιχείου της ουράς και μείωση του τελευταίου αθροίσματος
        }

        // Δημιουργία νέας τυχαίας τιμής και υπολογισμός νέου μέσου όρου
        float newVal = Random.Range(minIntensity, maxIntensity);  // Δημιουργία νέας τυχαίας τιμής έντασης
        smoothQueue.Enqueue(newVal);  // Προσθήκη της νέας τιμής στην ουρά
        lastSum += newVal;  // Προσθήκη της νέας τιμής στο τελευταίο άθροισμα

        // Υπολογισμός νέου εξομαλυνθέντος μέσου όρου
        light.intensity = lastSum / (float)smoothQueue.Count;  // Ενημέρωση της έντασης του φωτός με τη μέση τιμή
    }
}
