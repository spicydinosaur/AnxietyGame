using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopOnTrigger : MonoBehaviour
{

    public NPCController controller;
    public Player player;
    

    public void Awake()
    {

        controller = GetComponentInParent<NPCController>();


    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        { 
            if (controller.currentMovementType != NPCController.movementType.Stop)
            {

                controller.ChangeState(NPCController.movementType.Stop);
                Debug.Log("onTriggerEnter2D: controller.prevMovementType equals" + controller.prevMovementType);
                Debug.Log("onTriggerEnter2D: controller.currentMovementType equals" + controller.currentMovementType);

            }
        }
    }



    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            controller.RevertState();
            Debug.Log("onTriggerExit2D: controller.prevMovementType equals" + controller.prevMovementType);
            Debug.Log("onTriggerExit2D: controller.currentMovementType equals" + controller.currentMovementType);

        }
    }

}


