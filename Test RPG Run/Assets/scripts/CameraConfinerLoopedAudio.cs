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
            FadeMixerGroup.exposedParameterFadeIn = exposedParameter;
            if (FadeMixerGroup.exposedParameterFadeOut != FadeMixerGroup.exposedParameterFadeIn && FadeMixerGroup.exposedParameterFadeOut != null)
            {
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, FadeMixerGroup.exposedParameterFadeIn, durationFadeIn, targetVolumeFadeIn));
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, FadeMixerGroup.exposedParameterFadeOut, durationFadeOut, targetVolumeFadeOut));
            }
            else if (FadeMixerGroup.exposedParameterFadeOut == null)
            {
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, FadeMixerGroup.exposedParameterFadeIn, durationFadeIn, targetVolumeFadeIn));
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
            if (collider.gameObject.CompareTag("Player") && collider.GetComponent<Player>().transitioningToScene == false)
            {
                FadeMixerGroup.exposedParameterFadeOut = exposedParameter;
            }
            Debug.Log("hero exited the collider" + gameObject.name);
    }
}
