using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarObjectInteractionColliderSetup : MonoBehaviour
{
    public Player player;
    public GameObject northMoveCollider;
    public GameObject southMoveCollider;
    public GameObject eastMoveCollider;
    public GameObject westMoveCollider;

    public PillarObjectInteraction objectInteraction;
    
    
    public void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("colliding object tag" + collider.gameObject.tag);
        if (collider.gameObject.CompareTag("Player"))
        {


            northMoveCollider.SetActive(true);
            southMoveCollider.SetActive(true);
            eastMoveCollider.SetActive(true);
            westMoveCollider.SetActive(true);

            player.objectInteraction = objectInteraction;



        }


    }

    public void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {

            player.objectInteraction = null;

            northMoveCollider.SetActive(false);
            southMoveCollider.SetActive(false);
            eastMoveCollider.SetActive(false);
            westMoveCollider.SetActive(false);

            objectInteraction.canMove = false;

        }

    }

}
