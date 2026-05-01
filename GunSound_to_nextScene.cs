using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunSound_to_nextScene : MonoBehaviour
{
    public AudioClip gunSound;       // Drag and drop your AudioClip here
    public AudioSource audioSource;  // Drag and drop your AudioSource here (optional)

    void Start()
    {
        // If no AudioSource is assigned, get or add one
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Assign the clip to the AudioSource
        audioSource.clip = gunSound;

        // Start playing the sound and then load the next scene
        StartCoroutine(PlaySoundAndLoadNextScene());
    }

    IEnumerator PlaySoundAndLoadNextScene()
    {
        // Play the sound
        audioSource.Play();

        // Wait until the sound finishes
        yield return new WaitForSeconds(audioSource.clip.length);

        // Load the next scene
        LoadNextScene();
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No next scene. Returning to the initial scene.");

            // Load the Main Menu (assuming it's the first scene in the build settings)
            SceneManager.LoadScene(0);
        }
    }
}
