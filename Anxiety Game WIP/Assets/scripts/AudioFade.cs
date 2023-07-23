using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioFade : MonoBehaviour
{
    public AudioMixer audioMixer;

    public string exposedParameter;

    public float durationFadeOut;
    public float targetVolumeFadeOut;

    public float durationFadeIn;
    public float targetVolumeFadeIn;




    public void OnTriggerEnter2D(Collider2D collider)
    {


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


    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && collider.GetComponent<Player>().transitioningToScene == false)
        {
            FadeMixerGroup.exposedParameterFadeOut = exposedParameter;
        }
        Debug.Log("hero exited the collider" + gameObject.name);
    }
}
