using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : PersistentSingleton<GameManager>


{
    public static GameObject gameManagerObject;
    public bool tutorialComplete = false; //temporary permanent state until we have a way to save and keep it as a peristent variable
    public bool breakdownComplete = false;//same as above

    public List<GameObject> LightsOnPuzzleObjects = new List<GameObject>();

    public bool audioStopped;
    public float chimeTimer;

    public AudioSource[] allAudioSources;

    public int loopCount;

    public float timer;

    public void OnEnable()
    {
        gameManagerObject = gameObject;
        timer = 1f;
        allAudioSources = null;

    }

    public void Update()
    {
        if (chimeTimer > 0)
        {
            chimeTimer -= Time.deltaTime;

        }
    }

    public void StopAllAudio()
    {
        Debug.Log("StopAllAudio function fired!");
        allAudioSources = FindObjectsOfType<AudioSource>();

        var traceString = "Audio Sources: \n";

        foreach (var audioS in allAudioSources)
        {
            audioS.Stop();
            traceString += audioS.name + "\n";
        }
        Debug.Log(traceString);

    }


}
