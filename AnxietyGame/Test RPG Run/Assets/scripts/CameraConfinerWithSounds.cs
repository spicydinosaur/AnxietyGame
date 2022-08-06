using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfinerWithSounds : CameraConfinerOnOffTriggers
{
    public AudioClip clockTick;
    public AudioClip clockChime;
    public AudioSource audioSource;
    public bool looping;
    public bool fromUpstairs;

    //below is for the confiner that triggers the upstairs sound
    public GameObject upstairsSeasideHouse;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        looping = false;

    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);
            if (gameObject != upstairsSeasideHouse && !fromUpstairs)
            {
                audioSource.loop = !looping;
                audioSource.PlayOneShot(clockChime, 1f);
            }
            else
            {

                //this may need a coroutine if there is a way to go from upstairs seaside house to anywhere but the lower floor in one port
                fromUpstairs = false;
                audioSource.loop = looping;
                audioSource.PlayOneShot(clockTick, 1f);
            }
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (audioSource.loop == false)
            {
                audioSource.loop = looping;
            }
            audioSource.PlayOneShot(clockTick, .5f);
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(false);
            audioSource.Stop();

        }

    }
}
