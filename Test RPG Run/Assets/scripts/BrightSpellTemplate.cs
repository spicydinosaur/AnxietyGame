using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Serialization;
using UnityEngine.U2D;


public class BrightSpellTemplate : SpellTemplate


{
 

    public GameObject activeLightPool;
    public int numberOfPools = 4;

    public GameObject lastPoolInList;

    //Should always return true unless the foreach loop runs to check and assure that there are no active pools.
    public bool hasPoolBeenDisabled = true;

    public Light lightGameObject;




    void Start()
    {

    }


    public override void castSpell()
    {
        int layerMask = LayerMask.GetMask("Scenery", "Enemy");

        heroTransform = hero.transform.position;
        direction = (worldPos - heroTransform).normalized;


        RaycastHit2D hit = Physics2D.Raycast(heroTransform, direction, rayCastDistance, layerMask);

        if (hit.collider != null)
        {

            if (hit.collider.CompareTag("Enemy") == true)
            {

                gameObject.transform.position = hit.transform.position;
                gameObject.GetComponent<Animator>().SetBool("isCasting", true);

                point = hit.point;

                hit.collider.GetComponent<NPCHealth>().damageNPCHealth(damageAmount);
                Debug.Log("hit enemy : " + hit.collider.name + ". Spell should be " + spellHolderScript.currentSpell + ". Collision occurred at " + hit.point);

            }

            else if (hit.collider.CompareTag("Scenery") == true)
            {

                gameObject.transform.position = hit.transform.position;
                gameObject.GetComponent<Animator>().SetBool("isCasting", true);

                point = hit.point;

                Debug.Log("hit Something : " + hit.collider.name + " spell should be " + spellHolderScript.currentSpell + ". Collision occurred at " + hit.point);
                Debug.Log("hit tag  : " + hit.collider.tag);
            }

            else
            {

                for (int i = 0; i < numberOfPools; i++)
                {

                    if (spellHolderScript.instantiatedPools[i].activeSelf == false)
                    {
                        Debug.Log("found inactive pool, recycling.");
                        spellHolderScript.instantiatedPools[i].SetActive(true);
                        activeLightPool = spellHolderScript.instantiatedPools[i];
                        lastPoolInList = spellHolderScript.instantiatedPools[numberOfPools];
                        //shuffle the order so that the oldest light gets used first
                        spellHolderScript.instantiatedPools.Insert(0, lastPoolInList);
                        spellHolderScript.instantiatedPools.Remove(activeLightPool);
                        spellHolderScript.instantiatedPools.Add(activeLightPool);
                        hasPoolBeenDisabled = false;
                        break;
                        
                    }

                    else
                    {
                        spellHolderScript.instantiatedPools[0].SetActive(true);
                        activeLightPool = spellHolderScript.instantiatedPools[0];
                        lastPoolInList = spellHolderScript.instantiatedPools[numberOfPools];
                        //shuffle the order so that the oldest light gets used first
                        spellHolderScript.instantiatedPools.Insert(0, lastPoolInList);
                        spellHolderScript.instantiatedPools.Remove(activeLightPool);
                        spellHolderScript.instantiatedPools.Add(activeLightPool);
                        hasPoolBeenDisabled = false;
                    }
                }

                point = heroTransform + direction * rayCastDistance;
                activeLightPool.transform.position = point;

                Debug.Log("Nothing hit. pool of light should be at " + point);

            }

            currentCastDownTime = castDownTime;
            spellHolderScript.currentCastDownTime = currentCastDownTime;
            spellHolderScript.globalCastDownTime = globalCastDownTime;

            spellIconMask.fillAmount = 1f;

        }

        else

        {

            for (int i = 0; i < numberOfPools; i++)
            {


                if (spellHolderScript.instantiatedPools[i].activeSelf == false)
                {
                    Debug.Log("found inactive pool, recycling.");
                    activeLightPool = spellHolderScript.instantiatedPools[i];
                    spellHolderScript.instantiatedPools[i].SetActive(true);
                    activeLightPool = spellHolderScript.instantiatedPools[0];
                    lastPoolInList = spellHolderScript.instantiatedPools[numberOfPools];
                    //shuffle the order so that the oldest light gets used first
                    spellHolderScript.instantiatedPools.Insert(0, lastPoolInList);
                    spellHolderScript.instantiatedPools.Remove(activeLightPool);
                    spellHolderScript.instantiatedPools.Add(activeLightPool);
                    hasPoolBeenDisabled = false;
                    break;

                }
                else
                {
                    Debug.Log("Pool is not enabled.");
                    spellHolderScript.instantiatedPools[0].SetActive(true);
                    activeLightPool = spellHolderScript.instantiatedPools[0];
                    lastPoolInList = spellHolderScript.instantiatedPools[numberOfPools];
                    //shuffle the order so that the oldest light gets used first
                    spellHolderScript.instantiatedPools.Insert(0, lastPoolInList);
                    spellHolderScript.instantiatedPools.Remove(activeLightPool);
                    spellHolderScript.instantiatedPools.Add(activeLightPool);
                    hasPoolBeenDisabled = false;
                }
            }

            point = heroTransform + direction * rayCastDistance;
            activeLightPool.transform.position = point;

            Debug.Log("Nothing hit. pool of light should be at " + point);

            spellHolderScript.globalCastDownTime = globalCastDownTime;
            spellHolderScript.currentCastDownTime = castDownTime;
            currentCastDownTime = castDownTime;


            spellIconMask.fillAmount = 1f;

            Debug.Log("Nothing hit. pool of light should be at " + point);

        }
    }
}