using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ObjectInteraction : MonoBehaviour
{

    public Player heroScript;
    public Animator heroAnim;

    public Rigidbody2D objectRB;
    //public bool moveObjectPossible;
   
      
    public Vector3 moveIncrementVector;
    public Vector3 previousObjectLocation;
    public float moveIncrementVectorFloat;

    public bool objectMoveObstructed;
    public float stepsOnInteract;
    public float countingSteps;
    public float waitForSecondsCooroutine;

    public GameObject northMoveCollider;
    public GameObject southMoveCollider;
    public GameObject eastMoveCollider;
    public GameObject westMoveCollider;

    public enum ColliderDirection { north, south, east, west, inactive };

    public ColliderDirection colliderDirection;

    public bool playerTouchingObject;

    public bool puzzleComplete;


    // calculate if player is facing object or not



    public void Start()
    {
 
        objectMoveObstructed = false;
        objectMoveObstructed = false;

        previousObjectLocation = gameObject.transform.position;

        countingSteps = 0;


        objectRB = GetComponent<Rigidbody2D>();
        colliderDirection = ObjectInteraction.ColliderDirection.inactive;

    }


    public void InteractWithObject(InputAction.CallbackContext ctx)
    {

        Debug.Log("InteractWithObject() function activated");
        if (playerTouchingObject && !puzzleComplete)
        {

            if (colliderDirection == ObjectInteraction.ColliderDirection.north)
            {
                //object can move south
                moveIncrementVector = new Vector3(0, -moveIncrementVectorFloat, 0f);
            }
            else if (colliderDirection == ObjectInteraction.ColliderDirection.south)
            {
                //object can move north
                moveIncrementVector = new Vector3(0, moveIncrementVectorFloat, 0f);
            }
            else if (colliderDirection == ObjectInteraction.ColliderDirection.east)
            {
                //object can move east
                moveIncrementVector = new Vector3(-moveIncrementVectorFloat, 0, 0f);
            }
            else if (colliderDirection == ObjectInteraction.ColliderDirection.west)
            {
                //object can move west
                moveIncrementVector = new Vector3(moveIncrementVectorFloat, 0, 0f);
            }

            previousObjectLocation = gameObject.transform.position;
            gameObject.transform.position = gameObject.transform.position + moveIncrementVector;
            Debug.Log("InteractWithObject() test move activated " + objectRB.transform.position + " prev move location " +previousObjectLocation);
            gameObject.transform.position = previousObjectLocation;
            if (!objectMoveObstructed)
            {
                countingSteps = 0;
                StartCoroutine("objectMove");
                Debug.Log("pathway for object NOT +obstructed.");
                

            }
            else
            {
                Debug.Log("pathway for object obstructed.");
                //play failure sound here
            }
        }
    }
    

    public IEnumerator objectMove()
    {
        Debug.Log("objectMove coroutine activated");
        if (countingSteps < stepsOnInteract)
        {

            //is this right? I want x or y to be .5f or -.5f
            heroAnim.SetFloat("Look X", Mathf.Clamp01(moveIncrementVector.x));
            heroAnim.SetFloat("Look Y", Mathf.Clamp01(moveIncrementVector.y));
            gameObject.transform.position = gameObject.transform.position + (moveIncrementVector/stepsOnInteract);
            countingSteps++;

            

            yield return new WaitForSeconds(waitForSecondsCooroutine);

        }
        else
        {
            StopCoroutine("objectMove");
            countingSteps = 0;
            Debug.Log("coroutine stopped");
        }

    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            heroScript.objectInteraction = gameObject.GetComponent<ObjectInteraction>();

            northMoveCollider.SetActive(true);
            southMoveCollider.SetActive(true);
            eastMoveCollider.SetActive(true);
            westMoveCollider.SetActive(true);


        }

        else if (!collider.gameObject.GetComponent<Collider2D>().isTrigger || !collider.gameObject.CompareTag("interactable object") || !collider.gameObject.CompareTag("Scenery"))
        {
            objectMoveObstructed = true;
        }

    }


    
    private void OnCollisionExit2D(Collision2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {

            heroScript.objectInteraction = null;

            //this is redundant with ObjectInteractionMiniColliders but hey, you never know if it will be needed and it doesn't hurt anything.
            colliderDirection = ObjectInteraction.ColliderDirection.inactive;

            northMoveCollider.SetActive(false);
            southMoveCollider.SetActive(false);
            eastMoveCollider.SetActive(false);
            westMoveCollider.SetActive(false);
        }
        else if (!collider.gameObject.GetComponent<Collider2D>().isTrigger || !collider.gameObject.CompareTag("interactable object") || !collider.gameObject.CompareTag("Scenery"))
        {
            objectMoveObstructed = false;
        }
    }

}
