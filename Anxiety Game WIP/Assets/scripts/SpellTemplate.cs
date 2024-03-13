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

    public float distance;

    public float mouseDistance;

    public Camera mainCam;

    public GameObject projectilePrefab;
    public GameObject currentProjectile;
    public GameObject spellCaster;
    public SpellTimer spellTimer;
    // The projectile script holds the sound of the projectile, whereas the spell gameobject holds the audio for the spell going off.
    // The FizzleSpell game object holds the sound for the fizzle, (and that is its only actual purpose.)
    // All animations are handled by the projectile if the spell has one.
    public Animator animator;
    public Collider2D spellCollider2D;
    public Animator heroAnim;

    public AudioSource spellSound;
    public enum LayersInMask { Enemy, Scenery, NPC, Allies }
    [SerializeField]
    public List<LayersInMask> layerMasksToHit;
    [SerializeField]

    public List<string> combinedLayerString;
    public float healthAmount;
    public float manaAmount;

    public virtual void Awake()
    {


        currentCastDownTime = 0f;
        player = hero.GetComponent<Player>();
        mainCam = Camera.main;


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

            casterTransform = hero.transform.position;
            direction = mousePos - casterTransform;
            directionNormalized = direction.normalized;
            spellStartPoint = new Vector3(casterTransform.x + directionNormalized.x, casterTransform.y + directionNormalized.y, 0f);
            mouseDistance = Vector3.Distance(casterTransform, mousePos);
            currentProjectile = Instantiate(projectilePrefab, spellStartPoint, Quaternion.identity);
            currentProjectile.SetActive(true);

            var projectileScript = currentProjectile.GetComponent<SpellProjectile>();
            projectileScript.spellTemplate = this;
            projectileScript.spellCaster = hero;
            spellTimer.spellIconMask.fillAmount = 1f;
            //below must be negativw or player will gain mana;
            player.PlayerMana(manaCost);

            currentCastDownTime = castDownTime;

    }


    //Work on this later! Gamepad controls



}




