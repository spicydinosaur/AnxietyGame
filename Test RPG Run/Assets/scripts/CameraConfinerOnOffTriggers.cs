 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfinerOnOffTriggers : MonoBehaviour
{

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

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);
            
        }

    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(false);
            StartCoroutine(FadeOutAudio());
            
        }

    }

}
