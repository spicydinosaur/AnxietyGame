using NavMeshPlus.Components;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class GameManager : PersistentSingleton<GameManager>


{
    public static GameObject gameManagerObject;
    [ShowNonSerializedField]
    public static bool tutorialComplete = true;
    [ShowNonSerializedField]
    public static bool breakdownComplete = true;


    public List<GameObject> LightsOnPuzzleObjects = new List<GameObject>();

    public bool audioStopped;
    public float chimeTimer;

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

    public Vector3 heroScaleAdult = new Vector3(.75f, .75f, 0.75f);
    public Vector3 heroScaleChild = new Vector3(.5f, .5f, 0.5f);

    public GameObject deathSceneBedPretutorial;
    public GameObject deathSceneBedPreBreakdown;
    public GameObject deathSceneBedPostBreakdown;

    public GameObject deathScenePillowsPretutorial;
    public GameObject deathScenePillowsPreBreakdown;
    public GameObject deathScenePillowsPostBreakdown;


    public void Start()
    {
        hero = GameObject.Find("Hero");
        player = hero.GetComponent<Player>();
        if  (SceneManager.GetActiveScene().name == "DeathCutscene")
        {
            if (breakdownComplete)
            {

                hero.transform.position = postBreakdownVector; 
                hero.transform.localScale = heroScaleAdult;
                eyesPostBreakdown.SetActive(true);
                deathSceneBedPostBreakdown.GetComponent<SpriteRenderer>().sortingLayerName = "LitDuringSleepSceneFront";
                deathScenePillowsPostBreakdown.GetComponent<SpriteRenderer>().sortingLayerName = "LitDuringSleepSceneBehind";

            }
            else if (tutorialComplete)
            {
                hero.transform.position = preBreakdownVector;
                hero.transform.localScale = heroScaleAdult;
                eyesPreBreakdown.SetActive(true);
                deathSceneBedPreBreakdown.GetComponent<SpriteRenderer>().sortingLayerName = "LitDuringSleepSceneFront";
                deathScenePillowsPreBreakdown.GetComponent<SpriteRenderer>().sortingLayerName = "LitDuringSleepSceneBehind";
            }
            else
            {
                hero.transform.position = preTutorialVector;
                hero.transform.localScale = heroScaleChild;
                eyesPreTutorial.SetActive(true);
                deathSceneBedPretutorial.GetComponent<SpriteRenderer>().sortingLayerName = "LitDuringSleepSceneFront";
                deathScenePillowsPretutorial.GetComponent<SpriteRenderer>().sortingLayerName = "LitDuringSleepSceneBehind";
            }
        }
    }
    public void OnEnable()
    {
        gameManagerObject = gameObject;
        timer = 1f;
        allAudioSources = null;
        Lua.RegisterFunction("CopingModifier", this, SymbolExtensions.GetMethodInfo(() => CopingModifier((double)0)));

    }

    public void TeleportedSuccessfully()
    {
        //There will have to be checks for what scene to load in the future.
        SceneManager.LoadScene("Scene One Tutorial and Seaside Town");
        player.lookDirection.Set(0f, -.5f);
        if (breakdownComplete)
        {
            hero.transform.position = postBreakdownVector;
        }
        else if (tutorialComplete)
        {
            hero.transform.position = preBreakdownVector;
        }
        else
        {
            hero.transform.position = preTutorialVector;
        }

        player.PlayerHealth(player.maxHealth);
        player.PlayerMana(player.maxMana);
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
