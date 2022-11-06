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
            else if (chimeTimer <= 0)
            {
                clockTick.Stop();
                clockChime.Play();
                chimeTimer = 300;
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(false);
            StartCoroutine(FadeOutAudio());

        }

    }
}
