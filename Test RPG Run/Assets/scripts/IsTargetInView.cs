using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//find answers to attack script to make slime stop moving on player death
//line 28

public class IsTargetInView : MonoBehaviour
{
    public EnemyController controller;
    public GameObject portalAnimation;

    public void Awake()
    {

        controller = GetComponentInParent<EnemyController>();

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            controller.prevMovementType = controller.currentMovementType;
            controller.currentMovementType = NPCController.movementType.ToOther;
            controller.targetToChase = collider.gameObject;
            controller.ChangeTargetViewState(true);
            controller.isChasing = true;


        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            controller.prevMovementType = controller.currentMovementType;
            controller.currentMovementType = controller.startingMovementType;
            controller.isChasing = false;
            controller.ChangeTargetViewState(false);

            //Debug.Log("OnTriggerExit2D function triggered by player!");
            //below must work with player porting out instead of dying and creating a tombstone

        if (GetComponentInParent<NPCPatrolRoute>().patrollingBool == true)
        {
            controller.SetNextLocation(GetComponentInParent<NPCPatrolRoute>().GetNextCoord());
        }
                //Debug.Log("disabled player detected!");

           

        }

    }
}
