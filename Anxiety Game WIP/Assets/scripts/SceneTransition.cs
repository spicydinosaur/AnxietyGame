using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class SceneTransition : MonoBehaviour
{

    public GameObject destination;
    public Vector2 lookDirectionOnEntry;
    public Player player;

    public GameManager gameManager;
    public GameObject collidingObject;

    public Camera mainCam;

    public float lightIntensity;
    public GameObject hero;
    public Canvas bossHUDElements;
    public TutorialBossPixie pixieScript;


    public void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCam = Camera.main;
        hero = GameObject.Find("Hero");


    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collidingObject = collider.gameObject;
            player = collidingObject.GetComponent<Player>();

            if (!gameManager.hasTransitioned)
            {
                gameManager.hasTransitioned = true;
                gameManager.LoadInternalLevel(destination.transform.position, collidingObject);
                player.lookDirection = new Vector2(lookDirectionOnEntry.x, lookDirectionOnEntry.y);
         
            }
            else
            {
                gameManager.hasTransitioned = false;

            }
        }
    }
}
