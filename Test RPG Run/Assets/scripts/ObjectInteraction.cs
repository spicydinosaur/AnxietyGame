using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ObjectInteraction : MonoBehaviour
{
    private PlayerControls playerControls;
    public GameManager gameManager;


    private InputAction interact;
    public Animator heroAnim;
    public Vector2 move;
    public GameObject hero;
    public LayerMask layerMask;








    // Start is called before the first frame update
    void Awake()
    {
        playerControls = gameManager.playerControls;
        interact = playerControls.PlayerActions.Interact;


    }

    private void Start()
    {

    }

    public void Update()
    {
        if (interact.triggered)
        {
            Interact();
            Debug.Log("interact button pressed");
        }
    }

    // Update is called once per frame

    public void Interact()
    {


        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, -Vector2.up, .5f, layerMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -Vector2.down, .5f, layerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -Vector2.left, .5f, layerMask);
        RaycastHit2D hitright = Physics2D.Raycast(transform.position, -Vector2.right, .5f, layerMask);

        Debug.Log("interact function activated");
        StartCoroutine("pillarMove");

    }
    

    public IEnumerator pillarMove()
    {
        Debug.Log("pillarmove function activated");
        gameObject.transform.position = gameObject.transform.position + new Vector3(-.5f, 0f, 0f);
        
        StopCoroutine("pillarMove");
        Debug.Log("coroutine stopped");

        yield return new WaitForSeconds(.1f);


    }




}
