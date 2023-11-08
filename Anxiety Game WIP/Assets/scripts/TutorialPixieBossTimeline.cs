using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;

public class TutorialPixieBossTimeline : MonoBehaviour
{
    /*public PlayableDirector directorPanCamera;
    public PlayableDirector directorIntro;
    public PlayableDirector directorPhaseOne;
    public PlayableDirector directorPhaseTwo;
    public PlayableDirector directorPhaseThree;
    */
    public PlayerControls playerControls;
    public GameObject hero;
    public Canvas pixieHUDElements;
    public GameObject pixieBoss;
    public GameObject totem;
    public Player player;
    public List<PlayableDirector> directors;
    public int cutsceneCount;
    public bool panCutscenePlayed;

    public void Start()
    {
        /*directorIntro.played += DirectorPlayed;
        directorIntro.stopped += DirectorStopped;
        directorPhaseOne.played += DirectorPlayed;
        directorPhaseOne.stopped += DirectorStopped;
        directorPhaseTwo.played += DirectorPlayed;
        directorPhaseTwo.stopped += DirectorStopped;
        directorPhaseThree.played += DirectorPlayed;
        directorPhaseThree.stopped += DirectorStopped;*/
        hero = GameObject.Find("Hero");
        player = hero.GetComponent<Player>();
        cutsceneCount = 0;
        panCutscenePlayed = false;
    }


    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            DirectorPlayed();
        }
    }

    public void DirectorPlayed()
    {
        player.playerControls.Disable();
        directors[cutsceneCount].Play();

    }

    public void DirectorStopped()
    {
        player.playerControls.Enable();
        player.playerControls.UIControl.Disable();
        directors[cutsceneCount].Stop();
        if (cutsceneCount == 0) 
        {
            panCutscenePlayed = true;
        }
        if (cutsceneCount < 4)
        {
            cutsceneCount++;
        }

    }

    public void PixieAlmostAwake()
    {
        pixieBoss.GetComponent<SpriteRenderer>().sortingLayerName = "special effects";
        pixieBoss.transform.position = new Vector3(-63.53f, 12.44f, 0.0f);
        pixieBoss.GetComponent<Collider2D>().enabled = false;
        pixieBoss.SetActive(true);
    }

    public void PixieAwake()
    {
        pixieHUDElements.enabled = true;

    }

}