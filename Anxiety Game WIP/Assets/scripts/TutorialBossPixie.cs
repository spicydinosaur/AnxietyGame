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

public class TutorialBossPixie: NPCController, IMakeDecision
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
    public float meleeAttackDistanceValue;

    public float timeUntilNextAction;

    public Vector3 faceTowardsHero;

    public List<Vector3> spawnDirectionModifier; 

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


    //make the black flame cloak for the pixie for phase three.

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
        if (rangedAttackDistanceValue == 0f)
        {
            rangedAttackDistanceValue = 4f;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("TutorialBossPixie script: pixie has collided with the hero");
            //how do we get the game to individually decide whether an interrupt is looped or not on advancedPatrolScript?


        }
    }

    public void Update()
    {
          if (timeUntilNextAction <= 0f)
          {
            Debug.Log("TutorialBossPixie script: Update() says Attack() should initiate now!");
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
        Debug.Log("TutorialBossPixie script:OnDeath() should initiate now!");
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
        Debug.Log("TutorialBossPixie script: Attack() func firing!");
        if (Vector3.Distance(gameObject.transform.position, hero.transform.position) >= rangedAttackDistanceValue)
        {
            Debug.Log("TutorialBossPixie script:LightningAttack() should initiate now!");
            LightningAttack();
        }
        else if (Vector3.Distance(gameObject.transform.position, hero.transform.position) < rangedAttackDistanceValue && Vector3.Distance(gameObject.transform.position, hero.transform.position) > meleeAttackDistanceValue)
        {
            Debug.Log("TutorialBossPixie script: advancedPatrolScript.Interrupt() should initiate now!");
            advancedPatrolScript.Interrupt(preinterruptGetInRange, interruptGetInRange, postinterruptGetInRange);
        }
        else
        {
            Debug.Log("TutorialBossPixie script:FireAttack() should initiate now!");
            FireAttack();
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
        Debug.Log("TutorialBossPixie script: PillarMove firing!");
        //step one: remove lit pillars from the selection.
        //step two: remove remaining unlit pillars that are at their starting location. 
        //step three: check to make sure that at least one pillar is available. If not, activate attack() instead.
        //step four: select closest pillar.

    }

    public void LitPillarMove()
    {
        Debug.Log("TutorialBossPixie script: LitPillarMove firing!");
        //step one: remove unlit pillars that are at their starting location.
        //step two: check to make sure that at least one pillar is available. If not, activate attack() instead.
        //step three: check for any unlit pillars not at starting location.
        //step four: select closest unlit pillar if any exist.
        //step five: otherwise select closest pillar.
    }


    public IEnumerator LightBrazier()
    {
        Debug.Log("TutorialBossPixie script: coroutine LightBrazier() firing!");
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
        Debug.Log("TutorialBossPixie script: coroutine ExtinguishBrazier() firing!");
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

    public void FireAttack()
    {
        //should we check if the fairy is too close to a lit pillar and have her abort the melee?
        Debug.Log("TutorialBossPixie script: Playeris close enough to the pixie that FireAttack() has initiated!");
        faceTowardsHero = Vector3.Normalize(hero.transform.position - transform.position);

        if (Mathf.Abs(faceTowardsHero.x) > Mathf.Abs(faceTowardsHero.y) && Mathf.Abs(faceTowardsHero.x) >= .5f)
        {
            animator.SetFloat("Look Y", 0f);

            if (faceTowardsHero.x > 0f)
            {
                animator.SetFloat("Look X", .5f);
                closeRangeSpellSpawn = gameObject.transform.position + spawnDirectionModifier[1];
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                animator.SetFloat("Look X", -.5f);
                closeRangeSpellSpawn = gameObject.transform.position + spawnDirectionModifier[2];
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
            }


        }
        else if (Mathf.Abs(faceTowardsHero.y) > Mathf.Abs(faceTowardsHero.x) && Mathf.Abs(faceTowardsHero.y) >= .5f)
        {
            animator.SetFloat("Look X", 0f);

            if (faceTowardsHero.y > 0f)
            {
                animator.SetFloat("Look Y", .5f);
                closeRangeSpellSpawn = gameObject.transform.position + spawnDirectionModifier[3];
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270);
            }
            else
            {
                animator.SetFloat("Look Y", -.5f);
                closeRangeSpellSpawn = gameObject.transform.position + spawnDirectionModifier[4];
                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
            }

        }
        else
        {
            Debug.Log("You have arrived at the else statement for FireAttack(), which should not happen.");
        }
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
        //change this to charge up and be line of sight to continue charging.
        //Do we want the fairy to be able to change her mind and go for a close range attack if the player moves too close
        //or be locked into charging the lightning attack, making her vulnerable to the player?
        //if the spell charges and hits, it leaves behind an aoe on the ground the size of the pillar light collider.
        lightningObject.SetActive(true);
        Debug.Log("TutorialBossPixie script: Playeris far enough away from the pixie that LightningAttack() has initiated!");
    }

    public void MakeDecision()
    {
        /* triggered by AdvancedPatrolScript.
         * 
         * move light pillars (closest first?)
         * light braziers (closest first?) - 3rd phase only
         * attack player
         
         * the boss fairy reevaluates her actions when all four pillars are lit and closest to the totem, any brazier is converted to light (with an increasing chance to handle that for each brazier converted,) any time she takes damage

         */
        Debug.Log("TutorialBossPixie script: func MakeDecision() fired.");

        if (phase == 1)
        {
            Debug.Log("TutorialBossPixie script: Attack() for phase 1 in MakeDecision() should now fire.");
            Attack();
            
        }
        else if (phase == 2)
        {
            Debug.Log("TutorialBossPixie script: phase 2 in MakeDecision()");
            if (totalLitLightPillars != 4 && UnityEngine.Random.Range(1f, 5f) <= totalLitLightPillars)
            {
                Debug.Log("TutorialBossPixie script: PillarMove() for phase 2 in MakeDecision() should now fire.");
                PillarMove();
            }

            else
            {
                Debug.Log("TutorialBossPixie script: Attack() for phase 2 in MakeDecision() should now fire.");
                Attack();
            }
        }
        else if (phase == 3)
        {
            //would relighting the braziers be a potential decision option?

            if (totalLitLightPillars != 4 && UnityEngine.Random.Range(1f, 5f) <= totalLitLightPillars)
            {
                Debug.Log("TutorialBossPixie script: LitPillarMove() for phase 3 in MakeDecision() should now fire.");
                LitPillarMove();
            }

            else
            {
                Debug.Log("TutorialBossPixie script: Attack() for phase 3 in MakeDecision() should now fire.");
                Attack();
            }
        }
    }

    public void ChangeFightPhase()
    {
        /*
         * Whenever the fight goes into another phase this resets everything.
         */
        Debug.Log("TutorialBossPixie script: ChangeFightPhase() function fired.");
        
        gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("foreground accessories");
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
        GetComponent<Collider2D>().enabled = false;
        transform.position = new Vector3(-63.53f, 12.44f, 0.0f);
        foreach (GameObject pillar in lightPillars)
        {
            pillar.GetComponent<TutorialPixiePillarMove>().objectMove(pillar.GetComponent<TutorialPixiePillarMove>().startLocation, .5f, .02f);
        }

        phase++;
        
    }



    public void StartFight()
    {
        Debug.Log("TutorialBossPixie script: function StartFight() fired, ChangeFightPhase() should be firing because of this.");
        ChangeFightPhase();
    }



}
