using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//find answers to attack script to make slime stop moving on player death
//line 28

public class IsTargetInView : MonoBehaviour
{
    public EnemyController controller;
    public GameObject tombstone;

    public void Awake()
    {

        controller = GetComponentInParent<EnemyController>();

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            controller.currentMovementType = NPCController.movementType.ToOther;
            controller.targetToChase = collider.GetComponent<Transform>();
            controller.ChangeTargetViewState(true);

        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            controller.currentMovementType = NPCController.movementType.Patrol;
            controller.targetToChase = null;
            controller.ChangeTargetViewState(false);

            //Debug.Log("OnTriggerExit2D function triggered by player!");

            if (tombstone.activeSelf == true)
            {
                controller.SetNextLocation(GetComponentInParent<NPCPatrolRoute>().GetNextCoord());
                controller.PauseMovement(3f);
                controller.prevMovementType = NPCController.movementType.Patrol;
                //Debug.Log("disabled player detected!");

            }

        }

    }
}
