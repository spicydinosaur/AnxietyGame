using JetBrains.Annotations;
using Language.Lua;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

//Connected to the script MovementOption
//OptionType { Stop, Wait, MoveTo, LookAt, FollowTarget, ChaseTarget, RunFromTarget }
//must implement modifications based on colliders
//need a timer to be utilized by the interrupts
//runfromtarget is currently incomplete.
//what do we do about the discrepancies with the speed value of the animator controller?
public class AdvancedPatrolScript : MonoBehaviour
{
    [SerializeField]
    public MovementOption[] patrolArray;
    public int positionInList;
    public float currentMovementOptionElapsedTime;
    public Coroutine runningCoroutine;


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

    

    

    public void OnEnable()
    {
        interruptionState = InterruptionState.None;
        positionInList= 0;
        positionInInterrupt= 0;
        interruptMovementOptionElapsedTime= 0f;
        currentMovementOptionElapsedTime= 0f;
        isInterrupted= false;
    }

    public void NextPatrolOrInterrupt()
    {
        if (isInterrupted)
        {
            NextInterrupt();
        }
        else
        {
            NextPatrol();
        }
    }

    public void NextPatrol()
    {
        if (runningCoroutine!= null) 
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        TriggerMovementOption(patrolArray[positionInList]);
        /*switch (patrolArray[positionInList].movementOption)
        {
            case MovementOption.OptionType.Stop:
                Stop();
                break;

            case MovementOption.OptionType.Wait:
                runningCoroutine = StartCoroutine(Wait(patrolArray[positionInList].duration));
                break;

            case MovementOption.OptionType.MoveTo:
                runningCoroutine = StartCoroutine(MoveTo(patrolArray[positionInList].movementPosition, patrolArray[positionInList].duration));
                break;

            case MovementOption.OptionType.LookAt:
                LookAt(patrolArray[positionInList].movementPosition);
                break;

            case MovementOption.OptionType.FollowTarget:
                runningCoroutine = StartCoroutine(FollowTarget(patrolArray[positionInList].target, patrolArray[positionInList].duration, patrolArray[positionInList].distanceFromTarget));
                break;

            case MovementOption.OptionType.ChaseTarget:
                runningCoroutine = StartCoroutine(ChaseTarget(patrolArray[positionInList].target, patrolArray[positionInList].duration));
                break;

            case MovementOption.OptionType.RunFromTarget:
                runningCoroutine = StartCoroutine(RunFromTarget(patrolArray[positionInList].target, patrolArray[positionInList].duration, patrolArray[positionInList].distanceFromTarget));
                break;
        }*/

        positionInList++;
        if (positionInList >= patrolArray.Length)
        {
            positionInList = 0;
            if (dontLoopPatrol)
            {
                Stop();
            }

        }
    }
    public void Stop()
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
        gameObject.GetComponent<NPCController>().isMoving = false;

    }
    public IEnumerator Wait(float waitTime)
    {

        gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
        gameObject.GetComponent<NPCController>().isMoving = false;
        yield return new WaitForSeconds(waitTime);
        NextPatrolOrInterrupt();
        yield break;


    }

    public IEnumerator MoveTo(Vector2 vectorDestination, float waitTime)
    {
        if (Mathf.Approximately(gameObject.transform.position.x, vectorDestination.x) == false || Mathf.Approximately(gameObject.transform.position.y, vectorDestination.y) == false)
        {
            gameObject.GetComponent<Animator>().SetFloat("Speed", 1f);
            gameObject.GetComponent<NPCController>().isMoving = true;
            var speed = gameObject.GetComponent<Animator>().GetFloat("Speed");
            Vector2 position = gameObject.transform.position;
            var horizontal = vectorDestination.x - position.x;
            var vertical = vectorDestination.y - position.y;
            var velocity = new Vector2(horizontal, vertical);
            velocity.Normalize();
            position.x = position.x + speed * velocity.x * Time.deltaTime;
            position.y = position.y + speed * velocity.y * Time.deltaTime;
            gameObject.GetComponent<Animator>().SetFloat("Look X", velocity.x);
            gameObject.GetComponent<Animator>().SetFloat("Look Y", velocity.y);
            
            yield return new WaitForSeconds(waitTime);
        }
        else 
        {
            NextPatrolOrInterrupt();
            yield break;
            
        }

    }
    public IEnumerator LookAt(Vector2 lookDirection, float waitTime)
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
        gameObject.GetComponent<Animator>().SetFloat("Look X", lookDirection.x);
        gameObject.GetComponent<Animator>().SetFloat("Look Y", lookDirection.y);
        gameObject.GetComponent<NPCController>().isMoving = false;
        yield return new WaitForSeconds(waitTime);
        NextPatrolOrInterrupt();

    }
    public IEnumerator FollowTarget(GameObject target, float waitTime, float distanceFromTarget)
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", 1f);
        Vector2 vectorFollowPosition = new Vector2(target.transform.position.x - distanceFromTarget, target.transform.position.y - distanceFromTarget);

        if (Mathf.Approximately(gameObject.transform.position.x, vectorFollowPosition.x) == false || Mathf.Approximately(gameObject.transform.position.y, vectorFollowPosition.y) == false)
        {

            gameObject.GetComponent<NPCController>().isMoving = true;
            var speed = gameObject.GetComponent<Animator>().GetFloat("Speed");
            Vector2 position = gameObject.transform.position;
            var horizontal = vectorFollowPosition.x;
            var vertical = vectorFollowPosition.y;
            var velocity = new Vector2(horizontal, vertical);
            velocity.Normalize();
            position.x = position.x + speed * velocity.x * Time.deltaTime;
            position.y = position.y + speed * velocity.y * Time.deltaTime;
            gameObject.GetComponent<Animator>().SetFloat("Look X", velocity.x);
            gameObject.GetComponent<Animator>().SetFloat("Look Y", velocity.y);
            
            yield return new WaitForSeconds(waitTime);
        }
        else
        {
            NextPatrolOrInterrupt();
            yield break;
        }
    }
    public IEnumerator ChaseTarget(GameObject target, float waitTime)
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", 1f);
        Vector2 relativeChasePosition = new Vector2(target.transform.position.x - gameObject.transform.position.x, target.transform.position.y - gameObject.transform.position.y);

        if (Mathf.Approximately(gameObject.transform.position.x, relativeChasePosition.x) == false || Mathf.Approximately(gameObject.transform.position.y, relativeChasePosition.y) == false)
        {

            gameObject.GetComponent<NPCController>().isMoving = true;
            var speed = gameObject.GetComponent<Animator>().GetFloat("Speed");
            Vector2 position = gameObject.transform.position;
            var horizontal = relativeChasePosition.x;
            var vertical = relativeChasePosition.y;
            var velocity = new Vector2(horizontal, vertical);
            velocity.Normalize();
            position.x = position.x + speed * velocity.x * Time.deltaTime;
            position.y = position.y + speed * velocity.y * Time.deltaTime;
            gameObject.GetComponent<Animator>().SetFloat("Look X", velocity.x);
            gameObject.GetComponent<Animator>().SetFloat("Look Y", velocity.y);

            yield return new WaitForSeconds(waitTime);
        }
        else
        {
            NextPatrolOrInterrupt();
            yield break;
        }

    }
    public IEnumerator RunFromTarget(GameObject target, float waitTime, float distanceFromTarget)
    {
        //gameObject.GetComponent<Animator>().SetFloat("Speed", 2f);
        //yield return new WaitForSeconds(waitTime);

        gameObject.GetComponent<Animator>().SetFloat("Speed", 1f);
        Vector2 vectorRunFromPosition = new Vector2(target.transform.position.x + distanceFromTarget, target.transform.position.y + distanceFromTarget);

        if (Mathf.Approximately(gameObject.transform.position.x, vectorRunFromPosition.x) == false || Mathf.Approximately(gameObject.transform.position.y, vectorRunFromPosition.y) == false)
        {

            gameObject.GetComponent<NPCController>().isMoving = true;
            var speed = gameObject.GetComponent<Animator>().GetFloat("Speed");
            Vector2 position = gameObject.transform.position;
            var horizontal = vectorRunFromPosition.x;
            var vertical = vectorRunFromPosition.y;
            var velocity = new Vector2(horizontal, vertical);
            velocity.Normalize();
            position.x = position.x + speed * velocity.x * Time.deltaTime;
            position.y = position.y + speed * velocity.y * Time.deltaTime;
            gameObject.GetComponent<Animator>().SetFloat("Look X", velocity.x);
            gameObject.GetComponent<Animator>().SetFloat("Look Y", velocity.y);

            yield return new WaitForSeconds(waitTime);
        }
        else
        {
            NextPatrolOrInterrupt();
            yield break;
        }


    }

    public void Interrupt(MovementOption[] _preinterrupt, MovementOption[] _interrupt, MovementOption[] _postinterrupt)
    {
        isInterrupted = true;
        preinterrupt = _preinterrupt;
        interrupt = _interrupt; 
        postinterrupt = _postinterrupt;
        StopAllCoroutines();
        interruptionState = InterruptionState.Preinterrupt;
        positionInInterrupt= -1;
        NextPatrolOrInterrupt();

    }

    private void NextInterrupt()
    {
        MovementOption[] currentInterruptArray;
        
        if (interruptionState == InterruptionState.Preinterrupt)
        {
            currentInterruptArray = preinterrupt;
        }
        else if (interruptionState == InterruptionState.Interrupt)
        {
            currentInterruptArray = interrupt;
        }
        else if (interruptionState == InterruptionState.Postinterrupt)
        {
            currentInterruptArray = postinterrupt;
        }
        else
        {
            currentInterruptArray = null;
            isInterrupted = false;
        }

        positionInInterrupt++;

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
                interruptionState = InterruptionState.None;
                if (runningCoroutine != null)
                {
                    StopCoroutine(runningCoroutine);
                    runningCoroutine = null;
                    isInterrupted = false;
                }
            }


        }



        TriggerMovementOption(currentInterruptArray[positionInInterrupt]);

        /*switch (currentInterruptArray[positionInInterrupt].movementOption)
        {
            case MovementOption.OptionType.Stop:
                Stop();
                break;

            case MovementOption.OptionType.Wait:
                Wait(currentInterruptArray[positionInInterrupt].duration);
                break;

            case MovementOption.OptionType.MoveTo:
                MoveTo(currentInterruptArray[positionInInterrupt].movementPosition, currentInterruptArray[positionInInterrupt].duration);
                break;

            case MovementOption.OptionType.LookAt:
                LookAt(currentInterruptArray[positionInInterrupt].movementPosition);
                break;

            case MovementOption.OptionType.FollowTarget:
                FollowTarget(currentInterruptArray[positionInInterrupt].target, currentInterruptArray[positionInInterrupt].duration, currentInterruptArray[positionInInterrupt].distanceFromTarget);
                break;

            case MovementOption.OptionType.ChaseTarget:
                ChaseTarget(currentInterruptArray[positionInInterrupt].target, currentInterruptArray[positionInInterrupt].duration);
                break;

            case MovementOption.OptionType.RunFromTarget:
                RunFromTarget(currentInterruptArray[positionInInterrupt].target, currentInterruptArray[positionInInterrupt].duration, currentInterruptArray[positionInInterrupt].distanceFromTarget);
                break;
        }*/
        
    }

    public void TriggerMovementOption(MovementOption movementOption)
    {

        switch (movementOption.movementOption)
        {
            case MovementOption.OptionType.Stop:
                Stop();
                break;

            case MovementOption.OptionType.Wait:
                runningCoroutine = StartCoroutine(Wait(movementOption.duration));
                break;

            case MovementOption.OptionType.MoveTo:
                runningCoroutine = StartCoroutine(MoveTo(movementOption.movementPosition, movementOption.duration));
                break;

            case MovementOption.OptionType.LookAt:
                runningCoroutine = StartCoroutine(LookAt(movementOption.movementPosition, movementOption.duration));
                break;

            case MovementOption.OptionType.FollowTarget:
                runningCoroutine = StartCoroutine(FollowTarget(movementOption.target, movementOption.duration, movementOption.distanceFromTarget));
                break;

            case MovementOption.OptionType.ChaseTarget:
                runningCoroutine = StartCoroutine(ChaseTarget(movementOption.target, movementOption.duration));
                break;

            case MovementOption.OptionType.RunFromTarget:
                runningCoroutine = StartCoroutine(RunFromTarget(movementOption.target, movementOption.duration, movementOption.distanceFromTarget));
                break;
        }
    }

    public void StopInterrupt()
    {
        
    }
}
