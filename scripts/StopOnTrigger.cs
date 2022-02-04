using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopOnTrigger : MonoBehaviour
{

    public NPCController controller;
    public Player player;
    private Animator animator;
    

    public void Awake()
    {

        controller = GetComponentInParent<NPCController>();
        animator = controller.animator;


    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            controller.ChangeState(NPCController.movementType.Stop);

        }
    }



    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            controller.RevertState();

        }
    }

}


