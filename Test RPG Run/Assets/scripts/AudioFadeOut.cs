using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioFadeOut : MonoBehaviour
{
    public AudioMixer audioMixer;

    public string exposedParameter;

    public float durationFadeOut;
    public float targetVolumeFadeOut;


    public void OnTriggerEnter2D(Collider2D collider)
    {


        if (collider.gameObject.CompareTag("Player") && collider.GetComponent<Player>().transitioningToScene == false)
        {
            StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameter, durationFadeOut, targetVolumeFadeOut));
        }

    }
}
