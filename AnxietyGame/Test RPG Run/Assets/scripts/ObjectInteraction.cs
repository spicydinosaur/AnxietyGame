using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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
    public bool objectMoveObstructed;
    public int stepsOnInteract;
    public int countingSteps;

    public GameObject northMoveCollider;
    public GameObject southMoveCollider;
    public GameObject eastMoveCollider;
    public GameObject westMoveCollider;

    public bool northColliderTriggered;
    public bool southColliderTriggered;
    public bool eastColliderTriggered;
    public bool westColliderTriggered;

    public bool playerFacingObject;

    public bool puzzleComplete;

    public Vector3 objectReturnLocation;


    // calculate if player is facing object or not


    void Awake()
    {
        playerControls = hero.GetComponent<Player>().playerControls;
        Debug.Log("playerControls = " + playerControls);
        interact = playerControls.PlayerActions.Interact;
        interact.performed += ctx => InteractWithObject();

        GetComponentInParent<Transform>().position = objectReturnLocation;
        objectRB = GetComponentInParent<Rigidbody2D>();

        objectMoveObstructed = false;
        moveObjectPossible = false;

        previousObjectLocation = GetComponentInParent<Rigidbody2D>().position;

        countingSteps = 0;

        heroAnim = hero.GetComponent<Animator>();
    }

    public void Update()
    {
        if (playerFacingObject && !puzzleComplete)
        {

            if (northColliderTriggered)
            {
                moveObjectPossible = true;
                //object can move south
                moveIncrementVector = new Vector2(0, -1);
            }
            else if (southColliderTriggered)
            {
                moveObjectPossible = true;
                //object can move south
                moveIncrementVector = new Vector2(0, 1);
            }
            else if (eastColliderTriggered)
            {
                moveObjectPossible = true;
                //object can move south
                moveIncrementVector = new Vector2(1, 0);
            }
            else if (westColliderTriggered)
            {
                moveObjectPossible = true;
                //object can move south
                moveIncrementVector = new Vector2(-1, 0);
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
            objectRB.transform.position = previousObjectLocation;
            if (!objectMoveObstructed)
            {
                countingSteps = 0;
                StartCoroutine("objectMove");
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
            heroAnim.SetFloat("Look X", moveIncrementVector.x);
            heroAnim.SetFloat("Look Y", moveIncrementVector.y);
            objectRB.transform.position = objectRB.transform.position + (moveIncrementVector / stepsOnInteract);
            countingSteps++;
            yield return new WaitForSeconds(.1f);

        }
        else
        {
            StopCoroutine("objectMove");
            countingSteps = 0;
            Debug.Log("coroutine stopped");
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            northMoveCollider.SetActive(true);
            southMoveCollider.SetActive(true);
            eastMoveCollider.SetActive(true);
            westMoveCollider.SetActive(true);
        }

        else if (!collider.isTrigger || !collider.gameObject.CompareTag("interactable object"))
        {
            objectMoveObstructed = true;
        }

    }


    
    private void OnCollisionExit2D(Collision2D collider)
    {
        moveObjectPossible = false;

        northMoveCollider.SetActive(false);
        southMoveCollider.SetActive(false);
        eastMoveCollider.SetActive(false);
        westMoveCollider.SetActive(false);

    }

}
