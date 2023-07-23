using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class SpellTemplate : MonoBehaviour
{

    public float manaCost;
    //above must be negative to cost the player rather than give mana.
    public float castDownTime;
    public float globalCastDownTime;
    public float currentCastDownTime;

    public GameObject hero;
    public Player player;

    public Vector3 mousePos;
    public Vector3 worldPos;
    public Vector3 direction;
    public Vector3 directionNormalized;
    public Vector3 transformDirection;
    public Vector3 casterTransform;

    public Vector3 spellStartPoint;
    public Quaternion quaternionRotation;
    public Angle angle;
    public float distance;

    public float mouseDistance;

    public Image spellIconMask;



    public Camera mainCam;

    //public GameObject hitObject;
    public GameObject projectilePrefab;
    public GameObject currentProjectile;
    public GameObject spellCaster;
    public SpellTimer spellTimer;
    // The projectile script holds the sound of the projectile, whereas the spell gameobject holds the audio for the spell going off. The FizzleSpell game object holds the sound for the fizzle, and that is its only actual purpose.


    public virtual void Awake()
    {


        currentCastDownTime = 0f;
        player = hero.GetComponent<Player>();


    }





    public virtual void Update()
    {

        if (currentCastDownTime > 0f)
        {
            currentCastDownTime -= Time.deltaTime;


        }
        else if (currentCastDownTime < 0f)
        {
            currentCastDownTime = 0f;

        }
    }





    public virtual void castSpell()
    {

        casterTransform = spellCaster.transform.position;
        direction = mousePos - casterTransform;
        directionNormalized = direction.normalized;
        spellStartPoint = new Vector3(casterTransform.x + directionNormalized.x, casterTransform.y + directionNormalized.y, casterTransform.z + directionNormalized.z);
        distance = Vector3.Distance(casterTransform, mousePos);
        currentProjectile = Instantiate(projectilePrefab, spellStartPoint, Quaternion.identity);
        currentProjectile.SetActive(true);

        var projectileScript = currentProjectile.GetComponent<SpellProjectile>();
        projectileScript.spellTemplate = this;
        projectileScript.spellCaster = spellCaster;


        if (spellCaster == hero)
        {

            //player.spellIconMask.fillAmount = 1f;
            //below must be negativw or player will gain mana;
            player.PlayerMana(manaCost);
            currentCastDownTime = castDownTime;
            spellTimer.globalCastDownTime = globalCastDownTime;


        }



    }


    //Work on this later! Gamepad controls



}




