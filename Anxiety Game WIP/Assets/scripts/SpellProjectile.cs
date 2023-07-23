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

    public Vector2 newPosition;


    public enum LayersInMask {Enemy, Scenery, NPC, Player, Allies}
    [SerializeField]
    public List<LayersInMask> layerMasksToHit;
    [SerializeField]
    public int positionInLayerList;

    public List<string> combinedLayerString; 
    public float healthAmount;
    public float manaAmount;
    public Vector3 directionToTarget;
    public float angle;


    public Vector3 mousePos;




    void Awake()
    {
        positionInLayerList = 0;
        mousePos = spellTemplate.mousePos;

        directionToTarget = mousePos - transform.position;

        // Calculate the angle in radians
        angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Rotate towards the target
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //layerMask = LayerMask.GetMask(combinedLayerString);
        //direction = spellTemplate.directionNormalized;
        //projectileSound.Play(0);
        spellStartPoint = transform.position;
        animator = gameObject.GetComponent<Animator>();
        //animator.SetBool("isOnTheMove", true);
        //animator.SetBool("hitTarget", false);
        //animator.SetBool("hasFizzled", false);
        //Debug.Log("hit.point = " + hit.point + ".");

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
        string collidingObjectString = collidingObject.layer.ToString();

        string completeList = string.Join(", ", combinedLayerString.ToArray());
        //Debug.Log("This debug is firing in the OnTriggerEnter2D function collidingObject.name = " + collidingObject + ". the tag of the collidingObject =  " + collidingObject.tag + " positionInLayerList");
        //Debug.Log("This debug is firing in the OnTriggerEnter2D function. The variable completeList = " + completeList + ".");
        //Debug.Log("collidingObjectString = " + collidingObjectString);
        
        foreach (string layer in combinedLayerString)
        {

            if (collidingObject.CompareTag(layer))
            {
                //Debug.Log("collidingObject.CompareTag(layer) = " + collidingObject.CompareTag(layer));
                speed = 0f;
                if (collidingObjectString == "Enemy")
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
        //it's possible there will be colliders that cause the projectile turned spell effect to "jump" from a small distance to the transform, it may be necessary to have colliders specifically for spells that have their own layers in the future.
        projectileSound.Stop();
        spellSound.Play(0);
        gameObject.transform.rotation = Quaternion.identity;
        animator.SetBool("hitTarget", true);
        animator.SetBool("hasFizzled", false);


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
