using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfinerSeaside : MonoBehaviour
{
    public AudioSource audioSourceSeagull;
    public AudioSource audioSourceWater;

    public float seagullVolume;
    public float waterVolume;

    public CinemachineVirtualCamera vCam;



    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);
            audioSourceSeagull.volume = seagullVolume;
            audioSourceSeagull.Play();
            audioSourceWater.volume = waterVolume;
            audioSourceWater.Play();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            audioSourceSeagull.Stop();
            audioSourceWater.Stop();
            vCam.gameObject.SetActive(false);


        }

    }
}
