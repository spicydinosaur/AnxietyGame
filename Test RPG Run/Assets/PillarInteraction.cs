using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PillarInteraction : MonoBehaviour
{
    private PlayerControls playerControls;

    public GameObject interactHere;
    private InputAction interact;
    public Animator heroAnim;
    public float compareTransformY;
    public bool movePillarPossible;
    public Vector2 move;
    public float moveTransform;
    public int heroStartPosition;
    public int pillarMoveIncrement;
    public GameObject hero;
    public int heroTransformRoundedY;
    public int heroTransformRoundedX;







    // Start is called before the first frame update
    void Awake()
    {

        interact = playerControls.PlayerActions.Interact;
        pillarMoveIncrement = 1;
        heroStartPosition = 57;
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.IsPressed() && movePillarPossible == true)
        {
            Debug.Log("interact button pressed and movePillarPossible = true");
            StartCoroutine("pillarMove");
            

        }
    }

    IEnumerator pillarMove()
    {

        transform.position = transform.position + new Vector3(-.1f, 0f, 0f);
        
        pillarMoveIncrement = pillarMoveIncrement++;
        heroStartPosition = heroStartPosition++;
        if (pillarMoveIncrement >= 10)
        {
            StopCoroutine("pillarMove");
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
        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }
}
