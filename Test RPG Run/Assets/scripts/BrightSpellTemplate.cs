using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Serialization;
using UnityEngine.U2D;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.Rendering.Universal;


public class BrightSpellTemplate : SpellTemplate


{
 

    public GameObject activeLightPool;
    public int numberOfPools;
    //public GameObject lastPoolInList;
    public bool forLoopFoundDisabledPoolToImplement = false;
    public List<GameObject> instantiatedPoolsTemp = new List<GameObject>(4);
    public float mouseDistance;
    //public Light2D lightGameObject;
    //public GameObject useThisLightPool;





    public override void castSpell()
    {


        int layerMask = LayerMask.GetMask("Scenery", "Enemy");

        heroTransform = hero.transform.position;

        Vector2 heroTransform2D = new Vector2(heroTransform.x, heroTransform.y);
        Debug.Log("pre ScreenToWorldPoint location for heroTransform2D = " + heroTransform2D + ".");
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.farClipPlane));

        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        Debug.Log("pre ScreenToWorldPoint location for mousePos2D = " + mousePos2D + ".");

        Vector3 direction3D = (mousePos3D - heroTransform).normalized;
        direction2D = (mousePos2D - heroTransform2D).normalized;
        Debug.Log("direction has coords " + direction2D + ".");

        Debug.Log("heroTransform2D registered at " + heroTransform2D + ". mousePos registered at " + mousePos + ".");

        Debug.Log("rayCastDistance set at " + rayCastDistance + ".");


        RaycastHit2D hit = Physics2D.Raycast(heroTransform2D, direction2D, rayCastDistance, layerMask);

        if (hit.collider != null)
        {
            Vector3 hit3D = new Vector3(hit.point.x, hit.point.y, 0f);
            Debug.Log("pre camera hit3D value is " + hit3D + ", hit.point value is " + hit.point + ".");
            hit3D = Camera.main.ScreenToWorldPoint(hit3D);
            Debug.Log("post camera hit3D value is " + hit3D + ".");
            gameObject.transform.position = hit3D;
            gameObject.GetComponent<Animator>().SetBool("isCasting", true);


            Vector3 point3D = new Vector3(hit3D.x, hit3D.y, hit3D.z);

            if (hit.collider.CompareTag("Enemy") == true)
            {

                hit.collider.GetComponent<NPCHealth>().damageNPCHealth(damageAmount);
                Debug.Log("hit enemy : " + hit.collider.name + ". Spell should be " + spellHolderScript.currentSpell + ". Collision occurred at " + hit.point);

            }

            else if (hit.collider.CompareTag("Scenery") == true)
            {

                Debug.Log("hit Something : " + hit.collider.name + " spell should be " + spellHolderScript.currentSpell + ". Collision occurred at " + hit.point);
                Debug.Log("hit tag  : " + hit.collider.tag);
            }

            currentCastDownTime = castDownTime;
            spellHolderScript.currentCastDownTime = currentCastDownTime;
            spellHolderScript.globalCastDownTime = globalCastDownTime;

            spellIconMask.fillAmount = 1f;

        }

        else

        {
            Debug.Log("no collider hit, moving on to making a light pool.");

            for (int i = 0; i < numberOfPools; i++)
            {
                Debug.Log("for statement for the bright spell cycling");

                if (spellHolderScript.instantiatedPools[i].activeSelf == false)
                {
                    Debug.Log("found inactive pool.");
                    activeLightPool = spellHolderScript.instantiatedPools[i];
                    forLoopFoundDisabledPoolToImplement = true;
                    break;
                }

            }

            instantiatedPoolsTemp.Clear();
            instantiatedPoolsTemp = new List<GameObject>(spellHolderScript.instantiatedPools.Capacity);

            if (forLoopFoundDisabledPoolToImplement == false)
            {

                Debug.Log("No pools are disabled. Recycling pool.");
                activeLightPool = spellHolderScript.instantiatedPools[0];

                spellHolderScript.instantiatedPools.Remove(activeLightPool);
                instantiatedPoolsTemp.AddRange(spellHolderScript.instantiatedPools);
                instantiatedPoolsTemp.Insert(3, activeLightPool);

            }
            else
            {
                Debug.Log("using inactive pool.");
                forLoopFoundDisabledPoolToImplement = false;
                spellHolderScript.instantiatedPools.Remove(activeLightPool);
                var t = 0;
                for (int i = 0; i < numberOfPools - 1; i++)
                {

                    if (spellHolderScript.instantiatedPools[i] != null && spellHolderScript.instantiatedPools[i] != activeLightPool)
                    {

                        instantiatedPoolsTemp.Insert(t, spellHolderScript.instantiatedPools[i]);
                    }
                    t++;
                }
                instantiatedPoolsTemp.Insert(3, activeLightPool);

            }


            spellHolderScript.instantiatedPools.Clear();
            spellHolderScript.instantiatedPools.AddRange(instantiatedPoolsTemp);
            instantiatedPoolsTemp.Clear();

            mouseDistance = Vector2.Distance(heroTransform2D, mousePos);

            if (mouseDistance < rayCastDistance)
            {
                Vector3 point = new Vector3(mousePos3D.x, mousePos3D.y, mousePos3D.z);
                Debug.Log("point now converted to mousePos3d and located at " + point + ".");
        }
            else
            {
                Vector3 point = new Vector3(heroTransform.x + direction3D.x, heroTransform.y + direction2D.y, 0f) * rayCastDistance;
                Debug.Log("point now converted to (heroTransform2D + direction2D * rayCastDistance) and located at " + point + ".");
            }

            //I need to figure out a way to drop the lightpools closer to the hero than the raycastdistance if the mouse clicks somewhere closer than it.
            //Consulting Donut for math tutoring!           
            activeLightPool.transform.position = point;
            activeLightPool.SetActive(true);

            Debug.Log("Pool of light should be at " + point);

            spellHolderScript.globalCastDownTime = globalCastDownTime;
            spellHolderScript.currentCastDownTime = castDownTime;
            currentCastDownTime = castDownTime;


            spellIconMask.fillAmount = 1f;


        }
    }
}