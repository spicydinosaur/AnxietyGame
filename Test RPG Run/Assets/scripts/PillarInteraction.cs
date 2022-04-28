using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PillarInteraction : MonoBehaviour
{
    private PlayerControls playerControls;

    public GameObject interactHere;
    private InputAction interact;
    public GameObject hero;

    public bool pillarInPosition;
    public Vector3 pillarMoveDirection;
    
    public bool canMoveUp;
    public bool canMoveDown;
    public bool canMoveLeft;
    public bool canMoveRight;

    public Vector3 oldPillarTransform;
    public float pillarMoveIncrement;
    public float maxPillarMoveIncrement;

    public float pillarMoveTime;



    private void OnEnable()
    {
        playerControls.Enable();
        interact.performed += ctx => Interact();

    }

    void Awake()
    {
        playerControls = new PlayerControls();
        interact = playerControls.PlayerActions.Interact;
        pillarInPosition = true;
    }



    public void Interact()
    {
        Debug.Log("interact button pressed");
        interactHere.SetActive(true);
        int layerMask = LayerMask.GetMask("Hero");

        oldPillarTransform = gameObject.transform.position;

        if (canMoveDown)
        {
            RaycastHit2D hit2Dup = Physics2D.Raycast(gameObject.transform.position, Vector3.up, .5f, layerMask);
            if (hit2Dup.collider != null)
            {

                pillarMoveDirection = new Vector3(0f, -.1f, 0f);

            }
        }
        if (canMoveUp)
        {
            RaycastHit2D hit2Ddown = Physics2D.Raycast(gameObject.transform.position, Vector3.down, .5f, layerMask);
            if (hit2Ddown.collider != null)
            {
                pillarMoveDirection = new Vector3(0f, .1f, 0f);
            }
        }
        if (canMoveRight)
        {
            RaycastHit2D hit2Dleft = Physics2D.Raycast(gameObject.transform.position, Vector3.left, .5f, layerMask);
            if ( hit2Dleft.collider != null)
            {
                pillarMoveDirection = new Vector3(.1f, 0f, 0f);
            }
        }
        if (canMoveLeft)
        {
            RaycastHit2D hit2Dright = Physics2D.Raycast(gameObject.transform.position, Vector3.right, .5f, layerMask);
            if (hit2Dright.collider != null)
            {
                pillarMoveDirection = new Vector3(-.1f, 0f, 0f);
            }
        }

        Debug.Log("Interact function triggered, pillarMoveDirection = " + pillarMoveDirection);

        pillarInPosition = false;
        StartCoroutine("pillarMove");

    }
    

    public IEnumerator pillarMove()
    {
        Debug.Log("pillarmove function activated");
        gameObject.transform.position = gameObject.transform.position + pillarMoveDirection;
        if (Vector3.Distance(oldPillarTransform, gameObject.transform.position) >= (pillarMoveIncrement * pillarMoveTime))
        {
            pillarInPosition = true;
            
        }
        else
        {
            pillarInPosition = false;
        }
  
        yield return new WaitForSeconds(.2f);


    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interactHere.SetActive(true);
            int layerMask = LayerMask.GetMask("Hero");

            if (canMoveDown)
            {
                RaycastHit2D hit2Dup = Physics2D.Raycast(gameObject.transform.position, Vector3.up, .5f, layerMask);
            }
            if (canMoveUp)
            {
                RaycastHit2D hit2Ddown = Physics2D.Raycast(gameObject.transform.position, Vector3.down, .5f, layerMask);
            }
            if (canMoveRight)
            {
                RaycastHit2D hit2Dleft = Physics2D.Raycast(gameObject.transform.position, Vector3.left, .5f, layerMask);
            }
            if (canMoveLeft)
            {
                RaycastHit2D hit2Dright = Physics2D.Raycast(gameObject.transform.position, Vector3.right, .5f, layerMask);
            }

        }
        else if (collider.gameObject.CompareTag("Misc Colliders"))
        {
            StopCoroutine("pillarMove");
            Debug.Log("coroutine stopped");
        }
    }



    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interactHere.SetActive(false);
        }
        else if (collider.gameObject.CompareTag("Misc Colliders"))
        {
     
        }
    }


    private void OnDisable()
    {

        playerControls.Disable();
    }
}
