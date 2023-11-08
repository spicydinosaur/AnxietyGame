using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using UnityEditor;


public class SpellProjectile : MonoBehaviour
{
    //This script resides on a gameobject that comes into creation when a spell like Smite or Flame is cast.


    public GameObject parentSpell;
    public Vector3 spellStartPoint;
    public GameObject spellCaster;
    public SpellTemplate spellTemplate;

    public Vector3 direction;
    public Vector3 casterTransform;
    public float speed;
    public float maxDistance;
    public float currentDistance;

    public Animator animator;
    // The projectile has an audiosource that holds the sound of the projectile, whereas the spell gameobject holds the audiosource for the spell going off. The FizzleSpell game object holds the sound for the fizzle, and that is its only actual purpose.
    public AudioSource projectileSound;
    public AudioSource spellSound;
    public AudioSource fizzleSound;

    //The below means I'm not typing the layer values over and over again as strings and potentially mispelling something here or there.
    public enum LayersInMask {Enemy, Scenery, NPC, Player, Allies}
    [SerializeField]
    public List<LayersInMask> layerMasksToHit;
    [SerializeField]

    public List<string> combinedLayerString; 
    public float healthAmount;
    public float manaAmount;
    public Vector3 directionToTarget;
    public float angle;


    public Vector3 mousePos;




    void Awake()
    {

        mousePos = spellTemplate.mousePos;
        maxDistance = spellTemplate.distance;
        directionToTarget = mousePos - transform.position;
        spellStartPoint = transform.position;

        // Calculate the angle of the rotation for the projectile and convert from radians to degrees
        angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Rotate towards where the mouse was clicked from where the projectile was instantiated.
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        animator = gameObject.GetComponent<Animator>();


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
                if (collidingObjectString == "Enemy")
                {
                    if (healthAmount != 0f)
                    {
                        //healthAmount must be a negative in the UI for it to damage, otherwise it will heal!
                        collidingObject.GetComponent<NPCHealth>().ChangeNPCHealth(healthAmount);
                        //Below will need to be tested with a variety of enemies, as it may make the spell warp from one place as its projectile,
                        //for too much of a distance to where the spell effect is seen.
                        transform.position = collidingObject.transform.position;
                    }
                    if (manaAmount != 0f)
                    {
                        //possibly useful later.
                    }
                }
                else if (collidingObjectString == "Player")
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
                else if (collidingObjectString == "Allies")
                {
                    if (healthAmount != 0f)
                    {
                        //healthAmount must be a negative in the UI for it to damage, otherwise it will heal!
                        collidingObject.GetComponent<NPCHealth>().ChangeNPCHealth(healthAmount);
                    }
                    if (manaAmount != 0f)
                    {
                        //possibly useful later.
                    }
                }
                SpellHit(collision.gameObject); break;
            }
        }

       
    }

    public void SpellHit(GameObject collidingGameObject)
    {

        projectileSound.Stop();
        spellSound.Play(0);
        animator.SetBool("hitTarget", true);
        animator.SetBool("hasFizzled", false);

    }

    public void rotateSpellToNormal()
    {
        transform.rotation = Quaternion.identity;
    }

    public void OnSpellEnd()
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        currentDistance = math.distance(spellStartPoint, transform.position);
        if (gameObject.transform.position == mousePos || currentDistance >= maxDistance && animator.GetBool("hitTarget") == false)
        {
            animator.SetBool("hasFizzled", true);
            projectileSound.Stop();
            fizzleSound.Play(0);
        }
        else if (animator.GetBool("hitTarget") ==false 
            && animator.GetBool("hasFizzled") == false) 
        {

            // Move towards the target
            transform.position = Vector2.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);

        }
    }
}
