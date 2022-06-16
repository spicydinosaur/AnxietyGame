using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ObjectInteraction : MonoBehaviour
{
    private PlayerControls playerControls;

    public GameObject interactHere;
    private InputAction interact;
    public Animator heroAnim;
    public bool movePillarPossible;
    public Vector2 move;
    public float heroStartPosition;
    public int pillarMoveIncrement;
    public GameObject hero;
    public int heroTransformRoundedY;
    public int heroTransformRoundedX;








    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerControls();
        interact = playerControls.PlayerActions.Interact;
        pillarMoveIncrement = 0;
        heroStartPosition = -57;
    }

    private void Start()
    {

    }

    public void Update()
    {
        interact = playerControls.PlayerActions.Interact;
        if (interact.triggered && movePillarPossible == true && pillarMoveIncrement<=12)
        {
            Interact();
            Debug.Log("interact button pressed");
        }
    }

    // Update is called once per frame

    public void Interact()
    {
        Debug.Log("interact function activated");
        StartCoroutine("pillarMove");

    }
    

    public IEnumerator pillarMove()
    {
        Debug.Log("pillarmove function activated");
        gameObject.transform.position = gameObject.transform.position + new Vector3(-.5f, 0f, 0f);
        
        pillarMoveIncrement = pillarMoveIncrement + 1;
        heroStartPosition = heroStartPosition -.5f;
        if (pillarMoveIncrement >= 12)
        {
            StopCoroutine("pillarMove");
            Debug.Log("coroutine stopped");
        }

        yield return new WaitForSeconds(.1f);


    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interactHere.SetActive(true);

            heroTransformRoundedY = Mathf.RoundToInt(hero.transform.position.y);
            heroTransformRoundedX = Mathf.RoundToInt(hero.transform.position.x);

            if ( heroTransformRoundedY == 27 && heroTransformRoundedX == heroStartPosition && heroAnim.GetFloat("Look X") <= -.1)
            {

                movePillarPossible = true;
            }
            else
            {
                movePillarPossible = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interactHere.SetActive(true);

            heroTransformRoundedY = Mathf.RoundToInt(hero.transform.position.y);
            heroTransformRoundedX = Mathf.RoundToInt(hero.transform.position.x);

            if (heroTransformRoundedY == 27 && heroTransformRoundedX == heroStartPosition && heroAnim.GetFloat("Look X") <= -.1)
            {

                movePillarPossible = true;
            }
            else
            {
                movePillarPossible = false;
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D collider)
    {
        movePillarPossible = false;
        interactHere.SetActive(false);
    }

    private void OnEnable()
    {
        playerControls.Enable();

    }

    private void OnDisable()
    {

        playerControls.Disable();
    }
}
