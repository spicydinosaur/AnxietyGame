using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfinerHouse : MonoBehaviour
{

    public AudioSource clockTick;
    public AudioSource clockChime;

    public CinemachineVirtualCamera vCam;

    public float chimeTimer;


    //this is for the seaside village house downstairs only



    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);
            if (chimeTimer <= 0)
            {
                clockTick.Stop();
                clockChime.Play();
                chimeTimer = 300;
            }
            else
            {
                clockTick.Play();
            }
        }
    }

    public void Update()
    {
        if (chimeTimer > 0)
        {
            chimeTimer -= Time.deltaTime;

        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            if (!clockChime.isPlaying && !clockTick.isPlaying)
            {
                clockTick.Play();
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(false);
            clockChime.Stop();
            clockTick.Stop();

        }

    }
}
