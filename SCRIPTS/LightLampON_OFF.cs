using System.Collections;
using UnityEngine;

public class LightLampON_OFF : MonoBehaviour
{
    Light myLight; // Μεταβλητή για τον έλεγχο του φωτός.
    public float interval = 2.0f; // Χρόνος σε δευτερόλεπτα που το φως παραμένει σε πλήρη ή μη ένταση.
    public float transitionDuration = 0.5f; // Διάρκεια της μετάβασης της έντασης του φωτός.

    // Καλείται πριν την πρώτη ανανέωση καρέ
    void Start()
    {
        myLight = GetComponent<Light>(); // Ανάθεση του Light στη μεταβλητή.
        StartCoroutine(AutoToggleLight()); // Εκκίνηση της coroutine για αυτόματο εναλλαγή του φωτός.
    }

    IEnumerator AutoToggleLight()
    {
        while (true) // Ατελείωτος βρόχος για συνεχή εκτέλεση της διαδικασίας.
        {
            // Σταδιακή αύξηση της έντασης του φωτός στην πλήρη.
            yield return StartCoroutine(ChangeIntensity(1));
            // Διατήρηση πλήρους έντασης για ένα καθορισμένο διάστημα.
            yield return new WaitForSeconds(interval);
            // Σταδιακή μείωση της έντασης του φωτός στο μηδέν.
            yield return StartCoroutine(ChangeIntensity(0));
            // Διατήρηση μηδενικής έντασης για ένα καθορισμένο διάστημα.
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator ChangeIntensity(float targetIntensity)
    {
        float initialIntensity = myLight.intensity; // Αποθήκευση της αρχικής έντασης του φωτός.
        float time = 0;

        while (time < transitionDuration)
        {
            // Σταδιακή αλλαγή της έντασης του φωτός κατά τη διάρκεια της μετάβασης.
            myLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, time / transitionDuration);
            time += Time.deltaTime; // Αυξάνει το χρόνο με το χρόνο που έχει περάσει από το τελευταίο καρέ.
            yield return null; // Περιμένει το επόμενο καρέ.
        }

        // Διασφαλίζει ότι το φως φτάνει στην τελική ένταση.
        myLight.intensity = targetIntensity;
    }
}
