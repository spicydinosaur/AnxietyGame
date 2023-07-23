using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using Unity.Mathematics;
//using System.Numerics;

public class TutorialBossFairy : MonoBehaviour
{

    public GameObject[] lightPillars;
    public GameObject[] braziers;

    public int totalLitLightPillars = 0;

    public int phase = 0;

    public GameObject totem;

    public GameObject flamesLootDrop;

    public GameObject hero;

    public NPCHealth healthScript;

    public Vector3 changePhaseLocation;

    void OnEnable()
    {

    }


    public void Attack()
    {

    }

    public void PillarMove()
    {

    }

    public void LightBrazier()
    {

    }

    private void MakeDecisions()
    {
        /* move light pillars (closest first?)
         * light braziers (closest first?) - 3rd phase only
         * attack player
         
         * the boss fairy reevaluates her actions when all four pillars are lit and closest to the totem, any brazier is converted to light (with an increasing chance to handle that for each brazier converted,) any time she takes damage

         */

        if (phase == 1)
        {
            if (totalLitLightPillars != 4 && UnityEngine.Random.Range(0f,4f) <= totalLitLightPillars)
            {
                PillarMove();
            }
            else
            {
                Attack();
            }
        }
    }

    public void ChangeFightPhase()
    {
        /*
         * Whenever the fight goes into another phase this resets everything.
         */
        transform.position = changePhaseLocation;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("foreground accessories");
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
        foreach (GameObject pillar in lightPillars)
        {
            pillar.GetComponent<TutorialPixiePillarMove>().objectMove(pillar.GetComponent<TutorialPixiePillarMove>().finalLocation, .5f, .02f);
        }

        phase++;
        transform.position = hero.transform.position + new Vector3(.5f, .5f, 0);
        gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Enemy");
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

    }



    public void StartFight()
    {
        ChangeFightPhase();
    }
}
