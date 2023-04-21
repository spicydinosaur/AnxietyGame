using JetBrains.Annotations;
using Language.Lua;
using NavMeshPlus.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public Rigidbody2D rigidBody2D;

    public float healthDamage;
    public float manaDamage;
    public float defaultHealthDamage;
    public float defaultManaDamage;

    public NavMeshAgent agent;

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
        rigidBody2D = GetComponent<Rigidbody2D>();
        healthDamage = defaultHealthDamage;
        manaDamage = defaultManaDamage;

        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (startOnLoad)
        {
            TriggerMovementOption(patrolArray[positionInList]);
        }
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
        controller.isMoving = false;
        agent.isStopped = true;
        Debug.Log("Stop functioned has fired!");


    }
    public IEnumerator Wait(float waitTime)
    {
        Debug.Log("Wait Coroutine has fired!");
        Debug.Log("runningCoroutine = " + runningCoroutine);
        animator.SetFloat("Speed", 0f);
        controller.isMoving = false;
        agent.isStopped = true;
        yield return new WaitForSeconds(waitTime);
        NextPatrolOrInterrupt();
        yield break;


    }

    public IEnumerator MoveTo(Vector2 vectorDestination,float distanceFromTarget)
    {
        Debug.Log("MoveTo Coroutine has fired!");
        agent.isStopped = false;
        SetDestination(vectorDestination);
        Debug.Log("destination coords are " + vectorDestination + "\ngameObject coords are " + gameObject.transform.position + ". distanceFromTarget = " + distanceFromTarget);
        if (distanceFromTarget == 0)
        {
            distanceFromTarget = 0.5f;
        }
        while (Vector2.Distance(transform.position, vectorDestination) > distanceFromTarget) 
        {
            animator.SetFloat("Speed", 1f);
            controller.isMoving = true;
            Vector2 position = rigidBody2D.position;
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
        controller.isMoving = false;
        Debug.Log("destination coords are " + vectorDestination + "\ngameObject coords are " + gameObject.transform.position + " MoveTo Complete.");
        NextPatrolOrInterrupt();
        yield break;

    }
    public IEnumerator LookAt(Vector2 lookDirection, float waitTime)
    {
        Debug.Log("LookAt Coroutine has fired!");
        animator.SetFloat("Speed", 0f);
        controller.isMoving = false;
        agent.isStopped = true;
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        yield return new WaitForSeconds(waitTime);
        NextPatrolOrInterrupt();

    }
    public IEnumerator FollowTarget(GameObject target, float distanceFromTarget)
    {
        Debug.Log("FollowTarget Coroutine has fired!");
        //what do we do if the player moves off the current navmesh? We need to be able to reset the NPC so that it doesn't create errors or just try endlessly to get to the PC or NPC it is trying to follow.
        agent.isStopped = false;
        Debug.Log("destination coords are " + target + "\ngameObject coords are " + gameObject.transform.position + ". distanceFromTarget = " + distanceFromTarget);
        if (distanceFromTarget == 0)
        {
            distanceFromTarget = 1f;
        }
        while (Vector2.Distance(transform.position, target.transform.position) > distanceFromTarget)
        {
            SetDestination(target);
            animator.SetFloat("Speed", 1f);
            controller.isMoving = true;
            Vector2 position = rigidBody2D.position;
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
        controller.isMoving = false;
        Debug.Log("destination coords are " + target.transform.position + "\ngameObject coords are " + gameObject.transform.position + " MoveTo Complete.");
        NextPatrolOrInterrupt();
        yield break;
        /*
        while (Vector2.Distance(rigidBody2D.position, target.transform.position) > distanceFromTarget)
        {
            animator.SetFloat("Speed", 1f);
            controller.isMoving = true;
            Debug.Log("destination coords are " + target.transform.position + "\ngameObject coords are " + gameObject.transform.position + "\npositioninlist is " + positionInList);

            Vector2 position = rigidBody2D.position;
            var horizontal = target.transform.position.x - position.x;
            var vertical = target.transform.position.x - position.y;
            var velocity = new Vector2(horizontal, vertical);
            Debug.Log("velocity = " + velocity);
            velocity.Normalize();
            Debug.Log("velocity.Normalize() = " + velocity);
            position.x = position.x + controller.speed * velocity.x * Time.deltaTime;
            position.y = position.y + controller.speed * velocity.y * Time.deltaTime;
            animator.SetFloat("Look X", velocity.x);
            animator.SetFloat("Look Y", velocity.y);
            rigidBody2D.MovePosition(position);

            yield return new WaitForFixedUpdate();
            
        }
        animator.SetFloat("Speed", 0f);
        controller.isMoving = false;
        NextPatrolOrInterrupt();
        yield break;
        */
    }

    public IEnumerator RunFromTarget(GameObject target, float distanceFromTarget)
    {
        Debug.Log("RunFromTarget Coroutine has fired!");
        //eventually there will probably be an error if the position is outside the navmesh. We need to be able to check if the position is a valid position and adjust accordingly if it is not.
        //this is currently NOT working! Find a way to calculate the position variable that allows the NPC to reliably run from its target.
        agent.isStopped = false;
        Debug.Log("destination coords are " + target + "\ngameObject coords are " + gameObject.transform.position + ". distanceFromTarget = " + distanceFromTarget);
        if (distanceFromTarget == 0)
        {
            distanceFromTarget = 5f;
        }
        while (Vector2.Distance(transform.position, target.transform.position) < distanceFromTarget)
        {
            Vector2 position = target.transform.position * -1;
            SetDestination(position);
            animator.SetFloat("Speed", 1f);
            controller.isMoving = true;
            var horizontal = target.transform.position.x;
            var vertical = target.transform.position.y;
            var velocity = new Vector2(horizontal, vertical);
            velocity.Normalize();
            position.x = position.x + controller.speed * velocity.x * Time.deltaTime;
            position.y = position.y + controller.speed * velocity.y * Time.deltaTime;
            animator.SetFloat("Look X", -velocity.x);
            animator.SetFloat("Look Y", -velocity.y);
            yield return new WaitForSeconds(0.1f);
            //Debug.Log("destination coords are " + vectorDestination + "\ngameObject coords are " + gameObject.transform.position + " MoveTo Continuing (God Willing).");
        }
        agent.isStopped = true;
        animator.SetFloat("Speed", 0f);
        controller.isMoving = false;
        Debug.Log("destination coords are " + target.transform.position + "\ngameObject coords are " + gameObject.transform.position + " RunFromTarget Complete.");
        NextPatrolOrInterrupt();
        yield break;
        /*
        Debug.Log("RunFromTarget Coroutine has fired!");

        yield return new WaitForFixedUpdate();

        while (Vector2.Distance(rigidBody2D.position, target.transform.position) < distanceFromTarget)
        {
            animator.SetFloat("Speed", 1f);
            controller.isMoving = true;
            Debug.Log("destination coords are " + target.transform.position + "\ngameObject coords are " + gameObject.transform.position + "\npositioninlist is " + positionInList);

            Vector2 position = gameObject.transform.position * -1;
            var horizontal = target.transform.position.x;
            var vertical = target.transform.position.y;
            var velocity = new Vector2(horizontal, vertical);
            velocity.Normalize();
            position.x = position.x + controller.speed * velocity.x * Time.deltaTime;
            position.y = position.y + controller.speed * velocity.y * Time.deltaTime;
            animator.SetFloat("Look X", -velocity.x);
            animator.SetFloat("Look Y", -velocity.y);
            rigidBody2D.MovePosition(position);

            yield return new WaitForFixedUpdate();

        }

        animator.SetFloat("Speed", 0f);
        controller.isMoving = false;
        NextPatrolOrInterrupt();
        yield break;
        */
    }


    //Needs more work to be fully integrated. Port enemyController values for attack over to NPCController? Or find another way. How to allow for different attack damages from the same mob, and different types of damage (health, coping, etc.)
    public IEnumerator MeleeAttack(GameObject target, float duration, float distanceFromTarget)
    {
        Debug.Log("MeleeAttack Coroutine has fired!");

        //yield return new WaitForSeconds(duration);
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
        }
        animator.SetBool("isAttacking", false);
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
        positionInInterrupt= 0;
        NextPatrolOrInterrupt();

    }

    private void NextInterrupt()
    {

        Debug.Log("NextInterrupt setting for currentInterruptArray = " + interruptionState + " and positionInInterrupt " + positionInInterrupt
    + "\nAnimator value for speed = " + animator.GetFloat("Speed") + " and runningCoroutine = " + runningCoroutine);

        MovementOption[] currentInterruptArray;


        if (interruptionState == InterruptionState.Preinterrupt)
        {
            currentInterruptArray = preinterrupt;
            Debug.Log("currentInterruptArray = preinterrupt");
        }
        else if (interruptionState == InterruptionState.Interrupt)
        {
            currentInterruptArray = interrupt;
            Debug.Log("currentInterruptArray = interrupt");
        }
        else if (interruptionState == InterruptionState.Postinterrupt)
        {
            currentInterruptArray = postinterrupt;
            Debug.Log("currentInterruptArray = postinterrupt");
        }
        else
        {
            currentInterruptArray = null;
            Debug.Log("currentInterruptArray = null");
            positionInInterrupt= 0;
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
            Debug.Log("positionInInterrupt = " + positionInInterrupt);
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
        Debug.Log("runningCoroutine = " + runningCoroutine);


        switch (movementOption.movementOption)
        {
            case MovementOption.OptionType.Stop:
                Debug.Log("runningCoroutine = null because the OptionType is Stop");
                Stop();
                break;

            case MovementOption.OptionType.Wait:
                runningCoroutine = StartCoroutine(Wait(movementOption.duration));
                Debug.Log("runningCoroutine = Wait()");
                break;

            case MovementOption.OptionType.MoveTo:
                runningCoroutine = StartCoroutine(MoveTo(movementOption.movementPosition, movementOption.distanceFromTarget));
                Debug.Log("runningCoroutine = MoveTo()");
                break;

            case MovementOption.OptionType.LookAt:
                runningCoroutine = StartCoroutine(LookAt(movementOption.movementPosition, movementOption.duration));
                Debug.Log("runningCoroutine = LookAt()");
                break;
               
            case MovementOption.OptionType.FollowTarget:
                runningCoroutine = StartCoroutine(FollowTarget(movementOption.target, movementOption.distanceFromTarget));
                Debug.Log("runningCoroutine = FollowTarget()");
                break;

            case MovementOption.OptionType.RunFromTarget:
                runningCoroutine = StartCoroutine(RunFromTarget(movementOption.target,  movementOption.distanceFromTarget));
                Debug.Log("runningCoroutine = RunFronTarget()");
                break;
                
            case MovementOption.OptionType.MeleeAttack:
                runningCoroutine = StartCoroutine(MeleeAttack(movementOption.target, movementOption.duration, movementOption.distanceFromTarget));
                Debug.Log("runningCoroutine = MeleeAttack()");
                break;

                /* The below is currently redundant
    case MovementOption.OptionType.ChaseTarget:
        runningCoroutine = StartCoroutine(ChaseTarget(movementOption.target, movementOption.duration));
        Debug.Log("runningCoroutine = ChaseTarget()");
        break;
        */

        }

        Debug.Log("positionInList = " + positionInList);
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

//ChaseTarget is pretty much just FollowTarget, so unless we need it to be done differently, it is redundant.
/*
public IEnumerator ChaseTarget(GameObject target, float waitTime)
{
    Debug.Log("ChaseTarget Coroutine has fired!");
    Vector2 relativeChasePosition = new Vector2(target.transform.position.x - gameObject.transform.position.x, target.transform.position.y - gameObject.transform.position.y);

    if (Mathf.Approximately(gameObject.transform.position.x, relativeChasePosition.x) == false 
        || Mathf.Approximately(gameObject.transform.position.y, relativeChasePosition.y) == false)
    {

        controller.isMoving = true;
        controller.isChasing = true;
        animator.SetFloat("Speed", 1f);
        Vector2 position = gameObject.transform.position;
        var horizontal = relativeChasePosition.x;
        var vertical = relativeChasePosition.y;
        var velocity = new Vector2(horizontal, vertical);
        velocity.Normalize();
        position.x = position.x + controller.speed * velocity.x * Time.deltaTime;
        position.y = position.y + controller.speed * velocity.y * Time.deltaTime;
        animator.SetFloat("Look X", velocity.x);
        animator.SetFloat("Look Y", velocity.y);

        yield return new WaitForSeconds(waitTime);
    }
    else
    {
        controller.isMoving = false;
        controller.isChasing = false;
        animator.SetFloat("Speed", 0f);
        NextPatrolOrInterrupt();
        yield break;
    }

}
*/