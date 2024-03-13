using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDoorOpenAndClose : MonoBehaviour
{

    public Animator animator;
    public AudioSource audioSource;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;


    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("doorTriggered", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && animator.GetBool("doorTriggered") != true)
        {
            animator.SetBool("doorTriggered", true);
            audioSource.PlayOneShot(doorOpenSound, .5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && animator.GetBool("doorTriggered") == true)
        {
            animator.SetBool("doorTriggered", false);
            audioSource.PlayOneShot(doorCloseSound, .5f);
        }
    }
}
