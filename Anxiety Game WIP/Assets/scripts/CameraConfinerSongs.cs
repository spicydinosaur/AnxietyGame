using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CameraConfinerSongs : MonoBehaviour
{
    public float timeUntilNextSound;
    public GameManager gameManager;
    public GameObject hero;

    public AudioSource[] IntermittentAudioSources;

    //public float audioSourceVolume;

    public AudioSource[] allAudioSources;

    //public float timer;

    public AudioMixer audioMixer;

    public string exposedParameter;

    public float durationFadeOut;
    public float targetVolumeFadeOut;

    public float durationFadeIn;
    public float targetVolumeFadeIn;



    public void Start()
    {
        hero = GameObject.Find("Hero");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("hero entered the collider" + gameObject.name);

            foreach (var lAudioS in IntermittentAudioSources)
            {

                lAudioS.Play(0);
                timeUntilNextSound = 600f;

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
        //Debug.Log("hero exited the collider" + gameObject.name);
    }

    // Start is called before the first frame update

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (timeUntilNextSound <= 0)
            {
                foreach (var lAudioS in IntermittentAudioSources)
                {

                    lAudioS.Play(0);
                    timeUntilNextSound = 600f;

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
            else
            {
                timeUntilNextSound -= Time.deltaTime;
            }
        }
        
    }

}
