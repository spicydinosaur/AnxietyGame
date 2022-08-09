using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ObjectInteraction : MonoBehaviour
{
    public PlayerControls playerControls;
    public InputAction interact;

    public GameObject hero;
    public Animator heroAnim;

    public Rigidbody2D objectRB;
    public bool moveObjectPossible;
   
      
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

    public Vector3 objectReturnLocation;


    // calculate if player is facing object or not


    void Awake()
    {
        playerControls = hero.GetComponent<Player>().playerControls;
        playerControls.PlayerActions.Interact.performed += ctx => InteractWithObject();

        objectReturnLocation = GetComponentInParent<Transform>().position;
        objectRB = GetComponentInParent<Rigidbody2D>();
        colliderDirection = ObjectInteraction.ColliderDirection.inactive;

    }

    public void Start()
    {
        objectMoveObstructed = false;
        moveObjectPossible = false;

        previousObjectLocation = GetComponentInParent<Rigidbody2D>().position;

        countingSteps = 0;

        heroAnim = hero.GetComponent<Animator>();
    }

    public void Update()
    {
        if (playerTouchingObject && !puzzleComplete)
        {

            if (colliderDirection == ObjectInteraction.ColliderDirection.north)
            {
                moveObjectPossible = true;
                //object can move south
                moveIncrementVector = new Vector3(0, -moveIncrementVectorFloat, 0f);
            }
            else if (colliderDirection == ObjectInteraction.ColliderDirection.south)
            {
                moveObjectPossible = true;
                //object can move north
                moveIncrementVector = new Vector3(0, moveIncrementVectorFloat, 0f);
            }
            else if (colliderDirection == ObjectInteraction.ColliderDirection.east)
            {
                moveObjectPossible = true;
                //object can move east
                moveIncrementVector = new Vector3(-moveIncrementVectorFloat, 0, 0f);
            }
            else if (colliderDirection == ObjectInteraction.ColliderDirection.west)
            {
                moveObjectPossible = true;
                //object can move west
                moveIncrementVector = new Vector3(moveIncrementVectorFloat, 0, 0f);
            }

        }
    }



    public void InteractWithObject()
    {

        Debug.Log("InteractWithObject() function activated");
        if (moveObjectPossible)
        {

            previousObjectLocation = objectRB.transform.position;
            objectRB.transform.position = objectRB.transform.position + moveIncrementVector;
            Debug.Log("InteractWithObject() test move activated " + objectRB.transform.position + " prev move location " +previousObjectLocation);
            objectRB.transform.position = previousObjectLocation;
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
            heroAnim.SetFloat("Look X", Mathf.Clamp01(moveIncrementVector.x/2f));
            heroAnim.SetFloat("Look Y", Mathf.Clamp01(moveIncrementVector.y/2f));
            objectRB.transform.position = objectRB.transform.position + (moveIncrementVector * stepsOnInteract);
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
            playerTouchingObject = false;
            moveObjectPossible = false;

            //this is redundant with ObjectInteractionMiniColliders but hey, you never know if it will be needed and it doesn't hurt anything.
            colliderDirection = ObjectInteraction.ColliderDirection.inactive;

            northMoveCollider.SetActive(false);
            southMoveCollider.SetActive(false);
            eastMoveCollider.SetActive(false);
            westMoveCollider.SetActive(false);
        }
    }

}
