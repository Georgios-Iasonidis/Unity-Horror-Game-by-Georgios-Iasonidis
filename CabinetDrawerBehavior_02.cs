using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDrawerBehavior_02 : MonoBehaviour
{
    [SerializeField] private Animator myCabinetDoor = null;
    [SerializeField] private Transform playerCamera = null; // The player's camera
    [SerializeField] private float viewAngle = 30f; // Player's view angle

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
        if (Input.GetKeyDown(KeyCode.E) && canInteract && IsPlayerLookingAtCollider())
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
        isDoorOpen = false;
        autoCloseCoroutine = null;
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
