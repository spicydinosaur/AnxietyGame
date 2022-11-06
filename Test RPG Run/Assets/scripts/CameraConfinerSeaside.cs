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

    public AudioSource[] allAudioSources;
    private AudioListener listener;


    private void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (var audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    public IEnumerator FadeOutAudio()
    {
        if (AudioListener.volume > 0.1f)
        {
            AudioListener.volume -= 0.1f;
            yield return new WaitForSeconds(.1f);
        }
        else
        {
            StopAllAudio();
            AudioListener.volume = 1f;
            StopCoroutine(FadeOutAudio());
        }

    }

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

            StartCoroutine(FadeOutAudio());
            vCam.gameObject.SetActive(false);


        }

    }
}
