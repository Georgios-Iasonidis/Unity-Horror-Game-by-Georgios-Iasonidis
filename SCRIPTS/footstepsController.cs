using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepsController : MonoBehaviour
{
    public AudioSource footstepsSound;

    void Update()
    {
        // Ελέγχει αν ο παίκτης πιέζει κάποιο από τα πλήκτρα κίνησης (W, A, S, D)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // Αν ο παίκτης κρατάει το πλήκτρο Shift
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Απενεργοποιεί τον ήχο των βημάτων (π.χ., αν τρέχει)
                footstepsSound.enabled = false;
            }
            else
            {
                // Ενεργοποιεί τον ήχο των βημάτων (π.χ., αν περπατάει)
                footstepsSound.enabled = true;
            }
        }
        else
        {
            // Αν ο παίκτης δεν κινείται, απενεργοποιεί τον ήχο των βημάτων
            footstepsSound.enabled = false;
        }
    }
}
