using UnityEngine;

public class CollectNotes : MonoBehaviour
{
    public static int totalNotes = 5; // Total number of notes
    public static int collectedNotes = 0; // Counter for collected notes

    public GameObject objectEnable; // The specific object to enable after collecting all notes

    private bool isPlayerInside = false; // To check if the player is currently inside the trigger zone

    private void Start()
    {
        // Ensure that the objectEnable is not null
        if (objectEnable == null)
        {
            Debug.LogError("ObjectEnable is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            // Optionally show a message or UI to indicate the note can be read
            Debug.Log("Player can read the note.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag("Player") && isPlayerInside)
        {
            // Collect the note
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false); // Deactivate the note
                collectedNotes++; // Increment the collected notes count

                Debug.Log($"Note collected. Total collected: {collectedNotes}");

                // Check if all notes have been collected
                if (collectedNotes == totalNotes)
                {
                    if (objectEnable != null)
                    {
                        objectEnable.SetActive(true);
                        Debug.Log("ObjectEnable has been enabled after collecting all notes.");
                    }
                    else
                    {
                        Debug.LogWarning("ObjectEnable is not assigned.");
                    }
                }
            }

            isPlayerInside = false; // Reset player inside status
        }
    }
}
