using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class TutorialPixieFightEntryTrigger : MonoBehaviour
{
    public Camera mainCam;

    public float lightIntensity;
    public GameObject hero;
    public TutorialPixieBossTimeline timelineScript;
    //public CinemachineTrackedDolly dollyTrack;

    public void Start()
    {
        mainCam = Camera.main;
        hero = GameObject.Find("Hero");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainCam.GetComponent<Light2D>().intensity = lightIntensity;
            if (lightIntensity < 1f)
            {
                hero.GetComponentInChildren<Light2D>().enabled = true;
            }
            else
            {
                hero.GetComponentInChildren<Light2D>().enabled = false;
            }
            /*if (!timelineScript.panCutscenePlayed)
            {
                timelineScript.DirectorPlayed();
            }*/
            
        }

    }

}
