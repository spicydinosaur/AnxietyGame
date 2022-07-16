using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfinerSoundElsewhere : CameraConfinerOnOffTriggers
{
    public GameObject soundSource;
    public AudioSource clockTick;
    public AudioSource clockChime;
    public void Start()
    {
        clockTick = soundSource.GetComponent<AudioSource>();
        clockChime = soundSource.GetComponentInChildren<AudioSource>();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);
            clockTick.Play();

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
