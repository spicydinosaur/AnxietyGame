using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

//Connected to the script MovementOption
//OptionType  { Stop, Wait, MoveTo, LookAt, FollowTarget, ChaseTarget, RunFromTarget }
//fix anything that checks mathf.approximately to other things for followtarget and chasetarget.
//must implement modifications based on colliders
//need a timer to be utilized by the interrupts
public class AdvancedPatrolScript : MonoBehaviour
{
    [SerializeField]
    public MovementOption[] patrolArray;
    public int positionInList;
    public int runAmount;
    public float currentMovementOptionElapsedTime;


    public bool isInterrupted = false;
    public float interruptMovementOptionElapsedTime;
    [SerializeField]
    public MovementOption[] preinterrupt;
    [SerializeField]
    public MovementOption[] interrupt;
    [SerializeField]
    public MovementOption[] postinterrupt;





    public void NextPatrol()
    {
        positionInList++;
        switch (patrolArray[positionInList].movementOption)
        {
            case MovementOption.OptionType.Stop:
                break;

            case MovementOption.OptionType.Wait:
                break;

            case MovementOption.OptionType.MoveTo:
                break;

            case MovementOption.OptionType.LookAt:
                break;

            case MovementOption.OptionType.FollowTarget:
                break;

            case MovementOption.OptionType.ChaseTarget:
                break;

            case MovementOption.OptionType.RunFromTarget:
                break;
        }
        
        
    }
    public void Stop()
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
        gameObject.GetComponent<NPCController>().isMoving = false;

        NextPatrol();
    }
    public IEnumerator Wait(float waitTime)
    {
        if (runAmount > 0)
        {
            NextPatrol();
        }
        else
        {
            yield return new WaitForSeconds(waitTime);
            gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
            gameObject.GetComponent<NPCController>().isMoving = false;
            runAmount = 0;
            runAmount++;
        }

    }

    public IEnumerator MoveTo(Vector2 vectorDestination, float waitTime)
    {
        if (Mathf.Approximately(gameObject.transform.position.x, vectorDestination.x) == false || Mathf.Approximately(gameObject.transform.position.y, vectorDestination.y) == false)
        {
            yield return new WaitForSeconds(waitTime);
            gameObject.GetComponent<Animator>().SetFloat("Speed", 2f);
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
        }
        else 
        {
            NextPatrol();
            yield break;
            
        }

    }
    public void LookAt(Vector2 lookDirection)
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
        gameObject.GetComponent<Animator>().SetFloat("Look X", lookDirection.x);
        gameObject.GetComponent<Animator>().SetFloat("Look Y", lookDirection.y);
        
        NextPatrol();

    }
    public IEnumerator FollowTarget(GameObject target, float movementSpeed, float waitTime, float distanceFromTarget)
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", movementSpeed);
        Vector2 vectorFollowPosition = new Vector2(target.transform.position.x - distanceFromTarget, target.transform.position.y - distanceFromTarget);

        if (Mathf.Approximately(gameObject.transform.position.x, vectorFollowPosition.x) == false || Mathf.Approximately(gameObject.transform.position.y, vectorFollowPosition.y) == false)
        {
            yield return new WaitForSeconds(waitTime);
            Vector2 targetPosition = target.transform.position;


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
        }
        else
        {
            NextPatrol();
            yield break;
        }
    }
    public IEnumerator ChaseTarget(GameObject target, float movementSpeed, float waitTime)
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", 2f);
        Vector2 relativeChasePosition = new Vector2(target.transform.position.x - gameObject.transform.position.x, target.transform.position.y - gameObject.transform.position.y);

        if (Mathf.Approximately(gameObject.transform.position.x, relativeChasePosition.x) == false || Mathf.Approximately(gameObject.transform.position.y, relativeChasePosition.y) == false)
        {
            yield return new WaitForSeconds(waitTime);
            Vector2 targetPosition = target.transform.position;


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
        }
        else
        {
            NextPatrol();
            yield break;
        }

    }
    public IEnumerator RunFromTarget(Vector2 vectorDestination, float waitTime)
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", 2f);
        yield return new WaitForSeconds(waitTime);

    }

    public void Interrupt()
    {
        NextPatrol();
    }
}
