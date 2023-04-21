using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveViewCone : MonoBehaviour
{

    public Rigidbody2D rigidBody;

    public NPCController controller;

    public AdvancedPatrolScript advancedPatrolScript;

    public MovementOption movementOption;

    [SerializeField]
    public MovementOption[] canseetarget_preinterrupt;
    [SerializeField]
    public MovementOption[] canseetarget_interrupt;
    [SerializeField]
    public MovementOption[] canseetarget_postinterrupt;


    public bool canSeeTarget = false;

    public float numberOfBlinks;
    public float currentBlink;

    public float textDisplayTime;
    public float currentTextDisplayTime;
    public TextMeshProUGUI textMeshProUGUI;
    public float secondsPerHalfBlink;

    //These need default values.
    public float canSeeTargetDisplayTime;
    public float canNOTSeeTargetDisplayTime;
    public float minimumViewDistance;



    //preinterrupt is to display "!" text, interrupt is to move to target on a loop until that is fulfilled, postinterrupt is to display "?" text.

    private void Awake()
    {
        controller = GetComponentInParent<NPCController>();
        rigidBody = GetComponentInParent<Rigidbody2D>();
        advancedPatrolScript = GetComponentInParent<AdvancedPatrolScript>(); 
        movementOption = GetComponent<MovementOption>();
        textMeshProUGUI.enabled = false;
        currentTextDisplayTime = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        //is it looking left?
        if (controller.lookDirection.x > .5f && controller.lookDirection.y > -.69f && controller.lookDirection.y < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
        }
        //is it looking right?
        else if (controller.lookDirection.x < -0.5f && controller.lookDirection.y > -.69f && controller.lookDirection.y < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270);
        }
        //is it looking backward?
        else if (controller.lookDirection.y > .5f && controller.lookDirection.x > -.69f && controller.lookDirection.x < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
        }
        else //we are looking forward
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        }

        if (textDisplayTime != 0f)
        {

            currentTextDisplayTime += Time.deltaTime;
            /*if (textMeshProUGUI == false
                &&
                currentTextDisplayTime <= (currentBlink + .5f) * textDisplayTime / numberOfBlinks
                &&
                currentTextDisplayTime >= (currentBlink) * textDisplayTime / numberOfBlinks)
            {
                textMeshProUGUI.enabled = true;
            }
            else if (textMeshProUGUI.enabled == true
                &&
                currentTextDisplayTime >= (currentBlink + 0.5f) * textDisplayTime / numberOfBlinks
                &&
                currentTextDisplayTime <= (currentBlink + 1f) * textDisplayTime / numberOfBlinks)
            {
                textMeshProUGUI.enabled = false;
                currentBlink++;
            }*/

            if (currentTextDisplayTime >= textDisplayTime)
            {
                textDisplayTime = 0f;
                currentTextDisplayTime = 0f;
                currentBlink = 0;
                textMeshProUGUI.enabled = false;
                textMeshProUGUI.SetText("");
            }

        }
    }

    public void ChangeTargetViewState()
    {

        if (!controller.isChasing)
        {
            if (Vector2.Distance(gameObject.transform.position, controller.target.transform.position) >= minimumViewDistance)
            {
                currentBlink = 0;
                currentTextDisplayTime = 0f;

                if (canSeeTarget)
                {
                    textDisplayTime = canSeeTargetDisplayTime;
                    advancedPatrolScript.Interrupt(canseetarget_preinterrupt, canseetarget_interrupt, canseetarget_postinterrupt);
                    textMeshProUGUI.enabled = true;
                    textMeshProUGUI.SetText("!");

                }
                else
                {
                    textDisplayTime = canNOTSeeTargetDisplayTime;
                    advancedPatrolScript.StopInterrupt();
                    textMeshProUGUI.enabled = true;
                    textMeshProUGUI.SetText("?");
                }
            }
        }


    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            canSeeTarget = true;
            ChangeTargetViewState();

        }
    }



    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //change this to StopInterrupt()
            canSeeTarget= false;
            ChangeTargetViewState();

        }
    }
}
