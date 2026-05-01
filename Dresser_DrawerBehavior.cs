using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dresser_DrawerBehavior : MonoBehaviour
{
    [SerializeField] private Animator myDrawer = null;
    [SerializeField] private AudioClip openSound = null;   // Sound for opening
    [SerializeField] private AudioClip closeSound = null;  // Sound for closing
    [SerializeField] private AudioSource audioSource = null; // AudioSource component

    private bool isDrawerOpen = false;
    private Coroutine autoCloseCoroutine;
    private bool canInteract = false; // Indicates if the player can interact with the drawer

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract && !IsAnimationPlaying())
        {
            if (isDrawerOpen)
            {
                CloseDrawer();
            }
            else
            {
                OpenDrawer();
            }
        }
    }

    private void OpenDrawer()
    {
        myDrawer.Play("DrawerOpen", 0, 0.0f); 
        PlaySound(openSound);  // Play opening sound
        isDrawerOpen = true;

        // If there is already an active coroutine for closing, stop it
        if (autoCloseCoroutine != null)
        {
            StopCoroutine(autoCloseCoroutine);
        }

        // Start a new coroutine to close the drawer after a delay
        autoCloseCoroutine = StartCoroutine(CloseDrawerAfterDelay(7.0f));
    }

    private IEnumerator CloseDrawerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseDrawer();
    }

    private void CloseDrawer()
    {
        myDrawer.Play("DrawerClose", 0, 0.0f); // Replace with the correct animation name
        PlaySound(closeSound); // Play closing sound
        isDrawerOpen = false;
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
        return myDrawer.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
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
}
