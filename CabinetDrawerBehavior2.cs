using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDrawerBehavior2 : MonoBehaviour
{
    [SerializeField] private Animator myCabinetDoor = null;
    [SerializeField] private Transform playerCamera = null; // The player's camera
    [SerializeField] private float viewAngle = 30f; // Player's view angle
    [SerializeField] private AudioClip openSound = null;   // Sound for opening
    [SerializeField] private AudioClip closeSound = null;  // Sound for closing
    [SerializeField] private AudioSource audioSource = null; // AudioSource component

    private bool isDoorOpen = false;
    private Coroutine autoCloseCoroutine;
    private bool canInteract = false; // Whether the player can interact with the door

    private Collider doorCollider;

    private void Start()
    {
        // Get the Collider of the object
        doorCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract && IsPlayerLookingAtCollider() && !IsAnimationPlaying())
        {
            if (isDoorOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
    }

    private bool IsPlayerLookingAtCollider()
    {
        if (doorCollider == null) return false;

        // Calculate the position of the Collider (center of the Collider)
        Vector3 colliderCenter = doorCollider.bounds.center;

        // Calculate the direction from the camera to the center of the Collider
        Vector3 directionToCollider = colliderCenter - playerCamera.position;

        // Calculate the angle between the camera's viewing direction and the direction to the Collider
        float angle = Vector3.Angle(playerCamera.forward, directionToCollider);

        return angle <= viewAngle;
    }

    private void OpenDoor()
    {
        myCabinetDoor.Play("DrawerOpen", 0, 0.0f); // Play the animation for opening the door
        PlaySound(openSound);  // Play opening sound
        isDoorOpen = true;

        // If there is already an active coroutine for closing, stop it
        if (autoCloseCoroutine != null)
        {
            StopCoroutine(autoCloseCoroutine);
        }

        // Start a new coroutine to close the door after 7 seconds
        autoCloseCoroutine = StartCoroutine(CloseDoorAfterDelay(7.0f));
    }

    private IEnumerator CloseDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseDoor();
    }

    private void CloseDoor()
    {
        myCabinetDoor.Play("DrawerClose", 0, 0.0f); // Play the animation for closing the door
        PlaySound(closeSound); // Play closing sound
        isDoorOpen = false;
        autoCloseCoroutine = null;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private bool IsAnimationPlaying()
    {
        // Check if any animation is currently playing
        AnimatorStateInfo stateInfo = myCabinetDoor.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("DrawerOpen") || stateInfo.IsName("DrawerClose") && stateInfo.normalizedTime < 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CompareTag("Cabinet_drawer_02"))
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
}
