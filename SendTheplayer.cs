using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendThePlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Λήψη του δείκτη της τρέχουσας σκηνής
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Φόρτωση της επόμενης σκηνής με αύξηση του δείκτη
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
