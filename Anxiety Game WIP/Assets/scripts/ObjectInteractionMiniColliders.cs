using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarObjectInteractionMiniColliders : MonoBehaviour
{

    public PillarObjectInteraction objectInteraction;

    public enum ColliderDirection { north, south, east, west};

    public ColliderDirection colliderDirection;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (colliderDirection == ColliderDirection.north)
            {
                objectInteraction.colliderDirection = PillarObjectInteraction.ColliderDirection.north;
            }
            else if (colliderDirection == ColliderDirection.south)
            {
                objectInteraction.colliderDirection = PillarObjectInteraction.ColliderDirection.south;
            }
            else if (colliderDirection == ColliderDirection.east)
            {
                objectInteraction.colliderDirection = PillarObjectInteraction.ColliderDirection.east;
            }
            else if (colliderDirection == ColliderDirection.west)
            {
                objectInteraction.colliderDirection = PillarObjectInteraction.ColliderDirection.west;
            }
            objectInteraction.playerTouchingObject = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //this is redundant with PillarObjectInteraction but hey, you never know if it will be needed and it doesn't hurt anything.
            objectInteraction.colliderDirection = PillarObjectInteraction.ColliderDirection.inactive;
            objectInteraction.playerTouchingObject = false;
        }
    }

}
