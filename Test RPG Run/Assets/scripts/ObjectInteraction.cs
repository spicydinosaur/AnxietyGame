using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ObjectInteraction : MonoBehaviour
{

    public Player heroScript;
    public Animator heroAnim;

    public BoxCollider2D objectBC;
    //public bool moveObjectPossible;


    public Vector3 moveIncrementVector;
    public Vector3 previousObjectLocation;
    public float moveIncrementVectorFloat;

    public bool objectMoveObstructed;
    public bool isInBoundary;
    public float stepsOnInteract;
    public float countingSteps;
    public float countingStepsMax;
    public float waitForSecondsCoroutine;

    public GameObject northMoveCollider;
    public GameObject southMoveCollider;
    public GameObject eastMoveCollider;
    public GameObject westMoveCollider;

    public enum ColliderDirection { north, south, east, west, inactive };

    public ColliderDirection colliderDirection;

    public bool playerTouchingObject;

    public bool puzzleComplete;

    public bool canMove;

    public bool objectMoveRunning;

    public Vector2 direction;
    public Vector2 size;
    public Vector3 boxCastMovement;

    public GameObject specialObject;
    public float boxDistance;

    public AudioSource pillarGrindSound;

    // calculate if player is facing object or not



    public void Start()
    {

        objectMoveObstructed = false;
        playerTouchingObject = false;
        canMove = false;
        objectMoveRunning = false;

        //experimental below
        countingSteps = 0f;


        previousObjectLocation = gameObject.transform.position;

        countingSteps = countingStepsMax;

        objectBC = GetComponent<BoxCollider2D>();
        colliderDirection = ObjectInteraction.ColliderDirection.inactive;


    }


    public void InteractWithObject()
    {
        isInBoundary = false;
        objectMoveObstructed = false;
        canMove = false;
        Debug.Log("InteractWithObject() function activated");
        if (playerTouchingObject && !puzzleComplete)
        {

            if (colliderDirection == ObjectInteraction.ColliderDirection.north)
            {
                //object can move south
                moveIncrementVector = new Vector3(0, -moveIncrementVectorFloat);
                direction = Vector2.down;

            }
            else if (colliderDirection == ObjectInteraction.ColliderDirection.south)
            {
                //object can move north
                moveIncrementVector = new Vector3(0, moveIncrementVectorFloat);
                direction = Vector2.up;
            }
            else if (colliderDirection == ObjectInteraction.ColliderDirection.east)
            {
                //object can move east
                moveIncrementVector = new Vector3(-moveIncrementVectorFloat, 0);
                direction = Vector2.left;
            }
            else if (colliderDirection == ObjectInteraction.ColliderDirection.west)
            {
                //object can move west
                moveIncrementVector = new Vector3(moveIncrementVectorFloat, 0);
                direction = Vector2.right;
            }

            var size = objectBC.size;
            int layerMask = LayerMask.GetMask("Enemy", "Boundary Box", "Player", "NPC", "Scenery");
            RaycastHit2D[] boxCast = Physics2D.BoxCastAll(gameObject.transform.position + moveIncrementVector, size, 0f, direction, .001f, layerMask);
            for (int i = 0; i < boxCast.Length; i++)
            {
                Debug.Log("colliding object " + boxCast[i].collider.gameObject.name);
                if (boxCast[i].collider.gameObject.tag == "boundary box")
                {
                    isInBoundary = true;
                    continue;
                }
                else if (boxCast[i].collider.gameObject.tag == "Spell Interaction")
                {
                    continue;
                }
                else
                {
                    objectMoveObstructed = true;
                    break;
                }
            }

            if (!objectMoveObstructed && isInBoundary && !puzzleComplete)
            {
                canMove = true;
            }


            if (canMove)
            {
                countingSteps = countingStepsMax;
                StartCoroutine("objectMove");
                Debug.Log("pathway for object NOT obstructed.");


            }
            else
            {
                Debug.Log("pathway for object obstructed.");
                //play failure sound here
            }
        }
    }

    public void Update()
    {
        if (objectMoveRunning == true)
        {
             if (pillarGrindSound.isPlaying == false)
            {

                pillarGrindSound.Play();

            }
        }
    }

    public IEnumerator objectMove()
    {
        objectMoveRunning = true;
        Debug.Log("objectMove coroutine activated");
        while (countingSteps <= stepsOnInteract)
        {
            if (countingSteps == stepsOnInteract)
            {
                StopCoroutine("objectMove");
                countingSteps = 0;
                Debug.Log("coroutine stopped");
                objectMoveRunning = false;
                pillarGrindSound.Stop();
            }

            Debug.Log("countingSteps = " + countingSteps + " and stepsOnInteract = " + stepsOnInteract);

            specialObject.transform.position = specialObject.transform.position + (moveIncrementVector / stepsOnInteract);
            countingSteps++;
            Debug.Log("after addition countingSteps = " + countingSteps);

            yield return new WaitForSeconds(waitForSecondsCoroutine);



        }
    }
}


