using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using System;
using UnityEngine.Audio;

public class CameraConfinerLoopedAudio : MonoBehaviour
{
    public AudioSource[] loopedAudioSources;

    //public float audioSourceVolume;

    public AudioSource[] allAudioSources;

    //public float timer;

    public GameManager gameManager;

    public AudioMixer audioMixer;

    public string exposedParameter;

    public float durationFadeOut;
    public float targetVolumeFadeOut;

    public float durationFadeIn;
    public float targetVolumeFadeIn;






    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hero entered the collider" + gameObject.name);

            foreach (var lAudioS in loopedAudioSources)
            {

                lAudioS.Play();

            }

            StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameter, durationFadeIn, targetVolumeFadeIn));

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("hero exited the collider" + gameObject.name);
        }
    }
}
