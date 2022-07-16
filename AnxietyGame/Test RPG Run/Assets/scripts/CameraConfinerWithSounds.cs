using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfinerWithSounds : CameraConfinerOnOffTriggers
{
    public AudioSource clockTick;
    public AudioSource clockChime;
    void Start()
    {
        clockTick = GetComponent<AudioSource>();
        clockChime = GetComponentInChildren<AudioSource>();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);
            clockTick.Play();
            if (!clockChime.isPlaying)
            {
                clockChime.Play();
            }

        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(false);
            clockTick.Pause();

        }

    }

}
