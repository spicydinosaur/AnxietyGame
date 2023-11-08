using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using Unity.Mathematics;
using Unity.VisualScripting;
using TMPro;
//using System.Numerics;

public class TutorialBossPixie: NPCController
{

    public List<GameObject> lightPillars;
    public List<GameObject> braziers;

    public int totalLitLightPillars = 0;

    public int phase = 0;

    public GameObject totem;

    public GameObject flameLootDrop;
    public GameObject flameLootDropInst;
    public spellLootDrop spellLootScript;

    public NPCHealth healthScript;

    public Vector3 changePhaseLocation;

    public GameObject lightningObject;

    public float candleWaitTime;

    public AudioSource candlesAudio;
    public AudioClip candlesLightingSound;
    public AudioClip candlesExtinguishSound;

    public float rangedAttackDistanceValue;

    public float timeUntilNextAction;

    public Vector3 faceTowardsHero;

    public float spawnDirectionModifier;

    public GameObject closeRangeSpell;
    public Vector3 closeRangeSpellSpawn;

    public GameObject flameSpellLoot;

    public float maxAggroDistance;

    public VariablesForTheGameManager varsForGM;

    [SerializeField]
    public MovementOption[] preinterruptGetInRange;
    [SerializeField]
    public MovementOption[] interruptGetInRange;
    [SerializeField]
    public MovementOption[] postinterruptGetInRange;

    public Canvas pixieHUDElements;


    public override void OnEnable()
    {
        hero = GameObject.Find("Hero");
        animator = GetComponent<Animator>();
        EventBroadcaster.HeroDeath.AddListener(HeroDied);
        advancedPatrolScript = GetComponent<AdvancedPatrolScript>();
        animator.SetBool("isDead", false);
        animator.SetBool("isAttacking", false);
        animator.SetFloat("Speed", 1f);
        pixieHUDElements.enabled = true;
        if (maxAggroDistance <= 0f)
        {
            maxAggroDistance = 8f;
        }
        if (timeUntilNextAction == 0f)
        {
            timeUntilNextAction = 3f;
        }
        if (spawnDirectionModifier == 0)
        {
            spawnDirectionModifier = .7f;
        }
        if (rangedAttackDistanceValue == 0f)
        {
            rangedAttackDistanceValue = 4f;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //how do we get the game to individually decide whether an interrupt is looped or not on advancedPatrolScript?
            

        }
    }

    public void Update()
    {
          if (timeUntilNextAction <= 0f)
          {
            Attack();
            timeUntilNextAction = 3f;
          }
        else
        {
            timeUntilNextAction -= Time.deltaTime;
        }
    }

    public override void OnDeath()
    {
        pixieHUDElements.enabled = false;
        advancedPatrolScript.Interrupt(death_preinterrupt, death_interrupt, death_postinterrupt);
        EventBroadcaster.HeroDeath.RemoveListener(HeroDied);
        varsForGM.FlameSpellLootLoc = transform.position;
        GameManager.tutorialPixieDefeated = true;
        flameLootDropInst = Instantiate(flameLootDrop, transform.position, quaternion.identity);
        flameLootDropInst.SetActive(true);
        flameLootDropInst.GetComponent<Animator>().SetTrigger("hasDropped");
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
        animator.SetFloat("Speed", 0f);


    }

    public void Attack()
    {
        if (Vector3.Distance(gameObject.transform.position, hero.transform.position) > rangedAttackDistanceValue)
        {
            LightningAttack();
        }
        else if (Vector3.Distance(gameObject.transform.position, hero.transform.position) < rangedAttackDistanceValue && Vector3.Distance(gameObject.transform.position, hero.transform.position) > 1)
        {
            advancedPatrolScript.Interrupt(preinterruptGetInRange, interruptGetInRange, postinterruptGetInRange);
        }
        else
        {
            CloseRangeFireSpell();
        }
    }

    public override void HeroDied()
    {
        pixieHUDElements.enabled = false;
        advancedPatrolScript.StopInterrupt();
        
        //event for herodied

    }

    public void PillarMove()
    {

    }

    public IEnumerator LightBrazier()
    {
        foreach (GameObject candle in braziers)
        {
            if (candlesAudio.isPlaying)
            {
                candlesAudio.Stop();
            }
            candlesAudio.clip = candlesLightingSound; 
            candlesAudio.Play(0);
            candle.SetActive(true);
            yield return new WaitForSeconds(candleWaitTime);
        }
        yield break;
    }

    public IEnumerator ExtinguishBrazier()
    {
        foreach (GameObject candle in braziers)
        {
            if (candlesAudio.isPlaying)
            {
                candlesAudio.Stop();
            }
            candlesAudio.clip = candlesExtinguishSound;
            candlesAudio.Play(0);
            candle.SetActive(true);
            yield return new WaitForSeconds(candleWaitTime);
        }
        yield break;
    }

    public void CloseRangeFireSpell()
    {
        faceTowardsHero = Vector3.Normalize(hero.transform.position - transform.position);

        if (Mathf.Abs(faceTowardsHero.x) > Mathf.Abs(faceTowardsHero.y) && Mathf.Abs(faceTowardsHero.x) >= .5f)
        {
            animator.SetFloat("Look Y", 0f);

            if (faceTowardsHero.x > 0f)
            {
                animator.SetFloat("Look X", .5f);
                closeRangeSpellSpawn.x = gameObject.transform.position.x + spawnDirectionModifier;
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                animator.SetFloat("Look X", -.5f);
                closeRangeSpellSpawn.x = gameObject.transform.position.x + -spawnDirectionModifier;
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
            }
            closeRangeSpellSpawn.y = gameObject.transform.position.y + 0f;

        }
        else if (Mathf.Abs(faceTowardsHero.y) > Mathf.Abs(faceTowardsHero.x) && Mathf.Abs(faceTowardsHero.y) >= .5f)
        {
            animator.SetFloat("Look X", 0f);

            if (faceTowardsHero.y > 0f)
            {
                animator.SetFloat("Look Y", .5f);
                closeRangeSpellSpawn.y = gameObject.transform.position.y + spawnDirectionModifier;
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270);
            }
            else
            {
                animator.SetFloat("Look Y", -.5f);
                closeRangeSpellSpawn.y = gameObject.transform.position.y + -spawnDirectionModifier;
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
            }
            closeRangeSpellSpawn.x = gameObject.transform.position.x + 0f;
        }
        else
        {
            Debug.Log("You have arrived at the else statement for CloseRangeFireSpell(), which should not happen.");
        }
        closeRangeSpellSpawn = new Vector3(closeRangeSpellSpawn.x, closeRangeSpellSpawn.y, 0f);
        closeRangeSpell.transform.position = closeRangeSpellSpawn;
        closeRangeSpell.GetComponent<Animator>().SetTrigger("isCasting");

       /* Below is an alternate possibility.
        * 
        * if (animator.GetFloat("Look X") >= .5f && animator.GetFloat("Look Y") >= -.69f && animator.GetFloat("Look Y") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270);

        }
        //is it looking right?
        else if (animator.GetFloat("Look X") <= -0.5f && animator.GetFloat("Look Y") >= -.69f && animator.GetFloat("Look Y") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);

        }
        //is it looking backward?
        else if (animator.GetFloat("Look Y") >= .5f && animator.GetFloat("Look X") >= -.69f && animator.GetFloat("Look X") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);

        }
        else //we are looking forward
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);

        }*/
    }
    public void LightningAttack()
    {
        lightningObject.SetActive(true);
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
            if (totalLitLightPillars != 4 && UnityEngine.Random.Range(1f,5f) <= totalLitLightPillars)
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
        gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("foreground accessories");
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
        GetComponent<Collider2D>().enabled = false;
        transform.position = new Vector3(-63.53f, 12.44f, 0.0f);
        foreach (GameObject pillar in lightPillars)
        {
            pillar.GetComponent<TutorialPixiePillarMove>().objectMove(pillar.GetComponent<TutorialPixiePillarMove>().finalLocation, .5f, .02f);
        }

        phase++;
        
    }



    public void StartFight()
    {
        ChangeFightPhase();
    }



}
