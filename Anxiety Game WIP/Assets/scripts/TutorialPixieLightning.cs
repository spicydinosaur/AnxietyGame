using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEditor;


public class TutorialPixieLightning : MonoBehaviour
{
    //This script resides on a gameobject that comes into creation when a spell like Smite or Flame is cast.
    //this needs to be changed to be line of sight to continue charging.


    public GameObject spellCaster;
    public Vector3 casterTransform;

    public Vector3 direction;
    public float speed;
    public float maxDistance;
    public float currentDistance;

    public Animator animator;
    public AudioSource spellSound;

    //The below means I'm not typing the layer values over and over again as strings and potentially mispelling something here or there.
    public enum LayersInMask { Player }
    [SerializeField]
    public List<LayersInMask> layerMasksToHit;
    [SerializeField]

    public List<string> combinedLayerString;
    public float healthAmount;
    public float manaAmount;
    public Vector3 directionToTarget;
    public GameObject targetToHit;
    public Vector3 targetPosition;
    public GameObject hero;
    public Player player;
    public float distanceIncrements;

    void Awake()
    {
        hero = GameObject.Find("Hero");
        player = hero.GetComponent<Player>();
        if (targetToHit == null) { targetToHit = hero; }
        spellCaster = GetComponentInParent<GameObject>();
        casterTransform = spellCaster.transform.position;
        targetPosition = targetToHit.transform.position;
        transform.position = spellCaster.transform.position;
        directionToTarget = targetPosition - transform.position;
        distanceIncrements = Mathf.RoundToInt(maxDistance / 5);
        animator = gameObject.GetComponent<Animator>();
        spellSound = GetComponent<AudioSource>();
        if (speed == 0f) { speed = 1f; }

        foreach (LayersInMask value in layerMasksToHit)
        {
            combinedLayerString.Add(value.ToString());
        }

        string completeList = string.Join(", ", combinedLayerString.ToArray());
        //Debug.Log("This debug is firing in the Awake function: the variable completeList = " + completeList + ".");

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var collidingObject = collision.gameObject;
        string collidingObjectString = collidingObject.tag.ToString();

        string completeList = string.Join(", ", combinedLayerString.ToArray());
        

        foreach (string checkLayer in combinedLayerString)
        {

            if (collidingObject.CompareTag(checkLayer))
            {
                //Debug.Log("collidingObject.CompareTag(checkLayer) = " + collidingObject.CompareTag(checkLayer));
             if (collidingObjectString == "Player")
                {
                    if (healthAmount != 0f)
                    {
                        //healthAmount must be a negative in the UI for it to damage, otherwise it will heal!
                        collidingObject.GetComponent<Player>().PlayerHealth(healthAmount);
                    }
                    if (manaAmount != 0f)
                    {
                        //manaAmount must be a negative in the UI for it to damage, otherwise it will heal!
                        collidingObject.GetComponent<Player>().PlayerMana(manaAmount);
                    }
                }
     
                break;
            }
        }


    }


    public void OnSpellEnd()
    {
        animator.SetBool("isCast", false);

    }

    public void OnLastCastEnd()
    {
        spellSound.Stop();
        Destroy(gameObject);
    }

    public void LightningCrack()
    {
        spellSound.Stop();
        spellSound.Play(0);
    }


    public void Update()
    {
        if (animator.GetBool("isCast") == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            currentDistance = math.distance(casterTransform, transform.position);

            if (Math.Abs(Math.Round(currentDistance / distanceIncrements) - currentDistance / distanceIncrements) < 0.01)
            {
                if (Math.Abs(Math.Round(currentDistance / distanceIncrements) - 5) < 0.01)
                {
                    animator.SetBool("isCast", true);
                    animator.SetBool("lastCast", true);
                }
                else if (Math.Abs(Math.Round(currentDistance / distanceIncrements) - 1) < 0.01
                    || Math.Abs(Math.Round(currentDistance / distanceIncrements) - 2) < 0.01
                    || Math.Abs(Math.Round(currentDistance / distanceIncrements) - 3) < 0.01
                    || Math.Abs(Math.Round(currentDistance / distanceIncrements) - 4) < 0.01)
                {
                    animator.SetBool("isCast", true);
                }
            }
        }
    }

}

