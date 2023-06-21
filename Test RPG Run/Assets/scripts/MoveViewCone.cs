using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveViewCone : MonoBehaviour
{



    public AdvancedPatrolScript advancedPatrolScript;

    public MovementOption movementOption;

    [SerializeField]
    public MovementOption[] canseetarget_preinterrupt;
    [SerializeField]
    public MovementOption[] canseetarget_interrupt;
    [SerializeField]
    public MovementOption[] canseetarget_postinterrupt;

    [SerializeField]
    public MovementOption[] canNOTseetarget_preinterrupt;
    [SerializeField]
    public MovementOption[] canNOTseetarget_interrupt;
    [SerializeField]
    public MovementOption[] canNOTseetarget_postinterrupt;


    public bool canSeeTarget = false;





    //preinterrupt is to display "!" text, interrupt is to move to target on a loop until that is fulfilled, postinterrupt is to display "?" text.

    private void Awake()
    {
        advancedPatrolScript = GetComponentInParent<AdvancedPatrolScript>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //is it looking left?
        if (GetComponentInParent<Animator>().GetFloat("Look X") >= .5f && GetComponentInParent<Animator>().GetFloat("Look Y") >= -.69f && GetComponentInParent<Animator>().GetFloat("Look Y") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
        }
        //is it looking right?
        else if (GetComponentInParent<Animator>().GetFloat("Look X") <= -0.5f && GetComponentInParent<Animator>().GetFloat("Look Y") >= -.69f && GetComponentInParent<Animator>().GetFloat("Look Y") < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270);
        }
        //is it looking backward?
        else if (GetComponentInParent<Animator>().GetFloat("Look Y") >= .5f && GetComponentInParent<Animator>().GetFloat("Look X") >= -.69f && GetComponentInParent<Animator>().GetFloat("Look X")  < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
        }
        else //we are looking forward
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    public void ChangeTargetViewState()
    {

            if (canSeeTarget)
            {

                advancedPatrolScript.Interrupt(canseetarget_preinterrupt, canseetarget_interrupt, canseetarget_postinterrupt);
                
            }
            else
            {

                advancedPatrolScript.Interrupt(canNOTseetarget_preinterrupt, canNOTseetarget_interrupt, canNOTseetarget_postinterrupt);

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
            canSeeTarget= false;
            ChangeTargetViewState();
        }
    }
}
