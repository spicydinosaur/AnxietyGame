using JetBrains.Annotations;
using Language.Lua;
using NavMeshPlus.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

//Connected to the script MovementOption
//OptionType { Stop, Wait, MoveTo, LookAt, FollowTarget, RunFromTarget, MeleeAttack }
//incomplete. Work on MeleeAttack coroutine.
//RunFromTarget, LookAt, and MeleeAttack need to be tested.
//Need RangedAttack coroutine. 
public class AdvancedPatrolScript : MonoBehaviour
{
    [SerializeField]
    public MovementOption[] patrolArray;
    public int positionInList;
    public float currentMovementOptionElapsedTime;
    public Coroutine runningCoroutine;
    public float destinationThreshold;

    //using this to dump the path from calculatepath
    public NavMeshHit hit;
    public Vector2 bestDistanceVector2;

    public bool isInterrupted;
    public float interruptMovementOptionElapsedTime;
    [SerializeField]
    public MovementOption[] preinterrupt;
    [SerializeField]
    public MovementOption[] interrupt;
    [SerializeField]
    public MovementOption[] postinterrupt;

    [SerializeField]
    public enum InterruptionState { None, Preinterrupt, Interrupt, Postinterrupt }
    public InterruptionState interruptionState;
    public int positionInInterrupt;

    public bool dontLoopPatrol;
    public bool dontLoopInterrupt;
    public bool startOnLoad;

    public NPCController controller;
    public Animator animator;
    //public Rigidbody2D rigidBody2D;

    public float meleeHealthDamage;
    public float meleeManaDamage;
    public float rangedHealthDamage;
    public float rangedManaDamage;

    public float defaultHealthDamage;
    public float defaultManaDamage;

    public bool hasRangedAttack;

    public NavMeshAgent agent;
    public bool hasBestVector2Changed;
    public GameObject destinationToken;
    public bool isRunningBackwards;
    public GameObject emergencyDestinationToken;

    public float wobbleVarForRunFromTarget;

    public void OnEnable()
    {
        interruptionState = InterruptionState.None;
        positionInList = 0;
        positionInInterrupt = 0;
        interruptMovementOptionElapsedTime = 0f;
        currentMovementOptionElapsedTime = 0f;
        isInterrupted = false;
        animator = GetComponent<Animator>();
        controller = GetComponent<NPCController>();
        //rigidBody2D = GetComponent<Rigidbody2D>();
        if (meleeHealthDamage == 0f)
        {
            meleeHealthDamage = defaultHealthDamage;
        }
        if (hasRangedAttack && rangedHealthDamage == 0f)
        {
            rangedHealthDamage = defaultHealthDamage;
        }


        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (startOnLoad)
        {
            TriggerMovementOption(patrolArray[positionInList]);
        }
    }

    public void Awake()
    {
        hit = new NavMeshHit();
    }
    void SetDestination(GameObject target)
    {
        var agentDrift = 0.0001f; // minimal
        var driftPos = target.transform.position + (Vector3)(agentDrift * UnityEngine.Random.insideUnitCircle);
        agent.SetDestination(driftPos);
    }

    void SetDestination(Vector3 vectorPosition)
    {
        var agentDrift = 0.0001f; // minimal
        var driftPos = vectorPosition + (Vector3)(agentDrift * UnityEngine.Random.insideUnitCircle);
        agent.SetDestination(driftPos);
    }



    public void NextPatrolOrInterrupt()
    {

        if (isInterrupted)
        {
            positionInInterrupt++;
            NextInterrupt();

        }
        else
        {
            positionInList++;
            NextPatrol();
        }
    }

    public void NextPatrol()
    {

        if (patrolArray != null)
        {
            if (positionInList >= patrolArray.Length)
            {
                positionInList = 0;
                if (dontLoopPatrol)
                {
                    Stop();
                }
                else
                {
                    TriggerMovementOption(patrolArray[positionInList]);
                }
            }
            else
            {
                TriggerMovementOption(patrolArray[positionInList]);
            }
        }


    }
    public void Stop()
    {
        animator.SetFloat("Speed", 0f);
        agent.isStopped = true;
        //Debug.Log("Stop functioned has fired!");


    }
    public IEnumerator Wait(float waitTime)
    {
        //Debug.Log("Wait Coroutine has fired!");
        //Debug.Log("runningCoroutine = " + runningCoroutine);
        animator.SetFloat("Speed", 0f);
        agent.isStopped = true;
        yield return new WaitForSeconds(waitTime);
        NextPatrolOrInterrupt();
        yield break;


    }

    public IEnumerator MoveTo(Vector2 vectorDestination, float distanceFromTarget)
    {
        //Debug.Log("MoveTo Coroutine has fired!");
        agent.isStopped = false;
        SetDestination(vectorDestination);
        //Debug.Log("destination coords are " + vectorDestination + "\ngameObject coords are " + gameObject.transform.position + ". distanceFromTarget = " + distanceFromTarget);
        if (distanceFromTarget == 0)
        {
            distanceFromTarget = 0.5f;
        }
        while (Vector2.Distance(transform.position, vectorDestination) > distanceFromTarget)
        {
            animator.SetFloat("Speed", 1f);
            Vector2 position = transform.position;
            var horizontal = vectorDestination.x - position.x;
            var vertical = vectorDestination.y - position.y;
            var velocity = new Vector2(horizontal, vertical);
            velocity.Normalize();
            animator.SetFloat("Look X", velocity.x);
            animator.SetFloat("Look Y", velocity.y);
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("destination coords are " + vectorDestination + "\ngameObject coords are " + gameObject.transform.position + " MoveTo Continuing (God Willing).");
        }
        agent.isStopped = true;
        animator.SetFloat("Speed", 0f);
        //Debug.Log("destination coords are " + vectorDestination + "\ngameObject coords are " + gameObject.transform.position + " MoveTo Complete.");
        NextPatrolOrInterrupt();
        yield break;

    }
    public IEnumerator LookAt(Vector2 lookDirection, float waitTime)
    {
        //Debug.Log("LookAt Coroutine has fired!");
        animator.SetFloat("Speed", 0f);
        agent.isStopped = true;
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        yield return new WaitForSeconds(waitTime);
        NextPatrolOrInterrupt();

    }
    public IEnumerator FollowTarget(GameObject target, float distanceFromTarget)
    {
        //Debug.Log("FollowTarget Coroutine has fired!");
        agent.isStopped = false;
        //Debug.Log("destination coords are " + target + "\ngameObject coords are " + gameObject.transform.position + ". distanceFromTarget = " + distanceFromTarget);
        if (distanceFromTarget == 0)
        {
            distanceFromTarget = 1f;
        }
        while (Vector2.Distance(transform.position, target.transform.position) > distanceFromTarget)
        {
            SetDestination(target);
            animator.SetFloat("Speed", 1f);
            Vector2 position = transform.position;
            var horizontal = target.transform.position.x - position.x;
            var vertical = target.transform.position.y - position.y;
            var velocity = new Vector2(horizontal, vertical);
            velocity.Normalize();
            animator.SetFloat("Look X", velocity.x);
            animator.SetFloat("Look Y", velocity.y);
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("destination coords are " + vectorDestination + "\ngameObject coords are " + gameObject.transform.position + " MoveTo Continuing (God Willing).");
        }
        agent.isStopped = true;
        animator.SetFloat("Speed", 0f);
        //Debug.Log("destination coords are " + target.transform.position + "\ngameObject coords are " + gameObject.transform.position + " MoveTo Complete.");
        NextPatrolOrInterrupt();
        yield break;

    }

    public IEnumerator RunFromTarget(GameObject target, float distanceFromTarget)
    {
        //Debug.Log("RunFromTarget Coroutine has fired!");
        agent.isStopped = false;
        //Debug.Log("destination coords are " + target + "\ngameObject coords are " + gameObject.transform.position + ". distanceFromTarget = " + distanceFromTarget);
        if (distanceFromTarget == 0)
        {
            distanceFromTarget = 3f;
        }
        if (wobbleVarForRunFromTarget == 0f)
        {
            wobbleVarForRunFromTarget = 1f;
        }
        while (Vector2.Distance(transform.position, target.transform.position) < distanceFromTarget)
        {
            var distance = Vector2.Distance(transform.position, target.transform.position);
            var horizontal = target.transform.position.x - transform.position.x;
            var vertical = target.transform.position.y - transform.position.y;
            var velocity = new Vector2(horizontal, vertical);
            velocity.Normalize();
            Vector2 position = new Vector2(transform.position.x - (distance * velocity.x), transform.position.y - (distance * velocity.y));
            //Debug.Log("distance variable = " + distance + ". velocity variable = " + velocity + ". position variable = " + position);
            if (destinationToken != null)
            {
                destinationToken.transform.position = position;
            }
            if (isRunningBackwards)
            {
                animator.SetFloat("Look X", velocity.x);
                animator.SetFloat("Look Y", velocity.y);
            }
            else
            {
                animator.SetFloat("Look X", -velocity.x);
                animator.SetFloat("Look Y", -velocity.y);
            }


            if (NavMesh.SamplePosition(position, out hit, 0.1f, NavMesh.AllAreas)) 
            {
                //Debug.Log("RunFronTarget coroutine initial if statement has fired!");
                SetDestination(position);
                if (emergencyDestinationToken != null)
                {
                    emergencyDestinationToken.gameObject.SetActive(false);
                }
            }
            else
            {
                //Debug.Log("in RunFronTarget coroutine, initial if statement moved to else.");
                bestDistanceVector2 = transform.position;

                hasBestVector2Changed = false;

                if (NavMesh.SamplePosition(new Vector2(transform.position.x - wobbleVarForRunFromTarget, transform.position.y), out hit, 0.1f, NavMesh.AllAreas)) 
                {
                    //Debug.Log("in RunFromTarget coroutine, inside else statement, first if successful");
                    TestDistanceFromTarget(new Vector2(transform.position.x - wobbleVarForRunFromTarget, transform.position.y), target);
                }
                if (NavMesh.SamplePosition(new Vector2(transform.position.x + wobbleVarForRunFromTarget, transform.position.y), out hit, 0.1f, NavMesh.AllAreas))
                {
                    //Debug.Log("in RunFromTarget coroutine, inside else statement, second if successful");
                    TestDistanceFromTarget(new Vector2(transform.position.x + wobbleVarForRunFromTarget, transform.position.y), target);
                }
                if (NavMesh.SamplePosition(new Vector2(transform.position.x, transform.position.y - wobbleVarForRunFromTarget), out hit, 0.1f, NavMesh.AllAreas))
                {
                    //Debug.Log("in RunFromTarget coroutine, inside else statement, third if successful");
                    TestDistanceFromTarget(new Vector2(transform.position.x, transform.position.y - wobbleVarForRunFromTarget), target);
                }
                if (NavMesh.SamplePosition(new Vector2(transform.position.x, transform.position.y + wobbleVarForRunFromTarget), out hit, 0.1f, NavMesh.AllAreas))
                {
                    //Debug.Log("in RunFromTarget coroutine, inside else statement, fourth if successful");
                    TestDistanceFromTarget(new Vector2(transform.position.x, transform.position.y + wobbleVarForRunFromTarget), target);
                }
                if (hasBestVector2Changed)
                {
                    //Debug.Log("in RunFromTarget coroutine, inside else statement, fifth if successful, now a path should be set! bestDistanceVector2 = " + bestDistanceVector2);
                    SetDestination(bestDistanceVector2);
                    //the below just gives bestDistanceVector2 a value to work with.
                    bestDistanceVector2 = transform.position;
                    if (emergencyDestinationToken != null)
                    {
                        emergencyDestinationToken.gameObject.SetActive(true);
                        emergencyDestinationToken.transform.position = bestDistanceVector2;
                    }
                }
                animator.SetFloat("Speed", 1f);
            }
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("destination coords are " + vectorDestination + "\ngameObject coords are " + gameObject.transform.position + " MoveTo Continuing (God Willing).");
        }
        agent.isStopped = true;
        animator.SetFloat("Speed", 0f);
        //Debug.Log("destination coords are " + target.transform.position + "\ngameObject coords are " + gameObject.transform.position + " RunFromTarget Complete.");
        NextPatrolOrInterrupt();
        yield break;

    }

    //untested material, See RunFronTarget Enum
    public void TestDistanceFromTarget(Vector2 attemptingPosition, GameObject target)
    {
        var distanceOfPath = Vector2.Distance(attemptingPosition, target.transform.position);
        if (!hasBestVector2Changed)
        {
            bestDistanceVector2 = attemptingPosition;
            hasBestVector2Changed = true;
        }
        else if (distanceOfPath > Vector2.Distance(bestDistanceVector2, target.transform.position))
        {
            bestDistanceVector2 = attemptingPosition;
        }
    }


    //Needs more work to be fully integrated. Port enemyController values for attack over to NPCController? Or find another way. How to allow for different attack damages from the same mob, and different types of damage (health, coping, etc.)
    public IEnumerator MeleeAttack(GameObject target, float duration, float distanceFromTarget)
    {
        //Debug.Log("MeleeAttack Coroutine has fired!");

        //yield return new WaitForSeconds(duration);
        animator.SetFloat("Speed", 0f);
        animator.SetBool("isAttacking", true);
        //Debug.Log("destination coords are " + target + "\ngameObject coords are " + gameObject.transform.position + ". distanceFromTarget = " + distanceFromTarget);
        if (distanceFromTarget == 0)
        {
            distanceFromTarget = 0.2f;
        }
        while (Vector2.Distance(transform.position, target.transform.position) <= distanceFromTarget)
        {
            if (meleeHealthDamage != 0f)
            {
                target.GetComponent<Player>().PlayerHealth(meleeHealthDamage);
            }
            if (meleeManaDamage != 0f)
            {
                target.GetComponent<Player>().PlayerMana(meleeManaDamage);
            }

            yield return new WaitForSeconds(duration);
        }
        animator.SetBool("isAttacking", false);
        NextPatrolOrInterrupt();
        yield break;
    }

    //Below is untouched and needs to be written, current code is from MeleeAttack
    public IEnumerator RangedAttack(GameObject target, float duration, float distanceFromTarget)
    {
        //Debug.Log("RangedAttack Coroutine has fired!");
        /*
        controller.isMoving = false;
        animator.SetFloat("Speed", 0f);
        animator.SetBool("isAttacking", true);
        Debug.Log("destination coords are " + target + "\ngameObject coords are " + gameObject.transform.position + ". distanceFromTarget = " + distanceFromTarget);
        if (distanceFromTarget == 0)
        {
            distanceFromTarget = 0.2f;
        }
        while (Vector2.Distance(transform.position, target.transform.position) <= distanceFromTarget)
        {
            if (healthDamage != 0f)
            {
                target.GetComponent<Player>().PlayerHealth(controller.healthDamage);
            }
            if (manaDamage != 0f)
            {
                target.GetComponent<Player>().PlayerMana(controller.manaDamage);
            }

            yield return new WaitForSeconds(duration);
        }*/
        animator.SetBool("isAttacking", false);
        NextPatrolOrInterrupt();
        yield break;
    }

    public IEnumerator PopUpText(float duration, string text, GameObject target)
    {
        
        target.GetComponent<TextMeshProUGUI>().enabled = true;
        target.GetComponent<TextMeshProUGUI>().SetText(text);

        yield return new WaitForSeconds(duration/2);

        target.GetComponent<TextMeshProUGUI>().enabled = false;
        target.GetComponent<TextMeshProUGUI>().SetText("");
        yield return new WaitForSeconds(duration/2);

        NextPatrolOrInterrupt();
        yield break;


    }

    public void Interrupt(MovementOption[] _preinterrupt, MovementOption[] _interrupt, MovementOption[] _postinterrupt)
    {
        isInterrupted = true;
        preinterrupt = _preinterrupt;
        interrupt = _interrupt;
        postinterrupt = _postinterrupt;

        interruptionState = InterruptionState.Preinterrupt;
        if (preinterrupt.Length == 0)
        {
            if (interrupt.Length == 0)
            {
                if (postinterrupt.Length == 0)
                {
                    interruptionState = InterruptionState.None;
                }
                else
                {
                    interruptionState = InterruptionState.Postinterrupt;
                }
            }
            else
            {
                interruptionState = InterruptionState.Interrupt;
            }
        }
        else
        {
            interruptionState = InterruptionState.Preinterrupt;
        }
        positionInInterrupt = 0;
        NextPatrolOrInterrupt();

    }

    private void NextInterrupt()
    {

        //Debug.Log("NextInterrupt setting for currentInterruptArray = " + interruptionState + " and positionInInterrupt " + positionInInterrupt + "\nAnimator value for speed = " + animator.GetFloat("Speed") + " and runningCoroutine = " + runningCoroutine);

        MovementOption[] currentInterruptArray;


        if (interruptionState == InterruptionState.Preinterrupt)
        {
            currentInterruptArray = preinterrupt;
            //Debug.Log("currentInterruptArray = preinterrupt");
        }
        else if (interruptionState == InterruptionState.Interrupt)
        {
            currentInterruptArray = interrupt;
            //Debug.Log("currentInterruptArray = interrupt");
        }
        else if (interruptionState == InterruptionState.Postinterrupt)
        {
            currentInterruptArray = postinterrupt;
            //Debug.Log("currentInterruptArray = postinterrupt");
        }
        else
        {
            currentInterruptArray = null;
            //Debug.Log("currentInterruptArray = null");
            positionInInterrupt = 0;
            isInterrupted = false;
        }



        if (currentInterruptArray != null)
        {
            if (positionInInterrupt >= currentInterruptArray.Length)

            {
                positionInInterrupt = 0;

                if (interruptionState == InterruptionState.Preinterrupt)
                {
                    interruptionState++;

                    if (dontLoopInterrupt)
                    {
                        interruptionState++;

                    }
                }
                else if (interruptionState == InterruptionState.Postinterrupt)
                {
                    interruptionState++;
                    currentInterruptArray = null;
                    isInterrupted = false;
                    //Below: Interrupt is over, this will work backwards from the back of the patrolArray to find out what has not happened yet that is -not- wait, stop, or lookat, which are immediate or important. This allows it to direct the npc to move back to any point it was going towards, follow the pc, etc.
                    for (var i = positionInList; i > 0; i--)
                    {
                        if (patrolArray[i].movementOption != MovementOption.OptionType.Wait ||
                            patrolArray[i].movementOption != MovementOption.OptionType.Stop ||
                            patrolArray[i].movementOption != MovementOption.OptionType.LookAt)
                        {
                            positionInList = i;
                            break;
                        }
                        else if (i == 0)
                        {
                            positionInList = i;
                            break;
                        }
                    }

                }


            }
        }

        if (currentInterruptArray != null)
        {
            //Debug.Log("positionInInterrupt = " + positionInInterrupt);
            TriggerMovementOption(currentInterruptArray[positionInInterrupt]);
        }
        else
        {
            TriggerMovementOption(patrolArray[positionInList]);
        }
    }

    public void TriggerMovementOption(MovementOption movementOption)
    {

        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        //Debug.Log("runningCoroutine = " + runningCoroutine);


        switch (movementOption.movementOption)
        {
            case MovementOption.OptionType.Stop:
                //Debug.Log("runningCoroutine = null because the OptionType is Stop");
                Stop();
                break;

            case MovementOption.OptionType.Wait:
                runningCoroutine = StartCoroutine(Wait(movementOption.duration));
                //Debug.Log("runningCoroutine = Wait()");
                break;

            case MovementOption.OptionType.MoveTo:
                runningCoroutine = StartCoroutine(MoveTo(movementOption.movementPosition, movementOption.distanceFromTarget));
                //Debug.Log("runningCoroutine = MoveTo()");
                break;

            case MovementOption.OptionType.LookAt:
                runningCoroutine = StartCoroutine(LookAt(movementOption.movementPosition, movementOption.duration));
                //Debug.Log("runningCoroutine = LookAt()");
                break;

            case MovementOption.OptionType.FollowTarget:
                runningCoroutine = StartCoroutine(FollowTarget(movementOption.target, movementOption.distanceFromTarget));
                //Debug.Log("runningCoroutine = FollowTarget()");
                break;

            case MovementOption.OptionType.RunFromTarget:
                runningCoroutine = StartCoroutine(RunFromTarget(movementOption.target, movementOption.distanceFromTarget));
                //Debug.Log("runningCoroutine = RunFronTarget()");
                break;

            case MovementOption.OptionType.MeleeAttack:
                runningCoroutine = StartCoroutine(MeleeAttack(movementOption.target, movementOption.duration, movementOption.distanceFromTarget));
                Debug.Log("runningCoroutine = MeleeAttack()");
                break;

            case MovementOption.OptionType.RangedAttack:
                runningCoroutine = StartCoroutine(RangedAttack(movementOption.target, movementOption.duration, movementOption.distanceFromTarget));
                Debug.Log("runningCoroutine = RangedAttack()");
                break;

            case MovementOption.OptionType.PopUpText:
                runningCoroutine = StartCoroutine(PopUpText(movementOption.duration, movementOption.text, movementOption.target));
                //Debug.Log("runningCoroutine = Wait()");
                break;
        }

        //Debug.Log("positionInList = " + positionInList);
    }

    public void StopInterrupt()
    {
        if (isInterrupted)
        {
            interruptionState = InterruptionState.Postinterrupt;
            positionInInterrupt = 0;
            NextInterrupt();

        }
    }



}

