using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfinerSoundElsewhere : CameraConfinerOnOffTriggers
{
    public AudioSource audioSource;
    public AudioClip clockTick;
    public AudioClip clockChime;
    public bool looping;

    public bool fromUpstairs;

    public float chimeNoise;
    public float tickNoise;

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
                audioSource.PlayOneShot(clockChime, chimeNoise);
            }
            else
            {

                //this may need a coroutine if there is a way to go from upstairs seaside house to anywhere but the lower floor in one port
                fromUpstairs = false;
                audioSource.loop = looping;
                audioSource.PlayOneShot(clockTick, tickNoise);
            }
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = looping;
            audioSource.PlayOneShot(clockTick, tickNoise);
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
