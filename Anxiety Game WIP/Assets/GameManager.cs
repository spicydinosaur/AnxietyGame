using NavMeshPlus.Components;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>


{
    public static GameObject gameManagerObject;
    public static bool tutorialComplete; 
    public static bool breakdownComplete;

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


    public void Start()
    {
        hero = GameObject.Find("Hero");
        player = hero.GetComponent<Player>();
        if  (SceneManager.GetActiveScene().name == "DeathCutscene")
        {
            if (breakdownComplete)
            {

                Vector3 pos = new Vector3(-64.92f, 95.15f, 0f);
                hero.transform.position = pos;
                hero.transform.localScale = new Vector3(.75f, .75f, 0.75f);
                eyesPostBreakdown.SetActive(true);

            }
            else if (tutorialComplete)
            {
                Vector3 pos = new Vector3(8.76f, 3.86f, 0f);
                hero.transform.position = pos;
                hero.transform.localScale = new Vector3(.75f, .75f, 0.75f);
                eyesPreBreakdown.SetActive(true);
            }
            else
            {
                Vector3 pos = new Vector3(-68.2f, 93.25f, 0);
                hero.transform.localPosition = pos;
                hero.transform.localScale = new Vector3(.5f, .5f, 0.5f);
                eyesPreTutorial.SetActive(true);
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

        SceneManager.LoadScene("Scene One Tutorial and Seaside Town");
        player.lookDirection.Set(0f, -.5f);
        if (breakdownComplete)
        {

            Vector3 pos = new Vector3(-64.92f, 95.15f, 0f);
            hero.transform.position = pos;

        }
        else if (tutorialComplete)
        {
            Vector3 pos = new Vector3(8.76f, 3.86f, 0f);
            hero.transform.position = pos;
        }
        else
        {
            Vector3 pos = new Vector3(-68.46f, 95.21f, 0f);
            hero.transform.position = pos;
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
