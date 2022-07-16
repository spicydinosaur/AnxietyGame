using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfinerWithLoopingSounds : CameraConfinerOnOffTriggers
{
    public AudioSource seagulls;
    public AudioSource windAndWaves;
    void Start()
    {
        seagulls = GetComponent<AudioSource>();
        windAndWaves = GetComponentInChildren<AudioSource>();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);
            seagulls.Play();
            windAndWaves.Play();
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(false);
            seagulls.Pause();
            windAndWaves.Pause();


        }

    }
}
