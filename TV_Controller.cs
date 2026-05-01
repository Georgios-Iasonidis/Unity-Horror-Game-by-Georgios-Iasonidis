using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TVBehavior : MonoBehaviour
{
    [SerializeField] private Light tvLight1 = null; // The first TV light
    [SerializeField] private Light tvLight2 = null; // The second TV light
    [SerializeField] private AudioClip tvSound = null; // The sound of the TV
    [SerializeField] private AudioSource audioSource = null; // AudioSource component
    [SerializeField] private Transform playerCamera = null; // The player's camera
    [SerializeField] private float viewAngle = 30f; // Player's view angle
    [SerializeField] private GameObject targetObject = null; // The object to enable/disable
    [SerializeField] private bool isSecondScene = false; // Whether this is the second scene

    private bool isTVOn = false; // Whether the TV is on
    private bool canInteract = false; // Whether the player can interact with the TV

    private Collider tvCollider;

    private void Start()
    {
        // Get the Collider of the TV
        tvCollider = GetComponent<Collider>();

        // Check if this is the second scene
        isSecondScene = SceneManager.GetActiveScene().buildIndex == 2;

        // Load the saved TV state or turn on the TV by default if it's the second or sixth scene
        LoadTVState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract && IsPlayerLookingAtCollider())
        {
            if (isTVOn)
            {
                TurnOffTV();
            }
            else
            {
                TurnOnTV();
            }
        }
    }

    private bool IsPlayerLookingAtCollider()
    {
        if (tvCollider == null) return false;

        // Calculate the position of the Collider (center of the Collider)
        Vector3 colliderCenter = tvCollider.bounds.center;

        // Calculate the direction from the camera to the center of the Collider
        Vector3 directionToCollider = colliderCenter - playerCamera.position;

        // Calculate the angle between the camera's viewing direction and the direction to the Collider
        float angle = Vector3.Angle(playerCamera.forward, directionToCollider);

        return angle <= viewAngle;
    }

    private void TurnOnTV()
    {
        if (tvLight1 != null)
        {
            tvLight1.enabled = true; // Enable the first TV light
        }

        if (tvLight2 != null)
        {
            tvLight2.enabled = true; // Enable the second TV light
        }

        if (targetObject != null)
        {
            targetObject.SetActive(true); // Enable the target object
        }

        PlaySound(tvSound); // Play the TV sound
        isTVOn = true;

        SaveTVState();
    }

    private void TurnOffTV()
    {
        if (tvLight1 != null)
        {
            tvLight1.enabled = false; // Disable the first TV light
        }

        if (tvLight2 != null)
        {
            tvLight2.enabled = false; // Disable the second TV light
        }

        if (targetObject != null)
        {
            targetObject.SetActive(false); // Disable the target object
        }

        StopSound(); // Stop the TV sound
        isTVOn = false;

        SaveTVState();
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    private void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    private void SaveTVState()
    {
        PlayerPrefs.SetInt("TVState", isTVOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadTVState()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 2 || currentSceneIndex == 6)
        {
            isTVOn = true; // Turn on the TV by default in the second and sixth scenes
        }
        else if (PlayerPrefs.HasKey("TVState"))
        {
            isTVOn = PlayerPrefs.GetInt("TVState") == 1;
        }

        if (isTVOn)
        {
            TurnOnTV();
        }
        else
        {
            TurnOffTV();
        }
    }
}
