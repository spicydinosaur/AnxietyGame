using NavMeshPlus.Components;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    //public NavMeshSurface Surface2D;


    public void OnEnable()
    {
        gameManagerObject = gameObject;
        timer = 1f;
        allAudioSources = null;
        Lua.RegisterFunction("CopingModifier", this, SymbolExtensions.GetMethodInfo(() => CopingModifier((double)0)));

    }

    /*void Start()
    {
        Surface2D.BuildNavMeshAsync();
    }
    */

    public void Update()
    {
        if (chimeTimer > 0)
        {
            chimeTimer -= Time.deltaTime;

        }
        //Below for NavMesh2D
        // Surface2D.UpdateNavMesh(Surface2D.navMeshData);

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



     void OnDisable()
     {
         // Note: If this script is on your Dialogue Manager & the Dialogue Manager is configured
         // as Don't Destroy On Load (on by default), don't unregister Lua functions.
         Lua.UnregisterFunction("CopingModifier"); // <-- Only if not on Dialogue Manager.
     }

     public void CopingModifier(double copingAmount)
     {
        Debug.Log("CopingModifier activated with the modifier " + copingAmount);
         Player.instance.PlayerMana((float)copingAmount);
     }
}
