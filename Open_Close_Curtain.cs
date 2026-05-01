using System.Collections;
using UnityEngine;

public class Open_Close_Curtain : MonoBehaviour
{
    [SerializeField] private Animator myCurtain = null;
    [SerializeField] private AudioClip openSound = null;   // Sound for opening
    [SerializeField] private AudioSource audioSource = null; // AudioSource component

    private bool isCurtainOpen = false;
    private Coroutine autoCloseCoroutine;
    private bool canInteract = false; // Indicates if the player can interact with the curtain

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract && !IsAnimationPlaying())
        {
            if (isCurtainOpen)
            {
                CloseCurtain();
            }
            else
            {
                OpenCurtain();
            }
        }
    }

    private void OpenCurtain()
    {
        myCurtain.SetBool("isOpen", true);
        PlaySound(openSound);  // Play opening sound
        isCurtainOpen = true;

        if (autoCloseCoroutine != null)
        {
            StopCoroutine(autoCloseCoroutine);
        }

        autoCloseCoroutine = StartCoroutine(CloseCurtainAfterDelay(15.0f));
    }

    private IEnumerator CloseCurtainAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseCurtain();
    }

    private void CloseCurtain()
    {
        myCurtain.SetBool("isOpen", false);
        PlaySound(openSound);  // Play opening sound
        isCurtainOpen = false;
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
        return myCurtain.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
