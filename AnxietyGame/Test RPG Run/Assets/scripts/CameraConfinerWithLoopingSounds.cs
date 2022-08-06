using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfinerWithLoopingSounds : CameraConfinerOnOffTriggers
{
    public AudioClip seagulls;
    public AudioClip windAndWaves;
    public AudioSource audioSource;
    public float seagullnoise;
    public float waternoise;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);
            audioSource.loop = true;
            audioSource.PlayOneShot(seagulls,seagullnoise);
            audioSource.loop = true;
            audioSource.PlayOneShot(windAndWaves, waternoise);
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            audioSource.Stop();
            vCam.gameObject.SetActive(false);


        }

    }
}
