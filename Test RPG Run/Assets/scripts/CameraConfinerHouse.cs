using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;

public class CameraConfinerHouse : MonoBehaviour
{

    public AudioSource clockTick;
    public AudioSource clockChime;

    public GameManager timer;


    //this is for the seaside village house downstairs only

    public AudioSource[] allAudioSources;

    public AudioMixer audioMixer;

    public string exposedParameter;
    public float durationFadeIn;
    public float targetVolumeFadeIn;

    public string exposedParameterTick;
    public string exposedParameterChime;

    public float durationFadeInChime;
    public float targetVolumeFadeInChime;

    public float durationFadeOutChime;
    public float targetVolumeFadeOutChime;

    public float durationFadeInTick;
    public float targetVolumeFadeInTick;

    public float durationFadeOutTick;
    public float targetVolumeFadeOutTick;

    public float durationFadeOut;
    public float targetVolumeFadeOut;

    public void OnEnable()
    {
        timer.chimeTimer = 0f;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hero entered the collider" + gameObject.name);
            //StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameter, durationFadeIn, targetVolumeFadeIn));
            if (timer.chimeTimer <= 0)
            {
                FadeMixerGroup.exposedParameterFadeIn = exposedParameterChime;
                clockChime.Play();
                timer.chimeTimer = 300;
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameterChime, durationFadeInChime, targetVolumeFadeInChime));
            }
            else
            {
                FadeMixerGroup.exposedParameterFadeIn = exposedParameterTick;
                clockTick.Play();
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameterTick, durationFadeInTick, targetVolumeFadeInTick));
            }
            if (FadeMixerGroup.exposedParameterFadeOut != FadeMixerGroup.exposedParameterFadeIn && FadeMixerGroup.exposedParameterFadeOut != null)
            {
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, FadeMixerGroup.exposedParameterFadeOut, durationFadeOut, targetVolumeFadeOut));
            }
        }
    }



    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            if (!clockChime.isPlaying && !clockTick.isPlaying)
            {
                clockTick.Play();
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameterChime, durationFadeOutChime, targetVolumeFadeOutChime));
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameterTick, durationFadeInTick, targetVolumeFadeInTick));
            }
            else if (timer.chimeTimer <= 0)
            {
                //clockTick.Stop();
                clockChime.Play();
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameterTick, durationFadeOutTick, targetVolumeFadeOutTick));
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameterChime, durationFadeInChime, targetVolumeFadeInChime));
                timer.chimeTimer = 300;
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && collider.GetComponent<Player>().transitioningToScene == false)
        {
            if (clockTick.isPlaying)
            {
                exposedParameter = exposedParameterTick;
            }
            else
            {
                exposedParameter = exposedParameterChime;
            }
            FadeMixerGroup.exposedParameterFadeOut = exposedParameter;
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer, FadeMixerGroup.exposedParameterFadeOut, durationFadeOut, targetVolumeFadeOut));
        }
    }

}
