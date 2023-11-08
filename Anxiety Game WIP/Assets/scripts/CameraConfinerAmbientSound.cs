using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfinerAmbientSound : MonoBehaviour
{
    //This script is not currently being worked on until I get ambient sounds that aren't looping for whatever reason I need them for. It is out of date and needs to be redone
    //by running things through the AudioMixer and fading things out and then new things in.

    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public float songVolume;

    public int randomNumber;

    public float maxTimeUntilNextSound;

    public float timeUntilNextSound;

    public CinemachineVirtualCamera vCam;

    public AudioSource[] allAudioSources;
    private AudioListener listener;
    public GameManager gameManager;


    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            timeUntilNextSound = maxTimeUntilNextSound;
            vCam.gameObject.SetActive(true);
            randomNumber = Random.Range(0, audioClips.Length -1);
            audioSource.PlayOneShot(audioClips[randomNumber], songVolume);
        }
    }

    public void Update()
    {
        if (vCam.gameObject.activeSelf == true)
        {
            if (timeUntilNextSound <= 0f)
            {
                randomNumber = Random.Range(0, audioClips.Length - 1);
                audioSource.PlayOneShot(audioClips[randomNumber], songVolume);
                timeUntilNextSound = maxTimeUntilNextSound;
            }
            else
            {
                timeUntilNextSound -= Time.deltaTime;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.StopAllAudio();
        }

    }

}

