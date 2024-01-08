using NavMeshPlus.Components;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;

public class GameManager : PersistentSingleton<GameManager>


{
    public static bool tutorialHasRuinsKey = false;
    public static bool tutorialRuinsEntranceRevealed = false;
    public static bool tutorialRuinsStairsRevealed = false;
    public static bool tutorialRuinsThreeDoorOpened = false;
    public static bool tutorialPixieDefeated = false;
    public static bool tutorialFlameSpellObtained = false;
    public static bool tutorialFinalBossRevealed = false;
    public static bool tutorialFatherGone = false;
    public static bool tutorialComplete = false;
    public static bool breakdownComplete = false;
    public static bool heroRecovering;

    public GameObject ruinsEntrance;
    public GameObject ruinsStairs;
    public GameObject ruinsThreeDoor;
    public DungeonDoorSuccessfullyOpened ruinsDoorThreeScript;
    public Vector3 FlameSpellLootLoc;
    public GameObject ruinsRoomThreeExit;

    //more as we work ever towards completing the tutorial!

    public List<GameObject> LightsOnPuzzleObjects = new List<GameObject>();

    public bool audioStopped;
    public float chimeTimer;
    public AudioSource clockTick;
    public AudioSource clockChime;

    public AudioSource[] allAudioSources;

    public int loopCount;

    public float timer;

    public Player player;
    public GameObject hero;

    public GameObject eyesPreTutorial;
    public GameObject eyesPreBreakdown;
    public GameObject eyesPostBreakdown;

    public Vector3 preTutorialVector = new Vector3(-68.28f, 93.21f, 0f);
    public Vector3 preBreakdownVector = new Vector3(19.36f, 64.29f, 0f);
    public Vector3 postBreakdownVector = new Vector3(-64.21f, 93.13f, 0f);

    public Vector3 heroScaleAdult = new Vector3(.8f, .8f, 0.8f);
    public Vector3 heroScaleChild = new Vector3(.6f, .6f, 0.6f);

    public GameObject deathSceneBedPretutorial;
    public GameObject deathSceneBedPreBreakdown;
    public GameObject deathSceneBedPostBreakdown;

    public GameObject deathScenePillowsPretutorial;
    public GameObject deathScenePillowsPreBreakdown;
    public GameObject deathScenePillowsPostBreakdown;

    public Vector3 preTutorialPlayVector = new Vector3(-68.28f, 93.21f, 0f);
    public Vector3 preBreakdownPlayVector = new Vector3(19.36f, 64.29f, 0f);
    public Vector3 postBreakdownPlayVector = new Vector3(-64.21f, 93.13f, 0f);

    public GameObject fadeObject;
    public Animator cameraFadeAnim;
    public float fadeTime;
    public bool hasTransitioned;




 
    public void OnEnable()
    {
        timer = 1f;
        allAudioSources = null;
        Lua.RegisterFunction("CopingModifier", this, SymbolExtensions.GetMethodInfo(() => CopingModifier((double)0)));
       SceneManager.sceneLoaded += OnSceneLoaded;
    }


    public void TeleportedSuccessfully()
    {

        //There will have to be checks for what scene to load in the future.
        if (breakdownComplete || !tutorialComplete)
        {

            LoadNextLevel(0);

        }
        else if (tutorialComplete)
        {
            LoadNextLevel(1);

        }

    }

    public void Update()
    {
        if (chimeTimer > 0)
        {

            chimeTimer -= Time.deltaTime;

        }
               

    }

    public void LoadNextLevel(int sceneNumber)
    {
        StopAllAudio();
        StartCoroutine(LoadLevel(sceneNumber));
    }

    public void LoadInternalLevel(Vector3 destinationTransform, GameObject objectToMove)
    {
        //Fading Out is set here, but the animator triggers off of that state to set up the FadeIn for a new "scene."
        StopAllAudio();
        cameraFadeAnim.SetTrigger("InSceneFade");
        fadeObject.GetComponent<InternalTransition>().objectToMove = objectToMove;
        fadeObject.GetComponent<InternalTransition>().destinationTransform = destinationTransform;

    }


    IEnumerator LoadLevel(int levelIndex)
    {
        if (fadeTime == 0f)
        {
            fadeTime = 1f;
        }
        //FadeOut is set here, but the animator triggers off of the entry state to set up the FadeIn for a new scene.
        cameraFadeAnim.SetTrigger("FadeOut");

        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(levelIndex);


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

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hero = GameObject.Find("Hero");
        fadeObject = GameObject.Find("CameraFadeImage");
        cameraFadeAnim = fadeObject.GetComponent<Animator>();
        player = hero.GetComponent<Player>();

        if (scene.buildIndex == 2)
        {
            hasTransitioned = false;
            heroRecovering = true;

                if (breakdownComplete)
                {
                    Debug.Log("breakdownComplete = true this is activating on start for deathscene!");
                    hero.transform.position = postBreakdownVector;
                    hero.transform.localScale = heroScaleAdult;
                    eyesPostBreakdown.SetActive(true);

                }
                else if (tutorialComplete)
                {
                Debug.Log("tutorialComplete = true this is activating on start for deathscene!");
                hero.transform.position = preBreakdownVector;
                    hero.transform.localScale = heroScaleAdult;
                    eyesPreBreakdown.SetActive(true);

                }
                else
                {
                    Debug.Log("breakdownComplete and tutorialComplete != true this is activating on start for deathscene!");
                    hero.transform.position = preTutorialVector;
                    hero.transform.localScale = heroScaleChild;
                    eyesPreTutorial.SetActive(true);

                }

                clockTick.Play();
            
        }
        else if (heroRecovering)
        {
            heroRecovering = false;
            player.lookDirection.Set(0f, -.5f);
            player.PlayerHealth(player.maxHealth);
            player.PlayerMana(player.maxMana);
            player = hero.GetComponent<Player>();

            if (breakdownComplete)
            {
                hero.transform.position = postBreakdownPlayVector;
                hero.transform.localScale = heroScaleAdult;

            }
            else if (tutorialComplete)
            {
                hero.transform.position = preBreakdownPlayVector;
                hero.transform.localScale = heroScaleAdult;
            }
            else
            {
                Debug.Log("on load of scene 0 in GameManager script, preTotorialPlayVector = " + preTutorialPlayVector);
                hero.transform.position = preTutorialPlayVector;
                hero.transform.localScale = heroScaleChild;
            }

        }
        else
        {
            //other things to figure out when moving between scenes that are not implemented yet!
        }
    }

    public void ClockChimeForRecoveryScene()
    {
        clockTick.Stop();
        clockChime.Play();
    }

    public void OnDisable()
     {
         // Note: If this script is on your Dialogue Manager & the Dialogue Manager is configured
         // as Don't Destroy On Load (on by default), don't unregister Lua functions.
         Lua.UnregisterFunction("CopingModifier"); // <-- Only if not on Dialogue Manager.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

     public void CopingModifier(double copingAmount)
     {
        Debug.Log("CopingModifier activated with the modifier " + copingAmount);
         Player.instance.PlayerMana((float)copingAmount);
     }
}
