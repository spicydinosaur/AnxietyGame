using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StopOnTrigger : MonoBehaviour
{

    public AdvancedPatrolScript controller;
    public MovementOption option;
    public Player player;
    public MovementOption[] preinterrupt;
    public MovementOption[] interrupt;
    public MovementOption[] postinterrupt;

    

    public void Awake()
    {

        controller = GetComponentInParent<AdvancedPatrolScript>();


    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            
            option.movementPosition = Vector3.Normalize(collider.transform.position - gameObject.transform.position);
            controller.Interrupt(preinterrupt, interrupt, postinterrupt);

        }
    }



    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //change this to StopInterrupt()
            controller.NextPatrol();

        }
    }

}


